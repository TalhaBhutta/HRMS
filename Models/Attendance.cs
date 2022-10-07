using HRMS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HRMS.Models
{
    public partial class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentID { get; set; }
        public string MarkAttendanceBY { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string IsLate { get; set; }
        public string IsHalfDay { get; set; }
        public DateTimeOffset? CheckInTime { get; set; }
        public DateTimeOffset? CheckOutTime { get; set; }
        public int? AssignmentId { get; set; }
        public int? TimesheetId { get; set; }
        public double? ManualCheckInLat { get; set; }
        public double? ManualCheckInLong { get; set; }
        public double? ManualCheckOutLat { get; set; }
        public double? ManualCheckOutLong { get; set; }
        public bool? ManualOutOfRangeCheckIn { get; set; }
        public bool? ManualOutOfRangeCheckOut { get; set; }
        public double? CheckInLat { get; set; }
        public double? CheckInLong { get; set; }
        public double? CheckOutLat { get; set; }
        public double? CheckOutLong { get; set; }
        public bool? OutOfRangeCheckIn { get; set; }
        public bool? OutOfRangeCheckOut { get; set; }
        public bool? NoShow { get; set; }
        public int? InvoiceId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string WorkingFrom { get; set; }
        public string multi_date { get; set; }
        public DateTimeOffset? Day { get; set; }
    }

}
