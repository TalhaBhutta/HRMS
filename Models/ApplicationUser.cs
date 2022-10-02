using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
	public class ApplicationUser : IdentityUser
	{
		//[PersonalData]
		//public string Name { get; set; }
		//[PersonalData]
		//public DateTime DOB { get; set; }
		//      [Column("Id", Order = 0)]
		//      public override string Id { get; set; }

		//      [Column("Discriminator", Order = 23)]
		//      public int Discriminator { get; set; }

		//      [Column("UserName", Order =24)]
		//public override string UserName { get; set; }

		//[Column("NormalizedUserName", Order = 1)]
		//public override string NormalizedUserName { get; set; }

		//[Column("Email", Order = 2)]
		//public override string Email	{ get; set; }

		//[Column("NormalizedEmail", Order = 3)]
		//public override string NormalizedEmail { get; set; }

		//[Column("EmailConfirmed", Order = 4)]
		//public override bool EmailConfirmed { get; set; }

		//[Column("PasswordHash", Order = 5)]
		//public override string PasswordHash { get; set; }

		//[Column("SecurityStamp", Order = 6)]
		//public override string SecurityStamp { get; set; }

		//[Column("ConcurrencyStamp", Order = 7)]
		//public override string ConcurrencyStamp { get; set; }

		//[Column("PhoneNumber", Order = 8)]
		//public override string PhoneNumber { get; set; }

		//[Column("PhoneNumberConfirmed", Order = 9)]
		//public override bool PhoneNumberConfirmed { get; set; }

		//[Column("TwoFactorEnabled", Order = 10)]
		//public override bool TwoFactorEnabled { get; set; }

		//[Column("LockoutEnd", Order = 11)]
		//public override DateTimeOffset? LockoutEnd { get; set; }

		//[Column("LockoutEnabled", Order = 12)]
		//public override bool LockoutEnabled { get; set; }

		//[Column("AccessFailedCount", Order = 13)]
		//public override int AccessFailedCount { get; set; }


		//[Column("LanguageId", Order = 14)]
		//public int? LanguageId { get; set; }

		////[Column("LocationId", Order = 15)]
		//public int? LocationId { get; set; }

		////[Column("MobileInstalled", Order = 16)]
		//public byte? MobileInstalled { get; set; }

		////[Column("ReputationId", Order = 17)]
		//public int? ReputationId { get; set; }

		////[Column("IsUsedByThirdParty", Order = 18)]
		//public byte? IsUsedByThirdParty { get; set; }
		//[PersonalData]
		public DateTimeOffset LastLoginDate { get; set; }

		//[Column("CreatedOn", Order = 19)]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTimeOffset CreatedOn { get; set; }

		//[Column("CreatedBy", Order = 20)]
		public string? CreatedBy { get; set; }

		//[Column("ModifiedOn", Order = 21)]
		public DateTimeOffset? ModifiedOn { get; set; }

		//[Column("ModifiedBy", Order = 22)]
		public string? ModifiedBy { get; set; }

		//public bool IsAdmin { get; set; }

		public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
		public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
		//public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
		//public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
	}
}
