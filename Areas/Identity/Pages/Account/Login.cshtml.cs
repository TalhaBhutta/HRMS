﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HRMS.Models;

namespace HRMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //return LocalRedirect(returnUrl);

                    //var user = await _userManager.FindByNameAsync(Input.UserName);
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user == null)
                    {
                        return NotFound("Unable to load user to update last login.");
                    }
                    //user.LastLoginDate = DateTimeOffset.UtcNow;
                    //user.LastLoginDate = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    user.LastLoginDate = DateTimeOffset.Now; //EST
                    var lastLoginResult = await _userManager.UpdateAsync(user);
                    if (!lastLoginResult.Succeeded)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting the last login date" +
                            $" ({lastLoginResult.ToString()}) for user with ID '{user.Id}'.");
                    }

                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin") || roles.Contains("Owner"))
                    {
                        //@await Html.PartialAsync("~/Views/_Shared/_sidebar.cshtml")
                        return LocalRedirect("/Index");

                    }
                    else if (roles.Contains("Employee"))
                    {
                        //@await Html.PartialAsync("~/Views/_Shared/_sidebarEmployee.cshtml")
                        return LocalRedirect("/EmployeesIndex");
                    }
                    else if (roles.Contains("Customer"))
                    {
                        //@await Html.PartialAsync("~/Views/_Shared/_sidebarCustomer.cshtml")
                        //return LocalRedirect("/CustomersIndex");
                        return LocalRedirect("/CustomerAttendanceReport/smarthr");
                        
                    }
                    else if (roles.Contains("Recruter"))
                    {
                        //@await Html.PartialAsync("~/Views/_Shared/_sidebarRecruter.cshtml")
                        return LocalRedirect("/RecrutersIndex");
                    }

                    //return LocalRedirect("/Index"); //HRMS Index


                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}