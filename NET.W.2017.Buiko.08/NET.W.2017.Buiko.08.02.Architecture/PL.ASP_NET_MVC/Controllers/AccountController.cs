using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Interface.AccountIdService;
using BLL.Interface.AccountService;
using BLL.Interface.MailService;
using PL.ASP_NET_MVC.Models.ViewModels;

namespace PL.ASP_NET_MVC.Controllers
{
    public class AccountController : Controller
    {
        private const string HostEmail = "youe_email@gmail.com";
        private const string HostEmailPassword = "your_email_password";

        private readonly IAccountService _accountService;
        private readonly IAccountIdService _accountIdService;
        private readonly IMailService _mailService;

        public AccountController(
            IAccountService accountService, 
            IAccountIdService accountIdService, 
            IMailService mailService)
        {
            _accountService = accountService;
            _accountIdService = accountIdService;
            _mailService = mailService;
        }

        [HttpGet]
        public JsonResult CheckAccountNumber(string accountNumber)
        {
            try
            {
                _accountService.GetAccountStatus(accountNumber);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #region open account

        [HttpGet]
        public ActionResult OpenAccount() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> OpenAccount(OpenAccountViewModel account)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string openedAccountId = await Task.Run(() => _accountService.OpenAccount(account.Type, account.OwnerFirstName,
                account.OwnerSecondName, account.Sum, account.OwnerEmail, _accountIdService));

            string subject = "Opened account on localhost!";
            string message = $"Thank you for choosing our service!<br/>Your account ID: {openedAccountId}";
            await this.SendMailAsync(account.OwnerEmail, subject, message);

            TempData["isAccountOpened"] = true;
            return RedirectToAction(nameof(this.AccountSuccessfullyOpened));
        }

        [HttpGet]
        public ActionResult AccountSuccessfullyOpened()
        {
            var isAccountOpened = TempData["isAccountOpened"] as bool?;
            if (!isAccountOpened.HasValue || !isAccountOpened.Value)
            {
                return RedirectToAction(nameof(this.OpenAccount));
            }

            TempData["isAccountOpened"] = false;
            return View();
        }

        #endregion // !open account.

        #region account operations

        [HttpGet]
        public ActionResult AccountOperations()
        {
            ViewBag.Error = false;
            ViewBag.IsAccountStatus = false;
            return View();
        }

        #region deposit money

        [HttpPost]
        public ActionResult DepositMoney() =>
            AccountOperationView(
                operation: nameof(this.DepositMoneyOperation),
                operationName: "Deposit money operation",
                viewName: "DepositOrWithdrawMoney");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> DepositMoneyOperation(AccountOperationViewModel data) 
        {
            bool isError;
            var result = this.DepositWithdrawOperation(data, (s, d) => s.DepositMoney(d.AccountNumber, d.Sum), out isError);

            if (isError)
            {
                return result;
            }

            string accountOwnerEmail = this.GetAccountEmail(data.AccountNumber);
            string subject = "Account operation";
            string message = $"Sum={data.Sum} was paid to account id={data.AccountNumber}";
            await this.SendMailAsync(accountOwnerEmail, subject, message);

            return result;
        }

        #endregion // !deposit money.

        #region withdraw money

        [HttpPost]
        public ActionResult WithdrawMoney() =>
            AccountOperationView(
                operation: nameof(this.WithdrawMoneyOperation),
                operationName: "Withdraw money operation",
                viewName: "DepositOrWithdrawMoney");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> WithdrawMoneyOperation(AccountOperationViewModel data)
        {
            bool isError;
            var result = this.DepositWithdrawOperation(data, (s, d) => s.WithdrawMoney(d.AccountNumber, d.Sum), out isError);

            if (isError)
            {
                return result;
            }

            string accountOwnerEmail = this.GetAccountEmail(data.AccountNumber);
            string subject = "Account operation";
            string message = $"From account with id={data.AccountNumber} sum={data.Sum} were withdrawn.";
            await this.SendMailAsync(accountOwnerEmail, subject, message);

            return result;
        }

        #endregion // !withdraw money.

        #region get account status

        [HttpPost]
        public ActionResult GetAccountStatus() =>
            AccountOperationView(
                operation: nameof(this.GetAccountStatusOperation),
                operationName: "Account information",
                viewName: "AccountStatusOrClose");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> GetAccountStatusOperation(AccountNumberViewModel accountId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                return View("AccountStatusOrClose");
            }

            try
            {
                var accountInfo = await Task.Run(() => _accountService.GetAccountStatus(accountId.AccountNumber));
                ViewBag.Error = false;
                ViewBag.IsAccountStatus = true;
                return View(nameof(this.AccountOperations), GetAccountStatusModel(accountInfo));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(accountId.AccountNumber), e.Message);
                ViewBag.Error = true;
                return View("AccountStatusOrClose");
            }           
        }

        #endregion // !get account status.

        #region transfer funds

        [HttpPost]
        public ActionResult TransferFunds() =>
            AccountOperationView(
                operation: nameof(this.TransferFundsOperation),
                operationName: "Transfer funds",
                viewName: "TransferFunds");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> TransferFundsOperation(TransferFundsViewModel transferData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                return View("TransferFunds");
            }

            try
            {
                await Task.Run(() => _accountService.TransferFunds(
                    sourceAccountId: transferData.FromAccountNumber,
                    destinationAccountId: transferData.ToAccountNumber,
                    transferSum: transferData.Sum));
                               
                string accountOwnerEmail = await Task.Run(() => this.GetAccountEmail(transferData.FromAccountNumber));
                string subject = "Transfer funds operation";
                string message = $"From account with id={transferData.FromAccountNumber} sum={transferData.Sum} were withdrawn.";
                await this.SendMailAsync(accountOwnerEmail, subject, message);

                accountOwnerEmail = await Task.Run(() => this.GetAccountEmail(transferData.ToAccountNumber));
                message = $"Sum={transferData.Sum} was paid to account id={transferData.ToAccountNumber}";
                await this.SendMailAsync(accountOwnerEmail, subject, message);

                ViewBag.Error = false;
                return RedirectToAction(nameof(this.AccountOperations));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Error = true;
                return View("_TransferFunds");
            }
        }

        #endregion // !transfer funds.

        #region close account

        [HttpPost]
        public ActionResult CloseAccount() =>
            AccountOperationView(
                operation: nameof(this.CloseAccountOperation),
                operationName: "Close account operation",
                viewName: "AccountStatusOrClose");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> CloseAccountOperation(AccountNumberViewModel accountId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                return View("AccountStatusOrClose");
            }

            try
            {
                var result = RedirectToAction(nameof(this.AccountOperations));

                string accountOwnerEmail = await Task.Run(() => this.GetAccountEmail(accountId.AccountNumber));
                string subject = "Account closed";
                string message = $"account with id={accountId.AccountNumber} is closed.";

                await Task.Run(() => _accountService.CloseAccount(accountId.AccountNumber));
               
                await this.SendMailAsync(accountOwnerEmail, subject, message);

                ViewBag.Error = false;
                return result;
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(accountId.AccountNumber), e.Message);
                ViewBag.Error = true;
                return View("AccountStatusOrClose");
            }
        }

        #endregion // !close account.

        #endregion // !account operations.

        #region private

        private static AccountStatusViewModel GetAccountStatusModel(string data)
        {
            var accountInfo = data.Split(' ');
            int i = 2;
            return new AccountStatusViewModel
            {
                Type = accountInfo[0],
                AccountNumber = accountInfo[i++],
                OwnerFirstName = accountInfo[i++],
                OwnerSecondName = accountInfo[i++],
                Sum = accountInfo[i++],
                BonusPoints = accountInfo[i++],
                OwnerEmail = accountInfo[i]              
            };
        }

        private Task SendMailAsync(string to, string subject, string message)
        {
            var mailData = new MailData
            {
                To = to,
                From = HostEmail,
                FromPassword = HostEmailPassword,
                Subject = subject,
                Message = message
            };

            return _mailService.SendMailAsync(mailData);
        }

        private ActionResult AccountOperationView(string operation, string operationName, string viewName)
        {
            ViewBag.Error = false;
            ViewBag.Operation = operation;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_" + viewName);
            }

            ViewBag.operationName = operationName;
            return View(viewName);
        }

        private ActionResult DepositWithdrawOperation(
            AccountOperationViewModel data, 
            Action<IAccountService, AccountOperationViewModel> operation,
            out bool isError)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                isError = true;
                return View("DepositOrWithdrawMoney");
            }

            try
            {
                operation(_accountService, data);
                ViewBag.Error = false;
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(data.AccountNumber), e.Message);
                ViewBag.Error = true;
                isError = true;
                return View("DepositOrWithdrawMoney");
            }

            isError = false;
            return RedirectToAction(nameof(this.AccountOperations));
        }

        private string GetAccountEmail(string accountId)
        {
            string accountInfo = _accountService.GetAccountStatus(accountId);
            var data = accountInfo.Split(' ');
            return data[data.Length - 1];
        }

        #endregion // !private.
    }
}