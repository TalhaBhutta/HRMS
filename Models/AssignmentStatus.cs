using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class AssignmentStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
