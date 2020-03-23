using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class SurveyAnswer
    {
        [Key]
        public int SurveyAnswerId { get; set; }
       
        public int AccountSurveyId { get; set; }
        [ForeignKey("AccountSurveyId")]
        public virtual AccountSurvey AccountSurvey { get; set; }
        
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
