using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Education
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Program { get; set; }
        public string Institution { get; set; }
        public string FieldofStudy { get; set; }
        public int? EducationLevelId { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
