﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace StatLightToNUnitReportGenerator
{
    public class StatLightTestResult
    {
        public StatLightTestResult(XElement xml)
        {
            Name = xml.Attribute("name").Value;
            var resultType = (string)xml.Attribute("resulttype");
            switch (resultType)
            {
                case "Passed":
                    Passed = true;
                    break;
            }
            ExecutionTime = TimeSpan.Parse((string)xml.Attribute("timeToComplete"));
        }

        public string Name { get; private set; }
        public bool Passed { get; private set; }
        public bool Ignored { get; private set; }
        public bool Failed { get; private set; }
        public string FailureMessage { get; private set; }
        public string FailureStackTrace { get; private set; }
        public TimeSpan ExecutionTime { get; private set; }
    }
}