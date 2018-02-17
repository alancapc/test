using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;
using static Utilities.CmsDbContext;


namespace Utilities
{
    public class InitialiseLookups : IInitialiseLookups
    {

        private readonly ILogger _logger;
        public InitialiseLookups(ILogger logger)
        {
            _logger = logger;
        }

        public void GetInsertFromFile(string line, List<string> inserts)
        {
            if (line.Contains("INSERT [dbo].[") && line.Contains("] ("))
            {
                inserts.Add(line);
            }
        }

        public void GetTableFromInserts(List<string> inserts, List<Tuple<string, List<Field>>> tables)
        {
            foreach (var insert in inserts)
            {
                int pFrom = insert.IndexOf("INSERT [dbo].[", StringComparison.Ordinal) + "INSERT [dbo].[".Length;
                int pTo = insert.LastIndexOf("] (", StringComparison.Ordinal);
                var tableName = insert.Substring(pFrom, pTo - pFrom);

                if (!tables.Any(tbl => tbl.Item1.Contains(tableName)))
                {
                    var myTypeSignature = tableName;

                    var myListOfFields = new List<Field>();
                    var columns = GetTableColumnsAndTypes(tableName);
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
                int pFrom = insert.IndexOf("INSERT [dbo].[", StringComparison.Ordinal) + "INSERT [dbo].[".Length;
                int pTo = insert.LastIndexOf("] (", StringComparison.Ordinal);
                var tableName = insert.Substring(pFrom, pTo - pFrom);

                var rawValues = insert.Split('(', ')')[3];

                string[] separatedvalues = rawValues.Split(",");
                List<string> listOfValues = new List<string>();
                foreach (var separatedvalue in separatedvalues)
                {
                    listOfValues.Add(separatedvalue);
                }

                var insertValues = Tuple.Create(tableName, listOfValues);
                values.Add(insertValues);
                
            }
        }

        public void CreateInitialiseLookupSqlFiles(List<Tuple<string, List<string>>> values, List<string> files)
        {
            var seed = 500000;
            DirectoryInfo directory = null;
            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}/Lookups"))
            {
                directory = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Lookups");
            }
            foreach (var value in values)
            {
                if (directory == null) continue;
                var fullFileName = $"{directory.FullName}/{seed}.Initialise{value.Item1}.sql";
                bool fileExists = Directory.EnumerateFiles($"{directory.FullName}").Any(f => f.Contains($"Initialise{value.Item1}.sql"));
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

        public void PopulateInitialiseLookupSqlFiles(List<Tuple<string, List<Field>>> tables, List<Tuple<string, List<string>>> values, List<string> files, List<string> inserts)
        {
            foreach (var file in files)
            {
                //get table and values
                var tableToUse = tables.First(tbl => file.Contains(tbl.Item1)).Item1;
                var table = tableToUse;
                var tempTable = $"#{table}";

                // step 1 create temp table to host the data 
                using (var streamWriter = File.AppendText(file))
                {
                    try
                    {
                        streamWriter.WriteLine("\n--step 1 create temp table to host the data");
                        streamWriter.WriteLine($"IF OBJECT_ID('tempdb..{tempTable}') IS NOT NULL DROP TABLE {tempTable}");
                        streamWriter.WriteLine($"GO");
                        streamWriter.WriteLine($"SELECT TOP 0 * INTO {tempTable} from [dbo].[{table}] SELECT * FROM {tempTable}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                //step 2 insert data into temp table
                IEnumerable<string> tempInserts = inserts.Where(insert => insert.Contains(table));
                IList<string> enumerable = tempInserts as IList<string> ?? tempInserts.ToList();
                var finalInserts = new List<string>();
                foreach (string tempInsert in enumerable)
                {
                    var finalInsert = tempInsert.Replace("INSERT [dbo].[", "INSERT [#");
                    finalInserts.Add(finalInsert);
                }
                using (var streamWriter = File.AppendText(file))
                {
                    try
                    {
                        streamWriter.WriteLine("\n--step 2 insert data into temp table");
                        streamWriter.WriteLine($"SET IDENTITY_INSERT {tempTable} ON");
                        foreach (var tempInsert in finalInserts)
                        {
                            streamWriter.WriteLine($"{tempInsert}");
                        }
                        streamWriter.WriteLine($"SET IDENTITY_INSERT {tempTable} OFF");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                /* //step 3 if record in temp table doesn't exist in table, insert data in table
                    SET IDENTITY_INSERT dbo.RouteTypeLookup ON
                    MERGE dbo.RouteTypeLookup AS T
                    USING #RouteTypeLookup AS S
                    ON T.[Key] = S.[Key]
                    WHEN NOT MATCHED BY TARGET 
                      THEN INSERT ([Key], [ShortText], [LongText], [RecordValid]) VALUES (S.[Key], S.ShortText, S.LongText, S.RecordValid);
                    SET IDENTITY_INSERT dbo.RouteTypeLookup OFF
                 */
                using (var streamWriter = File.AppendText(file))
                {
                    try
                    {
                        streamWriter.WriteLine("\n--step 3 if record in temp table doesn\'t exist in table, insert data in table");
                        streamWriter.WriteLine($"SET IDENTITY_INSERT dbo.{table} ON");

                        streamWriter.WriteLine($"MERGE dbo.{table} AS T");
                        streamWriter.WriteLine($"USING {tempTable} AS S");
                        streamWriter.WriteLine($"ON T.<key> = S.<key>");
                        streamWriter.WriteLine($"WHEN NOT MATCHED BY TARGET");
                        streamWriter.WriteLine($"THEN INSERT (" + /* COLUMNS */ ") VALUES (" + /* S.COLUMNS*/ ");" );

                        streamWriter.WriteLine($"SET IDENTITY_INSERT dbo.{table} OFF");
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
            DirectoryInfo directory = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Lookups");
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

            var header = "/*\r\nPost-Deployment Script Template\r\n--------------------------------------------------------------------------------------\r\n This file contains SQL statements that will be appended to the build script.\r\n Use SQLCMD syntax to include a file in the post-deployment script.\r\n Example:      :r .\\myfile.sql\r\n Use SQLCMD syntax to reference a variable in the post-deployment script.\r\n Example:      :setvar TableName MyTable\r\n               SELECT * FROM [$(TableName)]\r\n--------------------------------------------------------------------------------------\r\n*/";


            using (var streamWriter = File.AppendText(postDeploymentScript))
            {
                try
                {
                    streamWriter.WriteLine(header);
                    foreach (var file in files)
                    {
                        streamWriter.WriteLine($" :r .\\{file}");
                    }
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
