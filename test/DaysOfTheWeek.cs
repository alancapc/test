using System.Collections;

namespace test
{
    public partial class CSharpTwo
    {
        public class DaysOfTheWeek : IEnumerable
        {
            private readonly string[] _days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            public IEnumerator GetEnumerator() => _days.GetEnumerator();
        }
    }
}