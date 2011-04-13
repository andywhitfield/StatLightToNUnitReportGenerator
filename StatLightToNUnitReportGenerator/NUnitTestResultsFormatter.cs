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
                    new XAttribute("result", result.TotalFailed > 0 ? "Failure" : "Success"),
                    new XAttribute("success", result.TotalFailed > 0 ? "False" : "True"),
                    new XElement("results",
                        from r in result.Tests
                        select new XElement("test-case",
                            new XAttribute("name", r.Name),
                            new XAttribute("executed", r.Ignored ? "False" : "True"),
                            new XAttribute("result", r.Ignored ? "Ignored" : r.Failed ? "Failure" : "Success"),
                            new XAttribute("success", r.Passed ? "True" : "False"),
                            new XAttribute("time", r.ExecutionTime.ToString(@"hh\:mm\:ss\.FFFFFF"))))
                    )
                );

            xml.Save(writer);
        }
    }
}
