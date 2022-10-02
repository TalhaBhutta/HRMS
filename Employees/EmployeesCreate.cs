using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMS.Models;
using HRMS.Data;
using HRMS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Internal;


using System.ComponentModel.DataAnnotations;
using System.IO;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using HRMS.Utilities;


namespace HRMS.Employees
{
    public class EmployeesCreate : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ILogger<HomeController> _logger;
        //private EmployeeDBContext Context { get; }
        private ApplicationDBContext Context { get; }

        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".txt", ".pdf", ".jpg", ".jpeg", ".gif", ".doc", ".png" };
        private readonly string _targetFilePath;
        [BindProperty]
        public BufferedSingleFileUploadPhysical FileUpload { get; set; }

        public EmployeesCreate(ApplicationDBContext _context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.Context = _context;
            _userManager = userManager;

            // To save physical files to a path provided by configuration:
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
        }

        public ActionResult Index()
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ActionResult Index2()
        {
            return View("~/Views/EmployeesCreate/Index2.cshtml");
        }
        public ActionResult Index3()
        {
            return View("~/Views/EmployeesCreate/Index3.cshtml");
        }
        //public ActionResult Index4()
        //{
        //    return View("~/Views/EmployeesCreate/Index4.cshtml");
        //}
        public ActionResult Index5()
        {
            return View("~/Views/EmployeesCreate/Index5.cshtml");
        }

        [ActionName("employee-create")]
        public ActionResult employeecreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(EmployeeCreateViewModel mymodel)
        {
            //if(ModelState.IsValid)
            //EmployeeViewModel mymodel = new EmployeeViewModel();

            //EmployeeViewModel mymodel = new EmployeeViewModel
            //{
            //    emp = e,
            //    location = l

            //};
            //code here
            //mymodel.emp.FirstName = e.FirstName;
            //var LastName = e.LastName;
            //e.UserId = _userManager.GetUserId(HttpContext.User);
            var user = await GetCurrentUserAsync();

            var userId = user?.Id;
            string mail = user?.Email;

            mymodel.newEmployee.UserId = _userManager.GetUserId(HttpContext.User);
            //e.Gender = "Male";
            mymodel.newEmployee.DOB = DateTime.Now;

            Location l = new Location
            {
                Description = mymodel.expe.Description,
                CountryId = mymodel.location.CountryId,
                StateProvinceId = mymodel.location.StateProvinceId,
                CityId = mymodel.location.CityId,
                Address = mymodel.location.Address,
                PostalCode = mymodel.location.PostalCode,
                TimeZone = mymodel.location.TimeZone,
                Latitude = mymodel.location.Latitude,
                Longitude = mymodel.location.Longitude,
                CreatedBy = userId,
                CreatedOn = mymodel.location.CreatedOn
            };
            this.Context.Locations.Add(l);
            this.Context.SaveChanges();

            Employee e = new Employee
            {
                UserId = mymodel.newEmployee.UserId,
                FirstName = mymodel.newEmployee.FirstName,
                LastName = mymodel.newEmployee.LastName,
                Email = mymodel.newEmployee.Email,
                Introduction = mymodel.newEmployee.Introduction,
                LocationId = l.Id,
                PrimaryLanguageId = mymodel.newEmployee.PrimaryLanguageId,
                SecondaryLanguageId = mymodel.newEmployee.SecondaryLanguageId,
                EducationLevelId = mymodel.newEmployee.EducationLevelId,
                ReputationId = mymodel.newEmployee.ReputationId,
                DepartmentId = mymodel.newEmployee.DepartmentId,
                RateType = mymodel.newEmployee.RateType,
                PrimaryPhoneNumber = mymodel.newEmployee.PrimaryPhoneNumber,
                SecondaryPhoneNumber = mymodel.newEmployee.SecondaryPhoneNumber,
                HourRateSalary = mymodel.newEmployee.HourRateSalary,
                AvailableForDayShift = mymodel.newEmployee.AvailableForDayShift,
                AvailableForEveningShift = mymodel.newEmployee.AvailableForEveningShift,
                AvailableForNightShift = mymodel.newEmployee.AvailableForNightShift,
                AvailableForWeekEnds = mymodel.newEmployee.AvailableForWeekEnds,
                Gender = mymodel.newEmployee.Gender,
                Ssn = mymodel.newEmployee.Ssn,
                DOB = mymodel.newEmployee.DOB,
                CriminalRecord = mymodel.newEmployee.CriminalRecord,
                Handicap = mymodel.newEmployee.Handicap,
                EmployeeStateId = mymodel.newEmployee.EmployeeStateId,
                Picture = mymodel.newEmployee.Picture,
                IsActive = mymodel.newEmployee.IsActive,
                JobCategoryId = mymodel.newEmployee.JobCategoryId,
                Title = mymodel.newEmployee.Title,
            };
            this.Context.Employee.Add(e);
            this.Context.SaveChanges();


            this.Context.Experience.Add(new Experience
            {
                EmployeeId = e.Id,
                Title = mymodel.expe.Title,
                Company = mymodel.expe.Company,
                Location = mymodel.expe.Location,
                Description = mymodel.expe.Description,
                StartingDate = mymodel.expe.StartingDate,
                EndingDate = mymodel.expe.EndingDate,
            });

            this.Context.Education.Add(new Education
            {
                EmployeeId = e.Id,
                Program = mymodel.educations[0].Program,
                Institution = mymodel.educations[0].Institution,
                FieldofStudy = mymodel.educations[0].FieldofStudy,
                EducationLevelId = mymodel.educations[0].EducationLevelId,
                StartingDate = mymodel.educations[0].StartingDate,
                EndingDate = mymodel.educations[0].EndingDate,
            });

            this.Context.CreditCard.Add(new CreditCard
            {
                UserId = mymodel.newEmployee.UserId,
                AccountId = mymodel.creditcard.AccountId,
                CreditCardTypeId = mymodel.creditcard.CreditCardTypeId,
                AccountNumber = mymodel.creditcard.AccountNumber,
                First4Digits = mymodel.creditcard.First4Digits,
                Last4Digits = mymodel.creditcard.Last4Digits,
                ExpiryDate = mymodel.creditcard.ExpiryDate,
                Cvv = mymodel.creditcard.Cvv,
                Avsverified = mymodel.creditcard.Avsverified,
                Name = mymodel.creditcard.Name,
                AuthenticationSubmitted = mymodel.creditcard.AuthenticationSubmitted,
                Csnote = mymodel.creditcard.Csnote,
                Valid = mymodel.creditcard.Valid,
                Cavv = mymodel.creditcard.Cavv,
                Eciflag = mymodel.creditcard.Eciflag,
                Xid = mymodel.creditcard.Xid,
                CardLimit = mymodel.creditcard.CardLimit,
                ExpiryEmail1Sent = mymodel.creditcard.ExpiryEmail1Sent,
                ExpiryEmail2Sent = mymodel.creditcard.ExpiryEmail2Sent,
                Category = mymodel.creditcard.Category
            });
            //this.Context.Creditcard.Add(c);

            this.Context.Bank.Add(new Bank
            {
                UserId = mymodel.newEmployee.UserId,
                BankName = mymodel.bank.BankName,
                Acname = mymodel.bank.Acname,
                Acnumber = mymodel.bank.Acnumber,
                Institution = mymodel.bank.Institution,
                Branch = mymodel.bank.Branch,
                SignaturePicture = mymodel.bank.SignaturePicture,
                IsActive = mymodel.bank.IsActive
            });
            //e.locations.Add(l);
            //var LastName = e.;
            //@UserManager.GetUserName(User)
            //var country = e.Country;
            //var Email = e.Email;

            //save in database
            // this.Context.Locations.Add(l);
            //this.Context.
            //this.Context.Employees.Add(mymodel);

            //mymodel.emp.loc.Add(mymodel.location);
            //mymodel.emp.ex.Add(mymodel.expe);
            //mymodel.emp.e = mymodel.expe;
            //mymodel.emp.ex = mymodel.expes;

            //using (var context = new BloggingContext())
            //{
            //Console.WriteLine(this.Context.Model.ToDebugString(4));
            //}


            //using (var transaction = this.Context.Database.BeginTransaction())
            //{
            //this.Context.Employee.Add(mymodel.newEmployee);
                //this.Context.SaveChanges();

                //this.Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Experience ON;");
                this.Context.SaveChanges();
            //this.Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Experience OFF;");
            //    transaction.Commit();
            //}

            //Add location to db
            //this.Context.Locations.Add(mymodel.location);
            //this.Context.SaveChanges();
            //mymodel.emp.LocationId = mymodel.location.Id;

            //Add employee to db


            //Add employee's experience to db
            //this.Context.Experiences.AddRange(mymodel.emp.experience);
            //mymodel.expe.EmployeeId = mymodel.emp.Id;


            //List<Experience> ex = new List<Experience>();
            //ex.Add(new Experience { EmployeeId = 8, Title = "Test - P" });
            //ex.Add(new Experience { EmployeeId = 8, Title = "Test - Q" });
            //ex.Add(new Experience { EmployeeId = 8, Title = "Test - R" });

            //this.Context.Experiences.AddRange(ex);
            //foreach (var e in mymodel.expe)
            //{
            //    location = new Location
            //    {
            //        PersonId = model.PersonId,
            //        SiteCode = loc.SiteCode,
            //        IncidentId = loc.IncidentId
            //    };
            //}

            //this.Context.Experiences.Add(mymodel.expe);
            //this.Context.Experiences.AddRange(mymodel.expes);
            //this.Context.SaveChanges();

            //Add employee's education to db
            //this.Context.Education.AddRange(mymodel.education);
            //this.Context.SaveChanges();

            ////Add employee's cc to db
            //this.Context.Creditcards.AddRange(mymodel.creditcards);
            //this.Context.SaveChanges();

            ////Add employee's banks to db
            //this.Context.Banks.AddRange(mymodel.banks);
            //this.Context.SaveChanges();

            //code goes here
            //return View(new Employee());
            // return View(mymodel);
            //return View("~/Views/EmployeesCreate/Index.cshtml");




            /** File Upload Procedure**/
            //await OnPostUploadAsync();

            //return RedirectToAction("Index", "index", new { area = "" }); //HRMS Index
            return LocalRedirect("/EmployeesIndex");
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    Result = "Please correct the form.";

            //    return Page();
            //}

            var formFileContent =
                await FileHelpers.ProcessFormFile<BufferedSingleFileUploadPhysical>(
                    FileUpload.FormFile, ModelState, _permittedExtensions,
                    _fileSizeLimit);

            //if (!ModelState.IsValid)
            //{
            //    Result = "Please correct the form.";

            //    return Page();
            //}

            // For the file name of the uploaded file stored
            // server-side, use Path.GetRandomFileName to generate a safe
            // random file name.
            var trustedFileNameForFileStorage = Path.GetRandomFileName();
            var filePath = Path.Combine(
                _targetFilePath, trustedFileNameForFileStorage);

            // **WARNING!**
            // In the following example, the file is saved without
            // scanning the file's contents. In most production
            // scenarios, an anti-virus/anti-malware scanner API
            // is used on the file before making the file available
            // for download or for use by other systems. 
            // For more information, see the topic that accompanies 
            // this sample.

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await fileStream.WriteAsync(formFileContent);

                // To work directly with a FormFile, use the following
                // instead:
                //await FileUpload.FormFile.CopyToAsync(fileStream);
            }
            return RedirectToPage("./Index");
        }
    }

    public class BufferedSingleFileUploadPhysical
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }
}
