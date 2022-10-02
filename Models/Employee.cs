using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Introduction { get; set; }
        public int LocationId { get; set; }
        public int? PrimaryLanguageId { get; set; }
        public int? SecondaryLanguageId { get; set; }
        public int? EducationLevelId { get; set; }
        public int ReputationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? RateType { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public double? HourRateSalary { get; set; }
        public bool? AvailableForDayShift { get; set; }
        public bool? AvailableForEveningShift { get; set; }
        public bool? AvailableForNightShift { get; set; }
        public bool? AvailableForWeekEnds { get; set; }
        public string Gender { get; set; }
        public string Ssn { get; set; }
        public DateTime DOB { get; set; }
        public bool? CriminalRecord { get; set; }
        public bool? Handicap { get; set; }
        public int? EmployeeStateId { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public int? JobCategoryId { get; set; }
        public string Title { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
