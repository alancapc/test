using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Utilities.CmsDbContext;

namespace Utilities
{
    public class InitialiseLookups : IInitialiseLookups
    {
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
                        File.Create(fullFileName).Dispose();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    seed++;

                    using (var streamWriter = File.AppendText(fullFileName))
                    {
                        try
                        {
                            streamWriter.WriteLine($"{value.Item2}");
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
    }
}
