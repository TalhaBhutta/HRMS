using System;
using System.Collections.Generic;

#nullable disable

namespace HRMS.Models
{
    public partial class CreditCard
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? AccountId { get; set; }
        public int? CreditCardTypeId { get; set; }
        public string AccountNumber { get; set; }
        public string First4Digits { get; set; }
        public string Last4Digits { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
        public bool? Avsverified { get; set; }
        public string Name { get; set; }
        public string UserIpaddress { get; set; }
        public string Zip { get; set; }
        public bool? AuthenticationSubmitted { get; set; }
        public string Csnote { get; set; }
        public bool? Valid { get; set; }
        public string Cavv { get; set; }
        public string Eciflag { get; set; }
        public string Xid { get; set; }
        public decimal? CardLimit { get; set; }
        public bool? ExpiryEmail1Sent { get; set; }
        public bool? ExpiryEmail2Sent { get; set; }
        public int? Category { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
