using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;

namespace StatLightToNUnitReportGenerator
{
    public class StatLightReportParser
    {
        private readonly TextReader statlightInput;
        private readonly TextWriter nunitOutput;

        public StatLightReportParser(TextReader statlightInput, TextWriter nunitOutput)
        {
            this.statlightInput = statlightInput;
            this.nunitOutput = nunitOutput;
        }

        public StatLightResult Parse()
        {
            var inputXml = XDocument.Load(statlightInput);

            return new StatLightResult
            {
                Name= (string)inputXml.Descendants("tests").First().Attribute("xapFileName"),
                TotalTests = (int)inputXml.Root.Attribute("total"),
                TotalIgnored = (int)inputXml.Root.Attribute("ignored"),
                TotalFailed = (int)inputXml.Root.Attribute("failed"),
                DateRun = DateTime.ParseExact((string)inputXml.Root.Attribute("dateRun"), "yyyy-MM-dd HH:mm:ss", null)
            };
        }
    }
}
