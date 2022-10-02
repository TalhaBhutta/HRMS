using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Assignment
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public int EmployeeId { get; set; }
        public DateTimeOffset? StartingTime { get; set; }
        public DateTimeOffset? EndingTime { get; set; }
        public double? HourlyRate { get; set; }
        public double? NumHrs { get; set; }
        public string TimeZone { get; set; }
        public bool? IsReccurring { get; set; }
        public int? Recurrances { get; set; }
        public string EmployeeRemarks { get; set; }
        public int? EmpStatusId { get; set; }
        public int? CustStatusId { get; set; }
        public int? AssignmentStatusId { get; set; }
        public string EditRemarks { get; set; }
        public string AdminRemarks { get; set; }
        public byte? SubmitStatus { get; set; }
        public byte? ApproveStatus { get; set; }
        public DateTimeOffset? ApprovedDateTime { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
