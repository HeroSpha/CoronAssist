using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class Survey
    {
        public Survey()
        {
            Questions = new HashSet<Question>();
            UserSurveys = new HashSet<AccountSurvey>();
           
        }
        public int SurveyId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Survey description is required")]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public SurveyStatus SurveyStatus { get; set; }
        public double LowUpperBound { get; set; }
        public double MidUpperBound { get; set; }
       
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<AccountSurvey> UserSurveys { get; set; }
        
    }
}
