using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using LibraryApp.Models;

namespace LibraryApp.Test
{
    public static class PICTParser
    {
        // private static readonly string PICTParserPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PICTResult.txt");

        private static readonly string PICTParserPath = "C:\\Users\\makig\\Source\\Repos\\test-2-grupa-a-Mateja4329\\LibraryApp.Test\\PICTResult.txt";

        public static IEnumerable<TestCaseData> GetTestCase()
        {
            using(StreamReader sr = new StreamReader(PICTParserPath))
            {
                sr.ReadLine();

                string line;
                while(!sr.EndOfStream)
                {
                    line = sr.ReadLine();

                    string[] parts = line.Split('\t');

                    double bookPrice = double.Parse(parts[0]);
                    int numOfPurchasesInTheLastMonth = int.Parse(parts[1]);
                    bool penalty = bool.Parse(parts[2]);
                    ActivityFrequency activityFrequency = (ActivityFrequency)Enum.Parse(typeof(ActivityFrequency), parts[3]);
                    int discount = int.Parse(parts[4]);

                    yield return new TestCaseData(bookPrice, numOfPurchasesInTheLastMonth, penalty, activityFrequency, discount);
                }
            }
        }
    }
}
