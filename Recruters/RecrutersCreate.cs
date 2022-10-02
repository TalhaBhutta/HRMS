using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using HRMS.Data;
using HRMS.Models;
using Microsoft.AspNetCore.Identity;
using HRMS.ViewModels;
using Microsoft.Extensions.Configuration;

namespace HRMS.Recruters
{
    public class RecrutersCreate : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDBContext Context { get; }

        public RecrutersCreate(ApplicationDBContext _context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.Context = _context;
            _userManager = userManager;

            // To save physical files to a path provided by configuration:
            //_targetFilePath = config.GetValue<string>("StoredFilesPath");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View("~/Views/RecrutersCreate/Index2.cshtml");
        }
        public ActionResult Index3()
        {
            return View("~/Views/RecrutersCreate/Index3.cshtml");
        }

        [ActionName("recruter-create")]
        public ActionResult recrutercreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(RecruterCreateViewModel mymodel)
        {
            mymodel.newRecruter.UserId = _userManager.GetUserId(HttpContext.User);

            mymodel.newRecruter.DOB = DateTime.Now;

            Location l = new Location
            {
                Description = mymodel.location.Description,
                CountryId = mymodel.location.CountryId,
                StateProvinceId = mymodel.location.StateProvinceId,
                CityId = mymodel.location.CityId,
                Address = mymodel.location.Address,
                PostalCode = mymodel.location.PostalCode,
                TimeZone = mymodel.location.TimeZone,
                Latitude = mymodel.location.Latitude,
                Longitude = mymodel.location.Longitude
            };
            this.Context.Locations.Add(l);
            //this.Context.SaveChanges();

            this.Context.Recruter.Add(new Recruter
            {
                UserId = mymodel.newRecruter.UserId,
                FirstName = mymodel.newRecruter.FirstName,
                LastName = mymodel.newRecruter.LastName,
                //Title = mymodel.newRecruter.Title,
                LocationId = l.Id,
                //CompanyId = mymodel.newRecruter.CompanyId,
                //DepartmentId = mymodel.newRecruter.DepartmentId,
                PrimaryPhoneNumber = mymodel.newRecruter.PrimaryPhoneNumber,
                SecondaryPhoneNumber = mymodel.newRecruter.SecondaryPhoneNumber
            });
            //this.Context.SaveChanges();

            this.Context.CreditCard.Add(new CreditCard
            {
                UserId = mymodel.newRecruter.UserId,
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
                UserId = mymodel.newRecruter.UserId,
                BankName = mymodel.bank.BankName,
                Acname = mymodel.bank.Acname,
                Acnumber = mymodel.bank.Acnumber,
                Institution = mymodel.bank.Institution,
                Branch = mymodel.bank.Branch,
                SignaturePicture = mymodel.bank.SignaturePicture,
                IsActive = mymodel.bank.IsActive
            });
            //this.Context.Bank.Add(b);


            this.Context.SaveChanges();

            //return RedirectToAction("Index", "index", new { area = "" }); //HRMS Index
            return LocalRedirect("/RecrutersIndex");
        }
    }
}
