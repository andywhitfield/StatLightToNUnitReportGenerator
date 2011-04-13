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
  <test-suite type=""Namespace"" executed=""True"" result=""Success"" success=""True"">
    <results>
      <test-case name=""SomeApplication.Tests.ATest.My_first_test."" executed=""True"" time=""01:02:03.4567"" result=""Success"" success=""True"" />
    </results>
  </test-suite>
</test-results>", writer.ToString());
        }

        [Test]
        public void One_ignored_test_should_generate_test_suite_result_as_success()
        {
            var writer = new StringWriter();
            var formatter = new NUnitTestResultsFormatter(writer);
            formatter.Format(new StatLightResult("test", DateTime.ParseExact("2011-04-09", "yyyy-MM-dd", null), new[] {
                new StatLightTestResult("test1", true, false, false, "", "", TimeSpan.Zero),
                new StatLightTestResult("test2", false, true, false, "", "", TimeSpan.Zero)
            }));

            Console.WriteLine(writer.ToString());
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<test-results name=""test"" total=""2"" not-run=""1"" failures=""0"" date=""2011-04-09"" time=""00:00:00"">
  <test-suite type=""Namespace"" executed=""True"" result=""Success"" success=""True"">
    <results>
      <test-case name=""test1"" executed=""True"" time=""00:00:00.0000"" result=""Success"" success=""True"" />
      <test-case name=""test2"" executed=""False"" time=""00:00:00.0000"" result=""Ignored"" />
    </results>
  </test-suite>
</test-results>", writer.ToString());
        }

        [Test]
        public void One_failed_test_should_generate_test_suite_result_as_failure()
        {
            var writer = new StringWriter();
            var formatter = new NUnitTestResultsFormatter(writer);
            formatter.Format(new StatLightResult("test", DateTime.ParseExact("2011-04-09", "yyyy-MM-dd", null), new[] {
                new StatLightTestResult("test1", true, false, false, "", "", TimeSpan.Zero),
                new StatLightTestResult("test2", false, true, false, "", "", TimeSpan.Zero),
                new StatLightTestResult("test3", false, false, true, "test-failure", "test-stack", TimeSpan.Zero)
            }));

            Console.WriteLine(writer.ToString());
            Assert.AreEqual(@"<?xml version=""1.0"" encoding=""utf-16""?>
<test-results name=""test"" total=""3"" not-run=""1"" failures=""1"" date=""2011-04-09"" time=""00:00:00"">
  <test-suite type=""Namespace"" executed=""True"" result=""Failure"" success=""False"">
    <results>
      <test-case name=""test1"" executed=""True"" time=""00:00:00.0000"" result=""Success"" success=""True"" />
      <test-case name=""test2"" executed=""False"" time=""00:00:00.0000"" result=""Ignored"" />
      <test-case name=""test3"" executed=""True"" time=""00:00:00.0000"" result=""Failure"" success=""False"">
        <failure>
          <message>test-failure</message>
          <stack-trace><![CDATA[test-stack]]></stack-trace>
        </failure>
      </test-case>
    </results>
  </test-suite>
</test-results>", writer.ToString());
        }
    }
}
