using System;
using System.Collections.Generic;

namespace Utilities
{
    public interface IInitialiseLookups
    {
        void GetInsertFromFile(string line, List<string> inserts);

        void GetTableFromInserts(List<string> inserts, List<Tuple<string, List<Field>>> tables);

        void GetValuesFromInserts(List<string> inserts, List<Tuple<string, List<string>>> values);
        void CreateInitialiseLookupSqlFiles(List<Tuple<string, List<string>>> values, List<string> files);
        void PopulateInitialiseLookupSqlFiles(List<Tuple<string, List<Field>>> postDeploymentTables, List<Tuple<string, List<string>>> postDeploymentValues, List<string> deploymentFiles, List<string> postDeploymentFiles);
        void CreatePostDeploymentScript(List<string> postDeploymentFiles);
    }
}
