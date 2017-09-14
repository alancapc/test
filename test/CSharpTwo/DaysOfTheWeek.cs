using System.Collections;

namespace test.CSharpTwo
{
    public class DaysOfTheWeek : IEnumerable
    {
        private readonly string[] _days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

        public IEnumerator GetEnumerator() => _days.GetEnumerator();
    }
}