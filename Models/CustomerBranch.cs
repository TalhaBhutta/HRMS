using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class CustomerBranch
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
