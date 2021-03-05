﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using BustadirForm.Models;
using Kthusid.Web;
using System.Threading;
using RestSharp;

namespace BustadirForm.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {

        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var cultureName = CookieHelper.GetCultureName(Request);
            var cultureInfo = new System.Globalization.CultureInfo(cultureName);
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return base.BeginExecuteCore(callback, state);
        }

        protected ApplicationUser GetUser()
        {
            return BustadirForm.BLL.AccountHandler.Instance.Initialize(User.Identity.Name);
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (GetUser() != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", @BootstrapResources.Resources.InvalidUsernameOrPassword);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (GetUser() != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    bool canRegister = db.Users.Where(x => x.UserIdCode.ToLower().Equals(model.UserIdCode)).FirstOrDefault() == null;
                    if (!canRegister)
                    {
                        ModelState.AddModelError("", "UserIdCode already in use!");
                        return View(model);
                    }
                }
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserIdCode = model.UserIdCode,
                    PhoneNumber = model.PhoneNumber
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", @BootstrapResources.Resources.UserDoesntExistOrNotConfirmed);
                    return View();
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", @BootstrapResources.Resources.NoUserFound);
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("EditBasicInfo", new { Message = message });
        }

        // GET: /Account/EditUserSettings
        public ActionResult EditUserSettings()
        {

            var manager = new UserManager<BustadirForm.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<BustadirForm.Models.ApplicationUser>(new BustadirForm.Models.ApplicationDbContext()));

            var user = manager.FindById(User.Identity.GetUserId());
            //var user = GetUser();
            if (user.UserSettings == null)
            {
                user.UserSettings = new UserSettings()
                {
                    LabelPrinterName = ""
                };
            }

            var client = new RestClient("http://localhost:9000");
            var request = new RestRequest("api/print/GetPrinters", Method.GET);
            var response = client.Execute<List<String>>(request);
            var installedPrinters = response.Data;

            if (installedPrinters != null)
            {
                ViewBag.LabelPrinterName = new SelectList(installedPrinters.Select(m => new SelectListItem
                {
                    Text = m,
                    Value = m
                }), "Value", "Text", user.UserSettings != null ? user.UserSettings.LabelPrinterName : "");
            }
            else
            {
                ViewBag.LabelPrinterName = new SelectList(new List<SelectListItem>() {new SelectListItem()
                {
                    Text = "-",
                    Value = "-"
                }}, "Value", "Text");
            }

            return View(new ApplicationUserSettingsViewModel()
            {
                LabelPrinterName = user.UserSettings.LabelPrinterName,
            });
        }

        // POST: /Account/EditUserSettings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserSettings([Bind(Include = "LabelPrinterName")] ApplicationUserSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //var user = GetUser();
                if (user != null)
                {
                    if (user.UserSettings == null)
                    {
                        user.UserSettings = new UserSettings();
                    }
                    user.UserSettings.LabelPrinterName = model.LabelPrinterName;
                    await UserManager.UpdateAsync(user);// manager.UpdateAsync(user);
                }
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        // GET: /Account/EditAppSettings
        public ActionResult EditAppSettings()
        {

            var manager = new UserManager<BustadirForm.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<BustadirForm.Models.ApplicationUser>(new BustadirForm.Models.ApplicationDbContext()));
            var user = manager.FindById(User.Identity.GetUserId());
            //var user = GetUser();
            if (user.AppSettings == null)
            {
                user.AppSettings = BustadirForm.Migrations.Configuration.GetDefaultAppSettings();
            }

            return View(new AppSettingsViewModel()
            {
                PrintServerUrl = user.AppSettings.PrintServerUrl,
                HolidayPayPercentage = user.AppSettings.HolidayPayPercentage,
                MaternityPaymentPercentage = user.AppSettings.MaternityPaymentPercentage,
                LaborInsurancePercentage = user.AppSettings.LaborInsurancePercentage,
                MemberFinancialAidPaymentPerDay = user.AppSettings.MemberFinancialAidPaymentPerDay,
                LaborInsuranceAmountPerDay = user.AppSettings.LaborInsuranceAmountPerDay
            });
        }

        // POST: /Account/EditAppSettings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAppSettings(AppSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //var user = GetUser();
                if (user != null)
                {
                    if (user.AppSettings == null)
                    {
                        user.AppSettings = new ApplicationSettings();
                    }
                    user.AppSettings.PrintServerUrl = model.PrintServerUrl;
                    user.AppSettings.HolidayPayPercentage = model.HolidayPayPercentage;
                    user.AppSettings.MaternityPaymentPercentage = model.MaternityPaymentPercentage;
                    user.AppSettings.LaborInsurancePercentage = model.LaborInsurancePercentage;
                    user.AppSettings.MemberFinancialAidPaymentPerDay = model.MemberFinancialAidPaymentPerDay;
                    user.AppSettings.LaborInsuranceAmountPerDay = model.LaborInsuranceAmountPerDay;

                    await UserManager.UpdateAsync(user);// manager.UpdateAsync(user);
                }
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        // GET: /Account/EditBasicInfo
        public ActionResult EditBasicInfo()
        {
            var manager = new UserManager<BustadirForm.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<BustadirForm.Models.ApplicationUser>(new BustadirForm.Models.ApplicationDbContext()));
            var user = manager.FindById(User.Identity.GetUserId());

            return View(new ExternalLoginConfirmationViewModel()
            {
                Email = user.Email,
                Birthday = user.Birthday,
                HomeTown = user.HomeTown,
                PhoneNumber = user.PhoneNumber,
                UserIdCode = user.UserIdCode,
                TwoFactorEnabled = user.TwoFactorEnabled
            });
        }

        // POST: /Account/EditBasicInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBasicInfo(ExternalLoginConfirmationViewModel model)// ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                //var user = manager.FindById(User.Identity.GetUserId());
                //user.Birthday = model.Birthday;
                //user.HomeTown = model.HomeTown;
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.Email = model.Email;
                user.HomeTown = model.HomeTown;
                user.Birthday = model.Birthday;
                user.PhoneNumber = model.PhoneNumber;
                //user.UserIdCode = model.UserIdCode;
                await UserManager.UpdateAsync(user);// manager.UpdateAsync(user);
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        //
        // GET: /Account/ChangePassword
        public ActionResult ChangePassword(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? @BootstrapResources.Resources.YourPasswordHasBeenChanged
                : message == ManageMessageId.SetPasswordSuccess ? @BootstrapResources.Resources.YourPasswordHasBeenSet
                : message == ManageMessageId.RemoveLoginSuccess ? @BootstrapResources.Resources.TheExternalLoginWasRemoved
                : message == ManageMessageId.Error ? @BootstrapResources.Resources.AnErrorHasOccurred
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("ChangePassword");
            return View();
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(ManageMessageId? message)
        {
            return RedirectToAction("EditBasicInfo", message);
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            //ViewBag.ReturnUrl = Url.Action("/EditBasicInfo");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        await SignInAsync(user, isPersistent: false);
                        return View();
                        //return RedirectToAction("ChangePassword", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        return View(model);
                        //AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return View();
                        //return RedirectToAction("ChangePassword", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        return View(model);
                        //AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("EditBasicInfo", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("EditBasicInfo");
            }
            return RedirectToAction("EditBasicInfo", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("EditBasicInfo");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Birthday = model.Birthday,
                    HomeTown = model.HomeTown
                };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Account");
        }

        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Account");
        }
    }
}