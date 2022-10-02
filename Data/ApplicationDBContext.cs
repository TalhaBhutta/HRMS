using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HRMS.Models;
using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using Microsoft.AspNetCore.Http;
//using System.Data.Entity;

namespace HRMS.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        //private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<AssignmentStatus> AssignmentStatus { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Availability> Availability { get; set; }
        public DbSet<Bank> Bank { get; set; }
        //public DbSet<Branch> Branch { get; set; }
        //public DbSet<City> City { get; set; }
        //public DbSet<Company> Company { get; set; }
        //public DbSet<Continent> Continent { get; set; }
        //public DbSet<Country> Country { get; set; }
        public DbSet<CreditCard> CreditCard { get; set; }
        //public DbSet<CreditCardType> CreditCardType { get; set; }
        //public DbSet<Currency> Currency { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerBranch> CustomerBranch { get; set; }
        //public DbSet<CustomerShiftStatus> CustomerShiftStatus { get; set; }
        //public DbSet<Department> Department { get; set; }
        public DbSet<Education> Education { get; set; }
        //public DbSet<EducationLevel> EducationLevel { get; set; }
        //public DbSet<EmailCategory> EmailCategory { get; set; }
        //public DbSet<EmailSubCategory> EmailSubCategory { get; set; }
        //public DbSet<EmailSubCategoryTemplate> EmailSubCategoryTemplate { get; set; }
        //public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<Employee> Employee { get; set; }
        //public DbSet<EmployeeShiftState> EmployeeShiftState { get; set; }
        //public DbSet<ErrorMessageCategory> ErrorMessageCategory { get; set; }
        //public DbSet<ErrorMessageSubCategory> ErrorMessageSubCategory { get; set; }
        public DbSet<Experience> Experience { get; set; }
        //public DbSet<Invoice> Invoice { get; set; }
        //public DbSet<JobCategory> JobCategory { get; set; }
        //public DbSet<JobType> JobType { get; set; }
        public DbSet<Location> Locations { get; set; }
        //public DbSet<PriorityRank> PriorityRank { get; set; }
        //public DbSet<RateType> RateType { get; set; }
        public DbSet<Recruter> Recruter { get; set; }
        //public DbSet<ReferralBonu> ReferralBonus { get; set; }
        //public DbSet<Referral> Referrals { get; set; }
        //public DbSet<Review> Review { get; set; }
        //public DbSet<Role> Role { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<ShiftState> ShiftState { get; set; }
        public DbSet<ShiftSubmissionState> ShiftSubmissionState { get; set; }
        //public DbSet<StateProvince> StateProvince { get; set; }
        public DbSet<Timesheet> Timesheet { get; set; }
        //public DbSet<TransactionState> TransactionState { get; set; }
        //public DbSet<TransactionType> TransactionType { get; set; }


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //    optionsBuilder
            //    .EnableSensitiveDataLogging()
            //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("ApplicationDBContext");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        //public DbSet<Employee> Student { get; set; }
        //public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Course>().ToTable("Course");
            //modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            //modelBuilder.Entity<Employee>().ToTable("Student");
            base.OnModelCreating(modelBuilder);

            /*-------------------------------Assignment TABLE----------------------------*/
            /*-------------------------------AssignmentStatus TABLE----------------------------*/
            /*-------------------------------Attendance TABLE----------------------------*/
            /*-------------------------------Bank TABLE----------------------------*/
            modelBuilder.Entity<Bank>(b =>
            {
                b.Property(e => e.IsActive)
                .HasDefaultValue(true);

                b.Property<DateTimeOffset>("CreatedOn")
                    .HasColumnType("datetimeoffset(7)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");
            });
            //modelBuilder.Entity<Bank>()
            //.HasOne(b => b.employee)
            //.WithMany(p => p.Banks)
            //.HasPrincipalKey(x => x.UserId)
            //.HasForeignKey(x => x.UserId);

            /*-------------------------------Branch TABLE----------------------------*/
            /*-------------------------------City TABLE----------------------------*/
            /*-------------------------------Company TABLE----------------------------*/
            /*-------------------------------Continent TABLE----------------------------*/
            /*-------------------------------Country TABLE----------------------------*/
            ///*-------------------------------CreditCard TABLE----------------------------*/
            //modelBuilder.Entity<CreditCard>()
            //.HasOne(b => b.employee)
            //.WithMany(p => p.Creditcards)
            //.HasPrincipalKey(x => x.UserId)
            //.HasForeignKey(x => x.UserId);
            modelBuilder.Entity<CreditCard>(b =>
            {
                b.Property<DateTimeOffset>("CreatedOn")
                    .HasColumnType("datetimeoffset(7)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");
            });
            /*-------------------------------CreditCardType TABLE----------------------------*/
            /*-------------------------------Currency TABLE----------------------------*/
            /*-------------------------------CUSTOMER TABLE----------------------------*/
            modelBuilder.Entity<Customer>(b =>
            {
                b.ToTable("Customer");
                //b.HasKey(m => m.Id);
                b.Property(e => e.CreatedOn)
                .HasColumnType("datetimeoffset(7)")
                .HasDefaultValueSql("(sysdatetimeoffset())");
            });

            /*-------------------------------CustomerBranch TABLE----------------------------*/
            /*-------------------------------CustomerShiftStatus TABLE----------------------------*/
            /*-------------------------------Department TABLE----------------------------*/
            /*-------------------------------Education TABLE----------------------------*/
            modelBuilder.Entity<Education>(b =>
            {
                b.Property(e => e.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            });
            /*-------------------------------EducationLevel TABLE----------------------------*/
            /*-------------------------------EmailCategory TABLE----------------------------*/
            /*-------------------------------EmailSubCategory TABLE----------------------------*/
            /*-------------------------------EmailSubCategoryTemplate TABLE----------------------------*/
            /*-------------------------------EmailTemplate TABLE----------------------------*/
            /*-------------------------------Employee TABLE----------------------------*/
            modelBuilder.Entity<Employee>(b =>
            {
                b.Property(e => e.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            });
            /*-------------------------------EmployeeShiftState TABLE----------------------------*/
            /*-------------------------------ErrorMessageCategory TABLE----------------------------*/
            /*-------------------------------ErrorMessageSubCategory TABLE----------------------------*/
            /*-------------------------------Experience TABLE----------------------------*/
            modelBuilder.Entity<Experience>(b =>
            {
                b.Property(e => e.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            });
            /*-------------------------------Invoice TABLE----------------------------*/
            /*-------------------------------JobCategory TABLE----------------------------*/
            /*-------------------------------JobType TABLE----------------------------*/
            /*-------------------------------Location TABLE----------------------------*/
            modelBuilder.Entity("HRMS.Models.Location", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                //b.Property<string>("Address")
                //    .HasColumnType("nvarchar(max)");

                //b.Property<int>("CityId")
                //    .HasColumnType("int");

                //b.Property<int>("CountryId")
                //    .HasColumnType("int");

                //b.Property<int>("CreatedBy")
                //    .HasColumnType("int");

                b.Property<DateTimeOffset>("CreatedOn")
                    .HasColumnType("datetimeoffset(7)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                //b.Property<double>("Latitude")
                //    .HasColumnType("float");

                //b.Property<double>("Longitude")
                //    .HasColumnType("float");

                //b.Property<int>("ModifiedBy")
                //    .HasColumnType("int");

                //b.Property<DateTimeOffset>("ModifiedOn")
                //    .HasColumnType("datetimeoffset(7)");

                //b.Property<string>("Description")
                //    .HasColumnType("nvarchar(max)");

                //b.Property<string>("PostalCode")
                //    .HasColumnType("nvarchar(max)");

                //b.Property<int>("StateProvinceId")
                //    .HasColumnType("int");

                //b.Property<string>("TimeZone")
                //    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Location");
            });

            /*-------------------------------PriorityRank TABLE----------------------------*/
            /*-------------------------------RateType TABLE----------------------------*/
            /*-------------------------------Recruter TABLE----------------------------*/
            /*-------------------------------ReferralBonus TABLE----------------------------*/
            /*-------------------------------Referrals TABLE----------------------------*/
            /*-------------------------------Review TABLE----------------------------*/
            /*-------------------------------Roles TABLE----------------------------*/
            /*-------------------------------Shift TABLE----------------------------*/
            /*-------------------------------ShiftState TABLE----------------------------*/
            /*-------------------------------ShiftSubmissionState TABLE----------------------------*/
            /*-------------------------------StateProvince TABLE----------------------------*/
            /*-------------------------------Timesheet TABLE----------------------------*/
            /*-------------------------------TransactionState TABLE----------------------------*/
            /*-------------------------------TransactionType TABLE----------------------------*/
            /*-------------------------------USERS TABLE----------------------------*/
            modelBuilder.Entity<IdentityUser>(b =>
            {
                b.ToTable("AspUsers");
                //b.HasKey(m => m.Id);
            });

            //modelBuilder.Entity<ApplicationUser>(b =>
            //{
            //    b.Property(e => e.LanguageId)
            //    .HasColumnName("LanguageId")
            //    .HasDefaultValue(1); /*English*/
            //});
            //modelBuilder.Entity<ApplicationUser>(b =>
            //{
            //    b.Property(e => e.LocationId).HasColumnName("LocationId");
            //});
            //modelBuilder.Entity<ApplicationUser>(b =>
            //{
            //    b.Property(e => e.MobileInstalled).HasColumnName("MobileInstalled");
            //});
            //modelBuilder.Entity<ApplicationUser>(b =>
            //{
            //    b.Property(e => e.IsUsedByThirdParty).HasColumnName("IsUsedByThirdParty");
            //});

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(e => e.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            });
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(e => e.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasDefaultValue("ASP");
            });
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn");
            });
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
            });
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(e => e.LastLoginDate)
                .HasColumnName("LastLoginDate")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            });

            /*-------------------------------System TABLE----------------------------*/

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("AspRoles");
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.Property(e => e.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("AspUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("AspUserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("AspUserTokens");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("AspRoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("AspUserRoles");
            });

        }

    }
}