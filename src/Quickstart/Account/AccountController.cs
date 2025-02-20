// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityProvider.Core;
using IdentityProvider.Data;
using IdentityProvider.Data.Entity;
using IdentityProvider.Models;
using IdentityProvider.Services;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace IdentityProvider.Quickstart.Account
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsPrincipalFactory;
        private readonly IEmailSenderService _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            SignInManager<ApplicationUser> signInManager,
            IEmailSenderService emailSender,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsPrincipalFactory,
            IWebHostEnvironment webHostEnvironment,
            ApplicationDbContext context)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { provider = vm.ExternalLoginScheme, returnUrl });
            }

            TempData["ReturnUrl"] = returnUrl;

            //return View(vm);

            return RedirectToAction("Authenticate");
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginInputModel model, string button)
        //{
        //    // check if we are in the context of an authorization request
        //    var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

        //    // the user clicked the "cancel" button
        //    if (button != "login")
        //    {
        //        if (context != null)
        //        {
        //            // if the user cancels, send a result back into IdentityServer as if they 
        //            // denied the consent (even if this client does not require consent).
        //            // this will send back an access denied OIDC error response to the client.
        //            await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

        //            // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
        //            if (context.IsNativeClient())
        //            {
        //                // The client is native, so this change in how to
        //                // return the response is for better UX for the end user.
        //                return this.LoadingPage("Redirect", model.ReturnUrl);
        //            }

        //            return Redirect(model.ReturnUrl);
        //        }
        //        else
        //        {
        //            // since we don't have a valid context, then we just go back to the home page
        //            return Redirect("~/");
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var user = await _signInManager.UserManager.FindByNameAsync(model.Username);

        //        // validate username/password against in-memory store
        //        if (user != null && (await _signInManager.CheckPasswordSignInAsync(user, model.Password, true)) == SignInResult.Success)
        //        {
        //            await _signInManager.UserManager.ResetAccessFailedCountAsync(user);

        //            if (await RequireTwoFactorAsync(user))
        //            {
        //                var token = await _signInManager.UserManager.GenerateTwoFactorTokenAsync(user, "Email");
        //                await SendEmailTwoFactorTokenAsync(user.Email, token);

        //                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
        //                    Store2Fa(user.Id, "Email"));

        //                TempData["ReturnUrl"] = model.ReturnUrl;
        //                TempData["RememberLogin"] = model.RememberLogin;

        //                return RedirectToAction("TwoFactor");
        //            }

        //            return await CompletePasswordSignInAsync(model.RememberLogin, model.ReturnUrl, user, context);
        //        }

        //        await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId:context?.Client.ClientId));
        //        ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
        //    }

        //    // something went wrong, show form with error
        //    var vm = await BuildLoginViewModelAsync(model);
        //    return View(vm);
        //}

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("auth/login")]
        public async Task<IActionResult> Authenticate(LoginInputModel model, string button)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            // the user clicked the "cancel" button
            if (button != "login")
            {
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(model.Username);

                // validate username/password against in-memory store
                if (user != null && (await _signInManager.CheckPasswordSignInAsync(user, model.Password, true)) == SignInResult.Success)
                {
                    await _signInManager.UserManager.ResetAccessFailedCountAsync(user);

                    if (await RequireTwoFactorAsync(user))
                    {
                        var token = await _signInManager.UserManager.GenerateTwoFactorTokenAsync(user, "Email");

                        await SendEmailTwoFactorTokenAsync(user.Email, token);
                        //await System.IO.File.WriteAllTextAsync("email2sv.txt", token);

                        await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
                            Store2Fa(user.Id, "Email"));

                        TempData["ReturnUrl"] = model.ReturnUrl;
                        TempData["RememberLogin"] = model.RememberLogin;

                        return RedirectToAction("TwoFactor");
                    }

                    return await CompletePasswordSignInAsync(model.RememberLogin, model.ReturnUrl, user, context);
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        private async Task<bool> RequireTwoFactorAsync(ApplicationUser user)
        {
            try
            {
                var twoFactorEnabled = await _signInManager.UserManager.GetTwoFactorEnabledAsync(user);
                if (!twoFactorEnabled)
                    return false;

                var ua = new UserAgentInformation();
                var browserInfo = ua.GetBrowserInfo(HttpContext);

                var userDevice = await _context.UserDeviceSet.FirstOrDefaultAsync(x => x.Device == browserInfo.UserAgentString);

                //var location = await GetAddressLocationAsync("129.0.78.146");

                if (userDevice == null)
                {
                    await AddUserDeviceAsync(user, browserInfo);
                    return true;
                }
                else
                {
                    if (userDevice.Expiration <= DateTime.UtcNow)
                    {
                        await InvalidateDeviceAsync(userDevice);
                        await AddUserDeviceAsync(user, browserInfo);
                        return true;
                    }
                    else
                    {
                        await UpdateDeviceAsync(user, browserInfo);
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
                return true;
            }
        }

        private async Task RegisterLoginAsync(ApplicationUser user)
        {
            try
            {
                var ua = new UserAgentInformation();
                var browserInfo = ua.GetBrowserInfo(HttpContext);

                var userLogin = new UserLogin
                {
                    Id = Guid.NewGuid().ToString(),
                    Address = browserInfo.IpAddress,
                    Device = browserInfo.UserAgentString,
                    User = user.Id,
                    Timestamp = DateTime.UtcNow
                };

                await _context.UserLoginSet.AddAsync(userLogin);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async Task UpdateDeviceAsync(ApplicationUser user, UserAgent browserInfo)
        {
            var device = await _context.UserDeviceSet.FirstOrDefaultAsync(x => x.Device == browserInfo.UserAgentString);
            if (device != null)
            {
                device.LastIp = browserInfo.IpAddress;
                device.LastUsed = DateTime.UtcNow;
                _context.Entry(device).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddUserDeviceAsync(ApplicationUser user, UserAgent browserInfo)
        {
            var userDevice = new UserDevice
            {
                User = user.UserCode,
                Device = browserInfo.UserAgentString,
                LastIp = browserInfo.IpAddress,
                LastUsed = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddMinutes(5)
            };
            await _context.UserDeviceSet.AddAsync(userDevice);
            await _context.SaveChangesAsync();
        }

        private async Task InvalidateDeviceAsync(UserDevice userDevice)
        {
            try
            {
                var device = await _context.UserDeviceSet.AsNoTracking().FirstOrDefaultAsync(x => x.Device == userDevice.Device);
                if (device != null)
                {
                    _context.Entry(device).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            return await _signInManager.UserManager.GetTwoFactorEnabledAsync(user);
        }

        private async Task SendEmailTwoFactorTokenAsync(string email, string token)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);

            string[] paths = { _webHostEnvironment.WebRootPath, "templates", "verification.htm" };
            var fullPath = Path.Combine(paths);

            string body;

            using (var reader = new StreamReader(fullPath))
            {
                body = await reader.ReadToEndAsync();
            }

            var ua = new UserAgentInformation();
            var browser = ua.GetBrowserInfo(HttpContext);

            body = body.Replace("{account}", email); //replacing the required things  
            body = body.Replace("{date}", DateTime.UtcNow.ToLongDateString());
            //body = body.Replace("{location}", token);
            body = body.Replace("{ipaddress}", browser.IpAddress);
            body = body.Replace("{os}", $"{browser.OperatingSystemFamily} {browser.OperatingSystemMajor}");
            body = body.Replace("{browser}", $"{browser.UserAgentFamily} {browser.UserAgentMajor}");
            body = body.Replace("{token}", token);

            var userEmailOptions = new UserEmailOptions
            {
                Body = body,
                Subject = "Dama Verification Code",
                ToEmail = user.Email,
                ToName = user.FullName,
            };

            await _emailSender.SendEmailAsync(userEmailOptions);
        }

        private async Task<LocationModel> GetAddressLocationAsync(string address)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://ipapi.co")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            LocationModel result;

            var response = await client.GetAsync($"/{address}/json");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<LocationModel>(data);
            }
            else
            {
                result = null;
            }

            return result;
        }

        [HttpGet]
        [Route("auth/login")]
        public async Task<IActionResult> Authenticate()
        {
            var returnUrl = "";

            if (TempData.ContainsKey("ReturnUrl"))
                returnUrl = TempData["ReturnUrl"] as string;


            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);

            return View(vm);
        }

        [HttpGet]
        [Route("auth/mfa-challenge")]
        public IActionResult TwoFactor()
        {
            var returnUrl = "";
            var rememberLogin = false;

            if (TempData.ContainsKey("ReturnUrl"))
                returnUrl = TempData["ReturnUrl"] as string;

            if (TempData.ContainsKey("RememberLogin"))
                rememberLogin = Convert.ToBoolean(TempData["RememberLogin"]);

            var vm = new TwoFactorModel
            {
                ReturnUrl = returnUrl,
                RememberLogin = rememberLogin
            };


            return View(vm);
        }

        [HttpPost]
        [Route("auth/mfa-challenge")]
        public async Task<IActionResult> TwoFactor(TwoFactorModel model)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Your login request has expired, please start over");
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByIdAsync(result.Principal.FindFirstValue("sub"));

                if (user != null)
                {
                    var isValid = await _signInManager.UserManager.VerifyTwoFactorTokenAsync(user,
                        result.Principal.FindFirstValue("amr"), model.Token);

                    //const bool isValid = true;

                    if (isValid)
                    {
                        await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

                        return await CompletePasswordSignInAsync(model.RememberLogin, model.ReturnUrl, user, context);
                    }

                    ModelState.AddModelError("", "Invalid token");
                    return View();
                }

                ModelState.AddModelError("", "Invalid Request");
            }

            return View();
        }

        [HttpGet]
        [Route("user/forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("user/forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await SendPasswordResetAsync(model.Email);
                }
                else
                {
                    // email user and inform them that they do not have an account
                }

                return View("Success");
            }

            return View();
        }

        private async Task SendPasswordResetAsync(string email)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);

            var token = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
            var confirmEmailLink = Url.Action("ResetPassword", "Account",
                new { token = token, email = user.Email }, Request.Scheme);

            string[] paths = { _webHostEnvironment.WebRootPath, "templates", "request-password-reset.html" };
            var fullPath = Path.Combine(paths);

            string body;

            try
            {
                using var reader = new StreamReader(fullPath);
                body = await reader.ReadToEndAsync();
            }
            catch (Exception e)
            {
                //_logger.LogInformation($"EXCEPTION OCCURED {e}");
                throw;
            }

            body = body.Replace("{Url}", confirmEmailLink).Replace("{UserName}", user.FullName); //replacing the required things  

            await SendEmailAsync(body, user.Email, user.FullName, "Reset Password");
        }

        private async Task SendEmailAsync(string body, string email, string fullName, string subject)
        {
            var userEmailOptions = new UserEmailOptions
            {
                Body = body,
                Subject = subject,
                ToEmail = email,
                ToName = fullName,
            };

            await _emailSender.SendEmailAsync(userEmailOptions);
        }

        [HttpGet]
        [Route("user/reset-password")]
        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordModel { Token = token, Email = email });
        }

        [HttpPost]
        [Route("user/reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.UserManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }
                    return View("Success");
                }
                ModelState.AddModelError("", "Invalid Request");
            }
            return View();
        }

        [HttpGet]
        //[Route("user/confirm-email")]
        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _signInManager.UserManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    //await SendPasswordResetEmailAsync(user);
                    return View("Success");
                }
            }

            return View("Error");
        }

        private static ClaimsPrincipal Store2Fa(string userId, string provider)
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("sub", userId),
                new Claim("amr", provider)
            }, IdentityConstants.TwoFactorUserIdScheme);

            return new ClaimsPrincipal(identity);
        }

        private async Task<IActionResult> CompletePasswordSignInAsync(bool rememberLogin, string returnUrl,
            ApplicationUser user, AuthorizationRequest context)
        {
            await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName,
                clientId: context?.Client.ClientId));

            // only set explicit expiration here if user chooses "remember me". 
            // otherwise we rely upon expiration configured in cookie middleware.
            AuthenticationProperties props = null;
            if (AccountOptions.AllowRememberLogin && rememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                };
            }

            // issue authentication cookie with subject ID and username
            var issuer = new IdentityServerUser(user.Id)
            {
                DisplayName = user.UserName
            };

            await HttpContext.SignInAsync(issuer, props);
            await RegisterLoginAsync(user);

            if (context != null)
            {
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", returnUrl);
                }

                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                return Redirect(returnUrl);
            }

            // request for a local page
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else if (string.IsNullOrEmpty(returnUrl))
            {
                return Redirect("~/");
            }
            else
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }
        }

        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User.Identity?.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return View("LoggedOut", vm);
            //return Redirect(vm.PostLogoutRedirectUri);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        /****************************************
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User.Identity?.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity?.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
