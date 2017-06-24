using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using ShoppingStore.Data.Identity.IdentityManagers;
using ShoppingStore.Data.ViewModels.AccountViewModels;
using ShoppingStore.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingStore.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }


        private void ErrorMessage(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendVerifiedEmail(SendEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Json("Success");
            }

            string code =
                await userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = Url.Action("ConfirmEmail", "Account", new
            {
                userId = user.Id,
                token = code
            }, protocol: Request.Url.Scheme);



            await userManager.SendEmailAsync(user.Id, "Confirm your account",
                "Please Confirm your account under this links <a href=\"" +
                 callbackUrl +
                "\">Confirm Link</a>" +
               "<h6>if this email is not yours,please ignore it.</h6>");


            return Json("Success");

        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("", "User Id and Code are required.");
                return View("ConfirmFailed");
            }


            IdentityResult result = await userManager.ConfirmEmailAsync(userId, token);

            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }

            return View("ConfirmFailed");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(SendEmailViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null || !(await userManager.IsEmailConfirmedAsync(user.Id)))
            {
                return Json("Please Confirm your Email");
            }

            string code = await userManager.GeneratePasswordResetTokenAsync(user.Id);

            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { userId = user.Id, token = code },
                protocol: Request.Url.Scheme);
            await userManager.SendEmailAsync(
                user.Id, "Reset Password",
                    "Please reset your password by clicking <a href=\""
                    + callbackUrl +
                    "\">" + "Here" + "</a>");

            return Json("Please Confirm your Email");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            return token == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(
            ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return View("ResetPasswordConfirm");
                }

                var result = await userManager.ResetPasswordAsync(
                    user.Id, model.Token, model.NewPassword);

                if (result.Succeeded)
                {
                    return View("ResetPasswordConfirm");
                }

                ErrorMessage(result);
            }

            return View(model);
        }


        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{

        //}



    }
}