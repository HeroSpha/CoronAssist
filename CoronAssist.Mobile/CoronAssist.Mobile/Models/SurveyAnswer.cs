using System;
using System.Collections.Generic;
using System.Text;

namespace CoronAssist.Mobile.Models
{
    public class SurveyAnswer
    {
        public int AccountSurveyId { get; set; }
        public virtual AccountSurvey AccountSurvey { get; set; }
        public virtual FlightDetail FlightDetail { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
