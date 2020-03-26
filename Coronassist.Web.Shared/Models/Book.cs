using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public DateTime BookDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public BookingType BookingType { get; set; }
        public TimeSpan Time { get; set; }
        public string Id { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool IsPaid { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser Patient { get; set; }
    }
}
