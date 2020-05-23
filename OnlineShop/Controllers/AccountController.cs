using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using BLL.Concrete;
using BLL.Models;
using OnlineShop.Helpers;
using System.IO;
using System.Drawing;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AccountProvider _accountProvider;
        public AccountController(AccountProvider accountProvider)
        {
            _accountProvider = accountProvider;
        }

        // GET: Account
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _accountProvider.Login(model);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new
                    {
                        ReturnUrl = returnUrl,
                        RememberMe = model.RememberMe
                    });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _accountProvider.LogOff();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
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
                string dir = Server.MapPath("~") + "Images\\User\\";
                int widthBig = 2560;
                // Імя фалу із зображенням
                try
                {
                    string filePhoto = string.Empty;
                    filePhoto = Guid.NewGuid().ToString() + ImageWorker.GetImageType(model.Image);
                    byte[] bytesPhoto = ImageWorker.GetImageBytesFromBase64(model.Image);
                    string imageType = ImageWorker.GetImageType(model.Image);
                    Bitmap imageBig = ImageWorker.SaveImageFromBytesTry(bytesPhoto, widthBig);
                    if (imageBig != null)
                    {
                        string fileName = widthBig + "_" + filePhoto;
                        string pathSaveFile = Path.Combine(dir, fileName);
                        imageBig.Save(pathSaveFile, ImageWorker.GetImageFormat(imageType));
                        model.Image = fileName;
                    }
                }
                catch {}
                StatusViewModel result = _accountProvider.Register(model);
                if (result == StatusViewModel.Success)
                {
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return RedirectToAction("Index", "Home");
                }
                //AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }


        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await _accountProvider.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            //var firstName = loginInfo
            //    .ExternalIdentity
            //    .Claims
            //    .First(c => c.Type == "urn:facebook:first_name").Value;
            //
            //var lastName = loginInfo
            //    .ExternalIdentity
            //    .Claims
            //    .First(c => c.Type == "urn:facebook:last_name").Value;
            //Sign in the user with this external login provider if the user already has a login

            var result = await _accountProvider.ExternalSignInAsync(loginInfo);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    var status = _accountProvider.CreateLogin(loginInfo.Email);
                    
                    if (status == StatusViewModel.Success)
                        return RedirectToLocal(returnUrl);

                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [ChildActionOnly]
        public ActionResult GetUserImage()
        {
            var user = _accountProvider.GetUserById(User.Identity.GetUserId());
            ImageViewModel image = new ImageViewModel();
        
            if (user.Image != null)
                image.ImageName = user.Image;
            image.Folder = "User";
            
            return PartialView("_Image", image);
        }

        [Authorize]
        [HttpGet]
        public ActionResult UserCart()
        {
            return View();
            //var user = _accountProvider.GetUserById(User.Identity.GetUserId());
            //return View(new UserInterfereViewModel
            //{
            //    Email = user.Email,
            //    FirstName = user.FirstName,
            //    Image = user.Image,
            //    LastName = user.LastName
            //});
        }

        [Authorize]
        [HttpGet]
        public ActionResult UserCabinet()
        {
            var user = _accountProvider.GetUserById(User.Identity.GetUserId());
            return View(new UserInterfereViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Image = user.Image,
                LastName = user.LastName
            });
        }

        [Authorize]
        [HttpPost]
        public ActionResult UserCabinet(UserInterfereViewModel model)
        {
            if (ModelState.IsValid)
            {
                string dir = Server.MapPath("~") + "Images\\User\\";
                int widthBig = 2560;
                // Імя фалу із зображенням
                string filePhoto = string.Empty;
                filePhoto = Guid.NewGuid().ToString() + ImageWorker.GetImageType(model.Image);
                byte[] bytesPhoto = ImageWorker.GetImageBytesFromBase64(model.Image);
                string imageType = ImageWorker.GetImageType(model.Image);
                Bitmap imageBig = ImageWorker.SaveImageFromBytesTry(bytesPhoto, widthBig);
                if (imageBig != null)
                {
                    string fileName = widthBig + "_" + filePhoto;
                    string pathSaveFile = Path.Combine(dir, fileName);
                    imageBig.Save(pathSaveFile, ImageWorker.GetImageFormat(imageType));
                    model.Image = fileName;
                }
                StatusViewModel result = _accountProvider.EditUser(model, User.Identity.GetUserId());
                //if (result == StatusViewModel.Success) { }
                //AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private const string XsrfKey = "XsrfId";

        internal class ChallengeResult : HttpUnauthorizedResult
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
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}