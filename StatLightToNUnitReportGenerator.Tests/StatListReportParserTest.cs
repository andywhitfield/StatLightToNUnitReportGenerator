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
            var parser = new StatLightReportParser(new StringReader(Resources.StatLightSimpleExample));
            var result = parser.Parse();
            Assert.AreEqual("bin\\Debug\\SomeApplication.Tests.xap", result.Name);
            Assert.AreEqual(1, result.TotalTests);
            Assert.AreEqual(0, result.TotalIgnored);
            Assert.AreEqual(0, result.TotalFailed);

            var tests = result.Tests;
            Assert.AreEqual(1, tests.Count());
            Assert.AreEqual("SomeApplication.Tests.ATest.My_first_test.", tests.ElementAt(0).Name);
            Assert.True(tests.ElementAt(0).Passed);
            Assert.AreEqual(TimeSpan.Parse("01:02:03.45678"), tests.ElementAt(0).ExecutionTime);
        }

        [Test]
        public void Given_ignored_tests_should_parse_as_ignored_state_and_not_success_or_fail()
        {
            var parser = new StatLightReportParser(new StringReader(Resources.StatLightExample));
            var result = parser.Parse();
            Assert.AreEqual("bin\\Debug\\SomeApplication.Tests.xap", result.Name);
            Assert.AreEqual(6, result.TotalTests);
            Assert.AreEqual(1, result.TotalIgnored);
            Assert.AreEqual(2, result.TotalFailed);

            var tests = result.Tests;
            Assert.AreEqual(6, tests.Count());
            var ignored = tests.FirstOrDefault(x => x.Name == "SomeApplication.Tests.This_one_is_disabled.");
            Assert.IsNotNull(ignored, "Didn't find test!");
            Assert.True(ignored.Ignored);
            Assert.False(ignored.Passed);
            Assert.False(ignored.Failed);
        }

        [Test]
        public void Given_failed_tests_should_parse_as_failed_state_and_populate_message_and_stack_trace()
        {
            var parser = new StatLightReportParser(new StringReader(Resources.StatLightExample));
            var result = parser.Parse();
            Assert.AreEqual("bin\\Debug\\SomeApplication.Tests.xap", result.Name);
            Assert.AreEqual(6, result.TotalTests);
            Assert.AreEqual(1, result.TotalIgnored);
            Assert.AreEqual(2, result.TotalFailed);

            var tests = result.Tests;
            Assert.AreEqual(6, tests.Count());
            var failed = tests.FirstOrDefault(x => x.Name == "SomeApplication.Tests.Util.My_second_test.Assert_failure.");
            Assert.IsNotNull(failed, "Didn't find test!");
            Assert.True(failed.Failed);
            Assert.False(failed.Ignored);
            Assert.False(failed.Passed);
            Assert.AreEqual("Assert.Fail failed. test", failed.FailureMessage);
            Assert.AreEqual("\n" +
"          at Microsoft.VisualStudio.TestTools.UnitTesting.Assert.HandleFail(String assertionName, String message, Object[] parameters)\n" +
"          at Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail(String message)\n" +
"          at SomeApplication.Tests.Util.My_second_test.Assert_failure()", failed.FailureStackTrace);
        }
    }
}
