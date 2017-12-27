using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.MailService;
using DAL.Interface;
using PL.ASP_NET_MVC.Models.ViewModels.AuthModels;

namespace PL.ASP_NET_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;

        private readonly string _hostEmail;
        private readonly string _hostEmailPassword;

        public AuthController(IUserRepository userRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _mailService = mailService;
            _hostEmail = WebConfigurationManager.AppSettings["HostEmail"];
            _hostEmailPassword = WebConfigurationManager.AppSettings["HostEmailPassword"];
        }

        [HttpGet]
        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userRepository.GetUser(registerData.Email);
            if (ReferenceEquals(user, null))
            {
                var dalUser = new DAL.Interface.DTO.DalUser
                {
                    Email = registerData.Email,
                    Password = registerData.Passwod
                };

                _userRepository.AddUser(dalUser);
                FormsAuthentication.SetAuthCookie(registerData.Email, true);

                string subject = "Registration on localhost!";
                string message = "You registered at the localhost to work with local crypto currency.";
                await this.SendMailAsync(registerData.Email, subject, message);

                return RedirectToAction("OpenAccount", "Account");
            }

            ModelState.AddModelError("", "User already exists");
            return View();
        }

        [HttpGet]
        public ActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userRepository.GetUser(loginData.Email);
            if (ReferenceEquals(user, null))
            {
                ModelState.AddModelError("", "User not found");
                return View();
            }

            if (!string.Equals(loginData.Passwod, user.Password, StringComparison.Ordinal))
            {
                ModelState.AddModelError("", "Wrong login or password");
                return View();
            }

            FormsAuthentication.SetAuthCookie(loginData.Email, true);
            return RedirectToAction("OpenAccount", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Auth");
        }

        private Task SendMailAsync(string to, string subject, string message)
        {
            var mailData = new MailData
            {
                To = to,
                From = _hostEmail,
                FromPassword = _hostEmailPassword,
                Subject = subject,
                Message = message
            };

            return _mailService.SendMailAsync(mailData);
        }
    }
}