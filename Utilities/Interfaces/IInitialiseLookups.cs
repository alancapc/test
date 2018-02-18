namespace Utilities.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IInitialiseLookups
    {
        void GeneratePostDeploymentScripts();

        void GetInsertsFromFile(IEnumerable<string> dataFile, List<string> inserts);
        void GetInsertFromDataFile(string line, List<string> inserts);
        void GetIdentitiesFromDataFile(IEnumerable<string> dataFile, List<string> identities);

        void GetTableFromInserts(List<string> inserts, List<Tuple<string, List<Field>>> tables);
        void GetValuesFromInserts(List<string> inserts, List<Tuple<string, List<string>>> values);

        void CreateInitialiseLookupSqlFiles(List<Tuple<string, List<string>>> values, List<string> files);
        void PopulateInitialiseLookupSqlFiles(List<Tuple<string, List<Field>>> tables, List<Tuple<string, List<string>>> values, List<string> files, List<string> inserts, List<string> identities);

        void CreatePostDeploymentScript(List<string> files);
    }
}
