using System;
using System.Collections.Generic;
using System.Text;

namespace CoronAssist.Mobile.Models
{
    public class Answer
    {
       
        public int AnswerId { get; set; }
        public string UserAnswer { get; set; }
        public double Percentage { get; set; }
        public bool IsActive { get; set; } = false;
        public int QuestionId { get; set; }
       
        public virtual Question Question { get; set; }
       

    }
}
