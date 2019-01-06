using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Examples.Json
{
    class Json : IJson
    {
        public void SerialiseJson()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.DateTime
            };

            var s = "['2016-05-10T13:51:20Z', '2016-05-10T13:51:20+00:00']";

            var array = JArray.Parse(s);
            foreach (var item in array)
            {
                var itemValue = item.Value<string>();
                Console.WriteLine(itemValue);
                Console.WriteLine(itemValue.GetType());
            }
        }

        public void DeserialiseJson()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var obj = JsonConvert.DeserializeObject<NewDate>("{ 'MyDate': '2016-03-30T07:02:00+07:00'}",
                new JsonSerializerSettings()
                {
                    DateParseHandling = DateParseHandling.None
                });
            Console.WriteLine(obj.MyDate);
            Console.WriteLine(obj.MyDate.GetType());
            Console.WriteLine("CC: " + CultureInfo.CurrentCulture);
            Console.WriteLine("CUC:" + CultureInfo.CurrentUICulture);
        }
    }

    public class NewDate
    {
        public DateTime MyDate = DateTime.Now;
    }
}
