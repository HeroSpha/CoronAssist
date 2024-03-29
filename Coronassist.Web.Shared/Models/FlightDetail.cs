﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class FlightDetail
    {
        public int FlightDetailId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string FlightNumber { get; set; }
        public int SurveyAnswerId { get; set; }
        public virtual SurveyAnswer SurveyAnswer { get; set; }
        
    }
}
