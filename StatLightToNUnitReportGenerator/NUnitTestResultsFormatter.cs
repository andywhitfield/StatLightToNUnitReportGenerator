using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace StatLightToNUnitReportGenerator
{
    public class NUnitTestResultsFormatter
    {
        private readonly TextWriter writer;
        public NUnitTestResultsFormatter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Format(StatLightResult result)
        {
            XElement xml = new XElement("test-results",
                new XAttribute("name", result.Name),
                new XAttribute("total", result.TotalTests),
                new XAttribute("not-run", result.TotalIgnored),
                new XAttribute("failures", result.TotalFailed),
                new XAttribute("date", result.DateRun.ToString("yyyy-MM-dd")),
                new XAttribute("time", result.DateRun.ToString("HH:mm:ss")),
                new XElement("test-suite",
                    new XAttribute("type", "Namespace"),
                    new XAttribute("executed", "True"),
                    new XAttribute("result", "Failure"),
                    new XAttribute("success", "False"),
                    new XElement("results",
                        // select all test cases...
                        null)
                    )
                );

            xml.Save(writer);
        }
    }
}
