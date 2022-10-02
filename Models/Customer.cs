using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int? LocationId { get; set; }
        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        //public virtual AspUser User { get; set; }
    }
}
