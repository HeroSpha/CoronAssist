﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class DailyReport
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }

}
