using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Timesheet
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int WeekPeriod { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public decimal NumHrs { get; set; }
        public decimal TotalAmount { get; set; }
        public int CurrencyId { get; set; }
        public bool IsPaid { get; set; }
        public string EmployeeRemarks { get; set; }
        public bool Locked { get; set; }
        public string AdminRemarks { get; set; }
        public byte ShiftSubmissionStateId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
