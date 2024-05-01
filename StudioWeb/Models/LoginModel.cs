// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudioData.Data;
using StudioData.Models.Business;
using StudioData.Models.JWT;
using StudioWeb.Helppers;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace StudioWeb.Models
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<StudioWebUser> _signInManager;
        private readonly UserManager<StudioWebUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        //private readonly JwtSettings _jwtSettings;

        public LoginModel(SignInManager<StudioWebUser> signInManager, ILogger<LoginModel> logger, UserManager<StudioWebUser> userManager/*, JwtSettings jwtSettings*/)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            //_jwtSettings = jwtSettings;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public class InputModel
        {

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
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/");
            }
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
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    StudioWebUser user = await _userManager.FindByEmailAsync(Input.Email);
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    IList<Claim> currentClaims = await _userManager.GetClaimsAsync(user);
                    List<string> roleClaims = currentClaims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

                    List<Claim> newClaims = roles.Except(roleClaims).Select(role => new Claim(ClaimTypes.Role, role)).ToList();

                    if (newClaims.Any())
                    {
                        await _userManager.AddClaimsAsync(user, newClaims);
                    }

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
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
            return Page();
        }
    }
}
