using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class Location
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CountryId { get; set; }
        public string StateProvinceId { get; set; }
        public string CityId { get; set; }
        public string Address { get; set; }
        //public string Unit { get; set; }
        public string PostalCode { get; set; }
        public string TimeZone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
