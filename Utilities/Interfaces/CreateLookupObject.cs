using System;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities.Interfaces
{
    public class CreateLookupObject
    {
        private static void CreateObjectsFromTables(IEnumerable<Tuple<string, List<Field>>> tables,
            ICollection<object> tableObjects)
        {
            foreach ((string myTypeSignature, List<Field> fields) in tables)
            {
                var myObject = MyTypeBuilder.CreateNewObject(myTypeSignature, fields);
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
                Console.WriteLine(
                    $"  Property Name: {prop.Name}\t Property Type: {prop.PropertyType}\n  Property Value: {propValue}");
            }
        }
    }
}