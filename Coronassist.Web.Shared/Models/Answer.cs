using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string UserAnswer { get; set; }
        public double Percentage { get; set; }
        public bool IsActive { get; set; } = false;
        public AnswerType AnswerType { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
       

    }
}
