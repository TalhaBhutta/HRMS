using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Bank
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string BankName { get; set; }
        public string Acname { get; set; }
        public string Acnumber { get; set; }
        public string Institution { get; set; }
        public string Branch { get; set; }
        public string SignaturePicture { get; set; }
        public bool? IsActive { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
