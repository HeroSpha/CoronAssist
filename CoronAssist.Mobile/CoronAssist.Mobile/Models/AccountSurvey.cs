
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronAssist.Mobile.Models
{
    public class AccountSurvey
    {
        public AccountSurvey()
        {
            QuestionAnswers = new HashSet<SurveyAnswer>();
        }
        public int AccountSurveyId { get; set; }
        public int SurveyAnswerId { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string MedicalAidNumber { get; set; }
        public int Age { get; set; }
        public int SurveyId { get; set; }
        public double Risk { get; set; }
        public DateTime SurveyDate { get; set; }
        public UserSurveyStatus UserSurveyStatus { get; set; }
        public string Id { get; set; }
      
        public virtual ApplicationUser Account { get; set; }
        
        public virtual Survey Survey { get; set; }
        public virtual ICollection<SurveyAnswer> QuestionAnswers { get; set; }
    }
}
