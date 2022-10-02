using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Shift
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? BranchId { get; set; }
        public DateTimeOffset? Day { get; set; }
        public DateTimeOffset? StartingTime { get; set; }
        public DateTimeOffset? EndingTime { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? IncreasedHourlyRate { get; set; }
        public decimal? Premium { get; set; }
        public decimal? NightPremium { get; set; }
        public decimal? HolidayPremium { get; set; }
        public decimal? NumHrs { get; set; }
        public bool? DayShift { get; set; }
        public bool? EveningShift { get; set; }
        public bool? NightShift { get; set; }
        public bool? WeekEndShift { get; set; }
        public bool? IsReccurring { get; set; }
        public byte PriorityRankId { get; set; }
        public int ShiftStateId { get; set; }
        public int? Recurrances { get; set; }
        public bool? Tips { get; set; }
        public int? ShiftGroupId { get; set; }
        public string ManagerName { get; set; }
        public int? ManagerId { get; set; }
        public string EmployeeRemarks { get; set; }
        public string EditRemarks { get; set; }
        public string Description { get; set; }
        public string JobTitle { get; set; }
        public int? WorkCatId { get; set; }
        public int? DepartmentId { get; set; }
        public string AdminRemarks { get; set; }
        public string AdditionalTasks { get; set; }
        public bool? ApproveStatus { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
