
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronAssist.Mobile.Models
{
    public class Survey
    {
        public Survey()
        {
            Questions = new HashSet<Question>();
            UserSurveys = new HashSet<AccountSurvey>();
           
        }
        public int SurveyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public SurveyStatus SurveyStatus { get; set; }
        public double LowUpperBound { get; set; }
        public double MidUpperBound { get; set; }
       
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<AccountSurvey> UserSurveys { get; set; }
        
    }
}
