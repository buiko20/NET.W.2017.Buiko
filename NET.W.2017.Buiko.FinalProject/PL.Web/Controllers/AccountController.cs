using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using BLL.Interface.Services.Exceptions;
using PL.Web.Models.ViewModels.AccountModels;

namespace PL.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBankService _bankService;

        public AccountController(IBankService bankService)
        {
            _bankService = bankService;
        }

        #region registration

        [HttpGet]
        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> Register(RegistrationViewModel registrationData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await Task.Run(() => _bankService.GetUserInfo(registrationData.Email));
            if (!ReferenceEquals(user, null))
            {
                ModelState.AddModelError(String.Empty, "User already exists");
                return View();
            }

            await Task.Run(() => _bankService.RegisterUser(
                registrationData.Email, 
                registrationData.Password,
                registrationData.FirstName,
                registrationData.SecondName));

            FormsAuthentication.SetAuthCookie(registrationData.Email, true);

            return RedirectToAction("Home", "Bank");    
        }

        #endregion // !registration.

        #region login

        [HttpGet]
        public ActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> Login(LoginViewModel loginData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await Task.Run(() => _bankService.VerifyUserCredentials(loginData.Email, loginData.Password));
            }
            catch (UserUnregisteredException)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View();
            }
            catch (WrongCredentialsException)
            {
                ModelState.AddModelError(string.Empty, "Wrong login or password");
                return View();
            }

            FormsAuthentication.SetAuthCookie(loginData.Email, true);
            return RedirectToAction("Home", "Bank");
        }

        #endregion // !login.

        #region logoff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        #endregion logoff

        #region remote

        //[HttpGet]
        //public JsonResult VerifyUserExists(string email) =>
        //    Json(this.IsUserExists(email), JsonRequestBehavior.AllowGet);

        //[HttpGet]
        //public JsonResult VerifyUserNotExists(string email) =>
        //    Json(!this.IsUserExists(email), JsonRequestBehavior.AllowGet);

        //private bool IsUserExists(string email)
        //{
        //    var user = _bankService.GetUserInfo(email);
        //    return !ReferenceEquals(user, null);
        //}

        #endregion // !remote.
    }
}