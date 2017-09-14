using System.Collections;

namespace test
{
    public partial class CSharpTwo
    {
        public class DaysOfTheWeek : IEnumerable
        {
            private string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            public IEnumerator GetEnumerator() => days.GetEnumerator();
        }
    }
}