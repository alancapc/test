namespace Utilities.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CreateLookupObject
    {
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

            foreach (var prop in props)
            {
                var propValue = prop.GetValue(myObject, null);
                Console.WriteLine($"  Property Name: {prop.Name}\t Property Type: {prop.PropertyType}\n  Property Value: {propValue}");
            }
        }
    }
}
