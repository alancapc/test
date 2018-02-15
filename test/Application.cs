namespace test
{
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Utilities;

    public class Application : IApplication
    {
        private readonly ILogger _logger;
        private readonly IUtility _utility;
        private readonly IInitialiseLookups _initlInitialiseLookups;
        public static List<string> Inserts { get; set; }
        public static List<Tuple<string, List<string>>> Values { get; set; }
        public static List<Tuple<string, List<Field>>> Tables { get; set; }
        public static List<string> Files { get; set; }
        public static List<object> TableObjects { get; set; }

        public Application(ILogger logger, IUtility utility, IInitialiseLookups initlInitialiseLookups)
        {
            _logger = logger;
            _utility = utility;
            _initlInitialiseLookups = initlInitialiseLookups;
        }

        public void Run()
        {
            Inserts = new List<string>();
            Values = new List<Tuple<string, List<string>>>();
            Tables = new List<Tuple<string, List<Field>>>();
            TableObjects = new List<object>();
            Files = new List<string>();

            GetInsertsFromFile(_initlInitialiseLookups, Inserts);

            _initlInitialiseLookups.GetValuesFromInserts(Inserts, Values);

            _initlInitialiseLookups.GetTableFromInserts(Inserts, Tables);

            _initlInitialiseLookups.CreateInitialiseLookupSqlFiles(Values, Files);

            CreateObjectsFromTables(Tables, TableObjects);

            _utility.WaitUserInput();
        }

        private static void GetInsertsFromFile(IInitialiseLookups initialiseLookups, List<string> inserts)
        {
            IEnumerable<string> dataFile = File.ReadLines(Directory.GetCurrentDirectory() + "/data.txt");

            foreach (var line in dataFile)
            {
                initialiseLookups.GetInsertFromFile(line, Inserts);
            }
        }

        private static void CreateObjectsFromTables(List<Tuple<string, List<Field>>> tables, List<object> tableObjects)
        {
            foreach (var table in Tables)
            {
                var myObject = MyTypeBuilder.CreateNewObject(table.Item1, table.Item2);
                TableObjects.Add(myObject);
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
