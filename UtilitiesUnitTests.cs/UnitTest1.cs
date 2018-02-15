using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using static Utilities.InitialiseLookups;

namespace UtilitiesUnitTests.cs
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1Async()
        {
            //arrange
            var insert = new List<string>(){ "INSERT [dbo].[lkpEntitlementProof] ([pkiKey], [szShortText], [szLongText], [bRecordValid]) VALUES(0, N'Unknown', N'Unknown', 1)" };
            var values = new List<Tuple<string, List<string>>>();

            //act
            var initialiseLookups = new InitialiseLookups();
            initialiseLookups.GetValuesFromInserts(insert, values);

            //assert
            var testValue = new List<Tuple<string, List<string>>>();
            var testTable = "test1";
            var testRowValues = new List<string>() { "value1" };
            var testTuple = Tuple.Create(testTable, testRowValues);
            testValue.Add(testTuple);

            var expectedValues = new List<Tuple<string, List<string>>>();
            var table = "test1";
            var rowValues = new List<string>() { "value1" };
            var tuple = Tuple.Create(table, rowValues);
            expectedValues.Add(tuple);
            CollectionAssert.AreEqual(expectedValues.Select(t => t.Item2).ToList(), testValue.Select(t => t.Item2).ToList());
        }
    }
}
