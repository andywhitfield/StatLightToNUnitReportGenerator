using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatLightToNUnitReportGenerator
{
    public class StatLightResult
    {
        public string Name { get; internal set; }
        public int TotalTests { get; internal set; }
        public int TotalIgnored { get; internal set; }
        public int TotalFailed { get; internal set; }
    }
}
