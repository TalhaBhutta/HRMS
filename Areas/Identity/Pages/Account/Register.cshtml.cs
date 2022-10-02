using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using HRMS.Models;
using HRMS.Data;

namespace HRMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private ApplicationDBContext db;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            //IEmailSender emailSender
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RoleName { get; set; }

        [BindProperty]
        public string id { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string RoleName, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            // pass the Role List using ViewData
            ViewData["roles"] = _roleManager.Roles.ToList();
            this.RoleName = id;
            ViewData["RoleName"] = RoleName;
            // ViewData["role"] = id;
            //if (RoleName != null)
            //{
                HttpContext.Session.SetString("RoleName", RoleName);
            //}
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            //var z = ViewData["RoleName"];
            string role = HttpContext.Session.GetString("RoleName");
            //var z = Request.Headers.GetValues("MyCustomID").FirstOrDefault();

            //IEnumerable<string> headerValues = Request.Headers.TryGetValue("MyCustomID", out headerValues);
            //var r = headerValues.FirstOrDefault();

            //var r =  HttpContext.Request.Query["RoleName"].ToString();

            //var r  =Request["RoleName"];
            //var token = string.Empty; if (Request.Headers.TryGetValue("MyKey", out headerValues)) 
            //{ 
            //    token = headerValues.FirstOrDefault(); 
            
            //}

            //var d = this.id;
            // assign role to user
            //string role = _roleManager.FindByNameAsync(z).Result.ToString();
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, LastLoginDate = DateTime.Now };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    // code for adding user to role
                    await _userManager.AddToRoleAsync(user, role); 
                    // ends here

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);


                        /**
                         Section for the main page registration options redirection
                         **/

                        if (role.Equals("Employee"))
                        //if (User.IsInRole("Employee"))
                        {
                            //return LocalRedirect(returnUrl);
                            //return View();
                            return RedirectToAction("Index", "EmployeesCreate");
                        }
                        else if (role.Equals("Customer"))
                        {
                            //return LocalRedirect(returnUrl);
                            return RedirectToAction("Index", "CustomersCreate");
                        }
                        else if (role.Equals("Recruter"))
                        {
                            //return LocalRedirect(returnUrl);
                            return RedirectToAction("Index", "RecrutersCreate");
                        }
                        //return LocalRedirect(returnUrl);
                        //return RedirectToAction("Index3", "EmployeesCreate");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
