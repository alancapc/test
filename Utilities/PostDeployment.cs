using System;
using System.Collections.Generic;

namespace Utilities
{
    public class PostDeployment
    {
        public List<string> Inserts { get; set; } = new List<string>();

        public static List<string> Test { get; set; } = new List<string>();
        public List<Tuple<string, List<string>>> Values { get; set; } = new List<Tuple<string, List<string>>>();
        public List<Tuple<string, List<Field>>> Tables { get; set; } = new List<Tuple<string, List<Field>>>();
        public List<string> Files { get; set; } = new List<string>();
        public List<object> TableObjects { get; set; } = new List<object>();
    }
}