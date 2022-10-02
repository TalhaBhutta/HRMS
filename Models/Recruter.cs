using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Recruter
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? LocationId { get; set; }
        public string ContactTitle { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public DateTime? DOB { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
