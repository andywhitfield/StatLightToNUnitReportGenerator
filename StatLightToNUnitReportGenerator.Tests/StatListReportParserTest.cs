using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using StatLightToNUnitReportGenerator.Tests.Properties;

namespace StatLightToNUnitReportGenerator.Tests
{
    [TestFixture]
    public class StatListReportParserTest
    {
        [Test]
        public void Should_parse_simple_sample_into_statlight_object_model()
        {
            var outputReader = new StringWriter();
            var parser = new StatLightReportParser(new StringReader(Resources.StatLightSimpleExample), outputReader);
            var result = parser.Parse();
            Assert.AreEqual("bin\\Debug\\SomeApplication.Tests.xap", result.Name);
            Assert.AreEqual(1, result.TotalTests);
            Assert.AreEqual(0, result.TotalIgnored);
            Assert.AreEqual(0, result.TotalFailed);
        }
    }
}
