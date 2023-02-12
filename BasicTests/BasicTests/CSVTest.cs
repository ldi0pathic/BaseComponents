using System.Collections.Generic;
using System.ComponentModel;
using Xunit;
using Extensions;
using System.Linq;
using System;

namespace BasicTests
{
    public class CSVTest
    {
        public class CsvTestData
        {
            public string Name;
            public string Value { get; set; }

            public long Index;

            [DisplayName("Ergebnis")]
            public string Daten { get; set;  }
        }

        public IEnumerable<CsvTestData> GetTestData(uint count)
        {
            for (int i = 0; i < count; i++)
                yield return new CsvTestData
                {
                    Name = "Name_" + i,
                    Value = "Value_" + i,
                    Index = i*count,
                    Daten = "Testergebnisse, die sehr aufschlussreich; sind, OderSooo ( " + i +  " )"
                };
        }

        public IDictionary<string,CsvTestData> GetDicTestData(uint count)
        {
            IDictionary<string, CsvTestData> dic = new Dictionary<string, CsvTestData>();
            for (int i = 0; i < count; i++)
                dic.Add ($"Key_{i}",new CsvTestData
                {
                    Name = "Name_" + i,
                    Value = "Value_" + i,
                    Index = i * count,
                    Daten = "Testergebnisse, die sehr aufschlussreich; sind, OderSooo ( " + i + " )"
                });

            return dic;
        }


        [Theory]
        [InlineData(1,",",";",true)]
        [InlineData(42,";",",",false)]  
        [InlineData(5,",/t",",",true)]
        [InlineData(27,"###","_",false)]
        public void TestToCsvExtension(uint count,string s, string a, bool header)
        {
            var data = GetTestData(count);

            var result = data.ToCsv(s, a, header).ToArray();

            Assert.Equal((int)count + (header ? 1 : 0), result.Count());

            if(header)
            {
                Assert.Contains("Name", result[0]);
                Assert.Contains("Value", result[0]);
                Assert.Contains("Index", result[0]);
                Assert.Contains("Ergebnis", result[0]);
            }

            Assert.Contains($"Name_{count-1}", result.Last());
            Assert.Contains($"Value_{count-1}", result.Last());
            Assert.Contains($"{(count-1) * count }", result.Last());
            Assert.Contains($"Testergebnisse, die sehr aufschlussreich; sind, OderSooo ( {count - 1} )".Replace(s,a), result.Last());

        }


        [Theory]
        [InlineData(1, ",", ";", true)]
        [InlineData(42, ";", ",", false)]
        [InlineData(5, ",/t", ",", true)]
        [InlineData(27, "###", "_", false)]
        public void TestToCsvDicExtension(uint count, string s, string a, bool header)
        {
            var data = GetDicTestData(count);

            var result = data.ToCsv(s, a, header).ToArray();

            Assert.Equal((int)count + (header ? 1 : 0), result.Count());

            if (header)
            {
                Assert.Contains("Key", result[0]);
                Assert.Contains("Name", result[0]);
                Assert.Contains("Value", result[0]);
                Assert.Contains("Index", result[0]);
                Assert.Contains("Ergebnis", result[0]);
            }
            Assert.Contains($"Key_{count - 1}", result.Last());
            Assert.Contains($"Name_{count - 1}", result.Last());
            Assert.Contains($"Value_{count - 1}", result.Last());
            Assert.Contains($"{(count - 1) * count }", result.Last());
            Assert.Contains($"Testergebnisse, die sehr aufschlussreich; sind, OderSooo ( {count - 1} )".Replace(s, a), result.Last());


        }
    }
}