namespace test
{
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Utilities;
    using Interfaces;
    using Utilities.Interfaces;
    using Examples.Threads;

    public class Application : IApplication
    {
        private readonly ILogger _logger;
        private readonly IUtility _utility;
        private readonly IThreading _threading;
        private readonly IInitialiseLookups _initlInitialiseLookups;

        public Application(ILogger logger, IUtility utility, IThreading threading, IInitialiseLookups initlInitialiseLookups)
        {
            _logger = logger;
            _utility = utility;
            _threading = threading;
            _initlInitialiseLookups = initlInitialiseLookups;
        }

        public void Run()
        {
            //_initlInitialiseLookups.GeneratePostDeploymentScripts();
            _threading.ThreadLockExample();
            _utility.WaitUserInput();
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
