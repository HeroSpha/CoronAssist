using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class AccountSurvey
    {
        public AccountSurvey()
        {
            QuestionAnswers = new HashSet<SurveyAnswer>();
        }
        [Key]
        public int AccountSurveyId { get; set; }
        public int SurveyId { get; set; }
        public double Risk { get; set; }
        public DateTime SurveyDate { get; set; }
        public UserSurveyStatus UserSurveyStatus { get; set; }
        public string Id { get; set; }
        [ForeignKey("Id")] 
        public virtual ApplicationUser Account { get; set; }
        [ForeignKey("SurveyId")]
        public virtual Survey Survey { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string MedicalNumber { get; set; }
        public int Age { get; set; }
        public virtual ICollection<SurveyAnswer> QuestionAnswers { get; set; }
    }
}
