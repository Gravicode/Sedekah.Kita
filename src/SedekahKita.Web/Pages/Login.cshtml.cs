using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SedekahKita.Web.Services;
using System.Linq;
using SedekahKita.Tools;

namespace SedekahKita.Web.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        
        public string ReturnUrl { get; set; }
        public async Task<IActionResult>
            OnGetAsync(string paramUsername, string paramPassword)
        {
            
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            bool isAuthenticate = true;
            UserProfileService svc = new UserProfileService();
            Encryption enc = new Encryption();
            paramPassword = enc.Encrypt(paramPassword);
            isAuthenticate = svc.TryLogin(paramUsername, paramPassword);
            // In this example we just log the user in
            // (Always log the user in for this demo)
            if (isAuthenticate)
            {
                // *** !!! This is where you would validate the user !!! ***
                // In this example we just log the user in
                // (Always log the user in for this demo)
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, paramUsername),
                new Claim(ClaimTypes.Role, "Administrator"),
            };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };
                try
                {
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            if (!isAuthenticate) returnUrl = "/auth/login?result=false";
            return LocalRedirect(returnUrl);
        }
    }
}
