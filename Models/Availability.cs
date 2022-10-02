using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Availability
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTimeOffset? Day { get; set; }
        public TimeSpan? StartingTime { get; set; }
        public TimeSpan? EndingTime { get; set; }
        public bool? IsReccurring { get; set; }
        public int? Recurrances { get; set; }
        public bool? DayShift { get; set; }
        public bool? EveningShift { get; set; }
        public bool? NightShift { get; set; }
        public bool? WeekEndShift { get; set; }
        public bool? IsBooked { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
