using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StatLightToNUnitReportGenerator
{
    public class StatLightResult
    {
        public string Name { get; internal set; }
        public DateTime DateRun { get; internal set; }
        public int TotalTests { get; internal set; }
        public int TotalIgnored { get; internal set; }
        public int TotalFailed { get; internal set; }
        public IEnumerable<StatLightTestResult> Tests { get; private set; }

        public StatLightResult() { Tests = new List<StatLightTestResult>(); }
        public StatLightResult(IEnumerable<XElement> testNodes) : this()
        {
            Tests = testNodes.Select(x => new StatLightTestResult(x));
        }
    }
}
