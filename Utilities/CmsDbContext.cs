using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Utilities
{
    public class CmsDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Translink2014_Dev;Integrated Security=True;Application Name=CMS");
        }

        public static List<Tuple<string, string>> GetTableColumnsAndTypes(string tableName)
        {
            using (var context = new CmsDbContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"SELECT 
                                                c.name 'column',
                                                t.Name 'type',
                                                c.max_length 'length',
                                                c.precision ,
                                                c.scale ,
                                                c.is_nullable,
                                                ISNULL(i.is_primary_key, 0) 'primaryKey'
                                            FROM    
                                                sys.columns c
                                            INNER JOIN 
                                                sys.types t ON c.user_type_id = t.user_type_id
                                            LEFT OUTER JOIN 
                                                sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                                            LEFT OUTER JOIN 
                                                sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
                                            WHERE
                                                c.object_id = OBJECT_ID(@tableName)
                                            ";
                    var name = new SqlParameter("@tableName", tableName);
                    command.Parameters.Add(name);
                    context.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        var tableColumns = new List<Tuple<string, string>>();
                        while (result.Read())
                        {
                            var column = Tuple.Create(result.GetString(0), result.GetString(1));
                            tableColumns.Add(column);
                        }
                        result.Close();
                        return tableColumns;
                    }
                }
            }

        }
    }
}