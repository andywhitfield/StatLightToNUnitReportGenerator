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
    public class NUnitTestResultsFormatterTest
    {
        [Test]
        public void Simple_example_should_format_with_one_nunit_test_result()
        {
            var writer = new StringWriter();
            var formatter = new NUnitTestResultsFormatter(writer);
            formatter.Format(new StatLightReportParser(new StringReader(Resources.StatLightSimpleExample)).Parse());

            Console.WriteLine(writer.ToString());
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<test-results name=""bin\Debug\SomeApplication.Tests.xap"" total=""1"" not-run=""0"" failures=""0"" date=""2011-04-09"" time=""21:06:03"">
  <test-suite type=""Namespace"" executed=""True"" result=""Failure"" success=""False"">
    <results />
  </test-suite>
</test-results>", writer.ToString());
        }
    }
}
