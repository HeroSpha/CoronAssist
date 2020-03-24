using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string SurveyQuestion { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public string Other { get; set; }
        public int SelectedAnswer { get; set; }
        [ForeignKey("SurveyId")]
        public virtual Survey Survey { get; set; }
    }
}
