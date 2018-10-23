using Utilities.Interfaces;

namespace Utilities.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.Options;

    public class PostDeployment
    {
        public IEnumerable<string> DataFile { get; set; } = File.ReadLines(Directory.GetCurrentDirectory() + "/data.txt");
        public List<string> Inserts { get; set; } = new List<string>();
        public List<string> Identities { get; set; } = new List<string>();

        public List<Tuple<string, List<string>>> Values { get; set; } = new List<Tuple<string, List<string>>>();
        public List<Tuple<string, List<Field>>> Tables { get; set; } = new List<Tuple<string, List<Field>>>();
        public List<string> Files { get; set; } = new List<string>();
        public List<object> TableObjects { get; set; } = new List<object>();
    }
}