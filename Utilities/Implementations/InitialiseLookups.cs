using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;
using Utilities.Interfaces;
using Utilities.Model;

namespace Utilities.Implementations
{
    public class InitialiseLookups : IInitialiseLookups
    {
        private readonly ILogger _logger;

        public InitialiseLookups(ILogger logger)
        {
            _logger = logger;
        }

        public void GeneratePostDeploymentScripts()
        {
            var postDeployment = new PostDeployment();

            GetInsertsFromFile(postDeployment.DataFile, postDeployment.Inserts);

            GetIdentitiesFromDataFile(postDeployment.DataFile, postDeployment.Identities);

            GetValuesFromInserts(postDeployment.Inserts, postDeployment.Values);

            GetTableFromInserts(postDeployment.Inserts, postDeployment.Tables);

            CreateInitialiseLookupSqlFiles(postDeployment.Values, postDeployment.Files);

            PopulateInitialiseLookupSqlFiles(postDeployment.Tables, postDeployment.Values, postDeployment.Files,
                postDeployment.Inserts, postDeployment.Identities);

            CreatePostDeploymentScript(postDeployment.Files);
        }

        public void GetInsertsFromFile(IEnumerable<string> dataFile, List<string> inserts)
        {
            foreach (var line in dataFile) GetInsertFromDataFile(line, inserts);
        }

        public void GetInsertFromDataFile(string line, List<string> inserts)
        {
            if (line.Contains("INSERT [dbo].[") && line.Contains("] (")) inserts.Add(line);
        }

        public void GetIdentitiesFromDataFile(IEnumerable<string> dataFile, List<string> identities)
        {
            foreach (var line in dataFile)
                if (line.Contains("SET IDENTITY_INSERT") && line.Contains("ON"))
                    identities.Add(line);
        }

        public void GetTableFromInserts(List<string> inserts, List<Tuple<string, List<Field>>> tables)
        {
            foreach (var insert in inserts)
            {
                var pFrom = insert.IndexOf("INSERT [dbo].[", StringComparison.Ordinal) + "INSERT [dbo].[".Length;
                var pTo = insert.LastIndexOf("] (", StringComparison.Ordinal);
                var tableName = insert.Substring(pFrom, pTo - pFrom);

                if (!tables.Any(tbl => tbl.Item1.Contains(tableName)))
                {
                    var myTypeSignature = tableName;

                    var myListOfFields = new List<Field>();
                    var columns = CmsDbContext.GetTableColumnsAndTypes(tableName);
                    foreach (var tableColumn in columns)
                    {
                        var fieldType = MapSqlTypeToCSharpType(tableColumn.Item2);
                        var newField = new Field
                        {
                            FieldName = tableColumn.Item1,
                            FieldType = Type.GetType($"{fieldType}")
                        };
                        myListOfFields.Add(newField);
                    }

                    var table = Tuple.Create(myTypeSignature, myListOfFields);

                    tables.Add(table);
                }
            }

            string MapSqlTypeToCSharpType(string sqlType)
            {
                switch (sqlType)
                {
                    case "bigint":
                        return "System.Int64";
                    case "binary":
                        return "System.Byte[]";
                    case "bit":
                        return "System.Boolean";
                    case "char":
                        return "System.String";
                    case "date":
                        return "System.DateTime";
                    case "datetime":
                        return "System.DateTime";
                    case "decimal":
                        return "System.Decimal";
                    case "float":
                        return "System.Double";
                    case "image":
                        return "System.Byte[]";
                    case "int":
                        return "System.Int32";
                    case "money":
                        return "System.Decimal";
                    case "nchar":
                        return "System.String";
                    case "ntext":
                        return "System.String";
                    case "numeric":
                        return "System.Decimal";
                    case "nvarchar":
                        return "System.String";
                    case "real":
                        return "System.Single";
                    case "rowversion":
                        return "System.Byte";
                    case "smalldatetime":
                        return "System.DateTime";
                    case "smallint":
                        return "System.Int16";
                    case "smallmoney":
                        return "System.Decimal";
                    case "sql_variant":
                        return "System.Object *";
                    case "text":
                        return "System.String";
                    case "time":
                        return "System.TimeSpan";
                    case "timestamp":
                        return "System.Byte";
                    case "tinyint":
                        return "System.Byte";
                    case "uniqueidentifier":
                        return "System.Guid";
                    case "varbinary":
                        return "System.Byte";
                    case "varchar":
                        return "System.String";
                    case "xml":
                        return "System.Xml";
                    default:
                        return "System.String";
                }
            }
        }

        public void GetValuesFromInserts(List<string> inserts, List<Tuple<string, List<string>>> values)
        {
            foreach (var insert in inserts)
            {
                var pFrom = insert.IndexOf("INSERT [dbo].[", StringComparison.Ordinal) + "INSERT [dbo].[".Length;
                var pTo = insert.LastIndexOf("] (", StringComparison.Ordinal);
                var tableName = insert.Substring(pFrom, pTo - pFrom);

                var rawValues = insert.Split('(', ')')[3];

                var separatedvalues = rawValues.Split(",");
                var listOfValues = new List<string>();
                foreach (var separatedvalue in separatedvalues) listOfValues.Add(separatedvalue);

                var insertValues = Tuple.Create(tableName, listOfValues);
                values.Add(insertValues);
            }
        }

        public void CreateInitialiseLookupSqlFiles(List<Tuple<string, List<string>>> values, List<string> files)
        {
            var seed = 500000;
            DirectoryInfo directory = null;
            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}/Lookups"))
                directory = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Lookups");
            foreach (var value in values)
            {
                if (directory == null) continue;
                var fullFileName = $"{directory.FullName}/{seed}.Initialise{value.Item1}.sql";
                var fileExists = Directory.EnumerateFiles($"{directory.FullName}")
                    .Any(f => f.Contains($"Initialise{value.Item1}.sql"));
                if (!fileExists)
                {
                    try
                    {
                        files.Add(fullFileName);
                        File.Create(fullFileName).Dispose();
                        _logger.Information($"{fullFileName} created");
                    }
                    catch (Exception e)
                    {
                        _logger.Information(e.ToString());
                        throw;
                    }

                    seed++;

                    using (var streamWriter = File.AppendText(fullFileName))
                    {
                        try
                        {
                            streamWriter.WriteLine($"-- Initialise {value.Item1} Lookup Table");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
            }
        }

        public void PopulateInitialiseLookupSqlFiles(List<Tuple<string, List<Field>>> tables,
            List<Tuple<string, List<string>>> values, List<string> files, List<string> inserts, List<string> identities)
        {
            foreach (var file in files)
            {
                //get table and values
                var tableToUse = tables.First(tbl => file.Contains(tbl.Item1));
                var table = tableToUse.Item1;
                var columns = tableToUse.Item2;
                var fieldNames = new List<string>();
                var sourceFieldNames = new List<string>();
                var updates = new List<string>();
                foreach (var column in columns)
                {
                    fieldNames.Add($"[{column.FieldName}]");
                    sourceFieldNames.Add($"S.[{column.FieldName}]");
                }

                foreach (var column in columns.Skip(1))
                    updates.Add($"[T].[{column.FieldName}] = [S].[{column.FieldName}]\n  ");
                var finalColumns = string.Join(",", fieldNames);
                var finalSourceColumns = string.Join(",", sourceFieldNames);
                var finalUpdates = string.Join(",", updates);
                var tempTable = $"#{table}";

                // step 1 create temp table to host the data 
                using (var streamWriter = File.AppendText(file))
                {
                    try
                    {
                        streamWriter.WriteLine("\n--step 1 create temp table to host the data");
                        streamWriter.WriteLine(
                            $"IF OBJECT_ID('tempdb..{tempTable}') IS NOT NULL DROP TABLE {tempTable}");
                        streamWriter.WriteLine("GO");
                        streamWriter.WriteLine(
                            $"SELECT TOP 0 * INTO {tempTable} from [dbo].[{table}] SELECT * FROM {tempTable}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                //step 2 insert data into temp table
                var tempInserts = inserts.Where(insert => insert.Contains(table));
                var enumerable = tempInserts as IList<string> ?? tempInserts.ToList();
                var finalInserts = new List<string>();
                foreach (var tempInsert in enumerable)
                {
                    var finalInsert = tempInsert.Replace("INSERT [dbo].[", "INSERT [#");
                    finalInserts.Add(finalInsert);
                }

                using (var streamWriter = File.AppendText(file))
                {
                    try
                    {
                        streamWriter.WriteLine("\n--step 2 insert data into temp table");
                        if (identities.Any(identity => identity.Contains(table)))
                        {
                            streamWriter.WriteLine($"SET IDENTITY_INSERT {tempTable} ON");
                            foreach (var tempInsert in finalInserts) streamWriter.WriteLine($"{tempInsert}");
                            streamWriter.WriteLine($"SET IDENTITY_INSERT {tempTable} OFF");
                        }
                        else
                        {
                            foreach (var tempInsert in finalInserts) streamWriter.WriteLine($"{tempInsert}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                //step 3 if record in temp table doesn't exist in table, insert data in table
                using (var streamWriter = File.AppendText(file))
                {
                    try
                    {
                        streamWriter.WriteLine(
                            "\n--step 3 if record in temp table doesn\'t exist in table, insert data in table");
                        if (identities.Any(identity => identity.Contains(table)))
                            streamWriter.WriteLine($"SET IDENTITY_INSERT [dbo].[{table}] ON");
                        streamWriter.WriteLine($"MERGE [dbo].[{table}] AS T");
                        streamWriter.WriteLine($"USING {tempTable} AS S");
                        streamWriter.WriteLine($"ON T.[{columns[0].FieldName}] = S.[{columns[0].FieldName}]\n");

                        streamWriter.WriteLine("WHEN NOT MATCHED BY TARGET");
                        streamWriter.WriteLine($"  THEN INSERT ( {finalColumns} ) VALUES ( {finalSourceColumns} )\n");

                        streamWriter.WriteLine("WHEN MATCHED \n  THEN UPDATE SET");
                        streamWriter.WriteLine($"  {finalUpdates}");


                        streamWriter.WriteLine("WHEN NOT MATCHED BY SOURCE \n  THEN DELETE;");

                        if (identities.Any(identity => identity.Contains(table)))
                            streamWriter.WriteLine($"SET IDENTITY_INSERT [dbo].[{table}] OFF");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        public void CreatePostDeploymentScript(List<string> files)
        {
            var directory = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Lookups");
            var postDeploymentScript = $"{directory.FullName}/Script.PostDeployment.sql";
            try
            {
                File.Create(postDeploymentScript).Dispose();
                _logger.Information($"{postDeploymentScript} created");
            }
            catch (Exception e)
            {
                _logger.Information(e.ToString());
                throw;
            }

            var header =
                "/*\r\nPost-Deployment Script Template\r\n--------------------------------------------------------------------------------------\r\n This file contains SQL statements that will be appended to the build script.\r\n Use SQLCMD syntax to include a file in the post-deployment script.\r\n Example:      :r .\\myfile.sql\r\n Use SQLCMD syntax to reference a variable in the post-deployment script.\r\n Example:      :setvar TableName MyTable\r\n               SELECT * FROM [$(TableName)]\r\n--------------------------------------------------------------------------------------\r\n*/";


            using (var streamWriter = File.AppendText(postDeploymentScript))
            {
                try
                {
                    streamWriter.WriteLine(header);
                    foreach (var file in files) streamWriter.WriteLine($" :r .\\{Path.GetFileName(file)}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}