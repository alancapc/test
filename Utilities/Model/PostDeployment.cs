using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities.Model
{
    public class PostDeployment
    {
        public IEnumerable<string> DataFile { get; } =
            File.ReadLines(Directory.GetCurrentDirectory() + "/data.txt");

        public List<string> Inserts { get; } = new List<string>();
        public List<string> Identities { get; } = new List<string>();

        public List<Tuple<string, List<string>>> Values { get; } = new List<Tuple<string, List<string>>>();
        public List<Tuple<string, List<Field>>> Tables { get; } = new List<Tuple<string, List<Field>>>();
        public List<string> Files { get; } = new List<string>();
        public List<object> TableObjects { get; set; } = new List<object>();
    }
}