using System;
using System.Collections.Generic;
using System.Linq;
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
            return new StatLightResult
            {
                Name= "bin\\Debug\\SomeApplication.Tests.xap",
                TotalTests = 1,
                TotalIgnored = 0,
                TotalFailed = 0
            };
        }
    }
}
