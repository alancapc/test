namespace test
{
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Utilities;
    using Interfaces;
    public class Application : IApplication
    {
        private readonly ILogger _logger;
        private readonly IUtility _utility;
        private readonly IInitialiseLookups _initlInitialiseLookups;

        public Application(ILogger logger, IUtility utility, IInitialiseLookups initlInitialiseLookups)
        {
            _logger = logger;
            _utility = utility;
            _initlInitialiseLookups = initlInitialiseLookups;
        }

        public void Run()
        {
            var postDeployment = new PostDeployment();

            GetInsertsFromFile(_initlInitialiseLookups, postDeployment.Inserts);

            _initlInitialiseLookups.GetValuesFromInserts(postDeployment.Inserts, postDeployment.Values);

            _initlInitialiseLookups.GetTableFromInserts(postDeployment.Inserts, postDeployment.Tables);

            _initlInitialiseLookups.CreateInitialiseLookupSqlFiles(postDeployment.Values, postDeployment.Files);

            _initlInitialiseLookups.PopulateInitialiseLookupSqlFiles(postDeployment.Tables, postDeployment.Values,
                postDeployment.Files, postDeployment.Inserts);

            _initlInitialiseLookups.CreatePostDeploymentScript(postDeployment.Files);
            //CreateObjectsFromTables(postDeployment.Tables, postDeployment.TableObjects);

            _utility.WaitUserInput();
        }

        private static void GetInsertsFromFile(IInitialiseLookups initialiseLookups, List<string> inserts)
        {
            IEnumerable<string> dataFile = File.ReadLines(Directory.GetCurrentDirectory() + "/data.txt");

            foreach (var line in dataFile)
            {
                initialiseLookups.GetInsertFromFile(line, inserts);
            }
        }

        private static void CreateObjectsFromTables(List<Tuple<string, List<Field>>> tables, List<object> tableObjects)
        {
            foreach (var table in tables)
            {
                var myObject = MyTypeBuilder.CreateNewObject(table.Item1, table.Item2);
                tableObjects.Add(myObject);
            }
        }

        private static void ListObjectPropertiesDetails(object myObject)
        {
            IList<PropertyInfo> props = new List<PropertyInfo>(myObject.GetType().GetProperties());

            Console.WriteLine($"Type: {myObject.GetType()}");

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(myObject, null);
                Console.WriteLine($"  Property Name: {prop.Name}\t Property Type: {prop.PropertyType}\n  Property Value: {propValue}");
            }
        }
    }
}
