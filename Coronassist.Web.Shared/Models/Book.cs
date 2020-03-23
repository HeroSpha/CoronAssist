using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class Book
    {
        public int AccountBookId { get; set; }
        public DateTime BookDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public string Id { get; set; }
        public bool IsPaid { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public string PatientId { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser Doctor { get; set; }
    }
}
