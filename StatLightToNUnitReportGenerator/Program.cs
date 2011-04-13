using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StatLightToNUnitReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = new StreamReader(args[0]);
            var outputFile = new StreamWriter(args[1]);
            var parser = new StatLightReportParser(inputFile);
            var formatter = new NUnitTestResultsFormatter(outputFile);
            formatter.Format(parser.Parse());
        }
    }
}
