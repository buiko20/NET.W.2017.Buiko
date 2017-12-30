using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Interface.Services;
using BLL.Interface.Services.Exceptions;
using PL.Web.Models.ViewModels.BankModels;
using PL.Web.Utils;

namespace PL.Web.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        #region home

        [HttpGet]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> Home(string order)
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("UsersInfo");
            }

            return View(await Task.Run(() => this.GetAndSortAccounts(order)));
        }

        [HttpGet]
        public async Task<ActionResult> OrderBy(string sortOrder)
        {
            if (Request.IsAjaxRequest())
            {
                var userAccounts = await Task.Run(() => this.GetAndSortAccounts(sortOrder));
                return PartialView(nameof(this.Home), userAccounts);
            }

            return RedirectToAction(nameof(this.Home), new { order = sortOrder });
        }

        #endregion // !home.

        #region open account

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult OpenAccount() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> OpenAccount(OpenAccountViewModel openAccountData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string userEmail = User.Identity.Name;
            await Task.Run(() => _bankService.OpenAccount(
                userEmail, 
                openAccountData.Password, 
                openAccountData.Sum, 
                openAccountData.Type));

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
        [Authorize(Roles = "user")]
        public ActionResult AccountOperations() => View();

        #region deposit money

        [HttpPost]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> DepositMoney() =>
            await Task.Run(() => this.AccountOperationView(
                operation: nameof(this.DepositMoneyOperation),
                operationName: "Deposit money operation",
                viewName: "DepositOrWithdraw"));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DepositMoneyOperation(DepositOrWithdrawViewModel operationData) =>
            await Task.Run(() => this.DepositOrWithdrawOperation(operationData, _bankService.DepositMoney));

        #endregion // !deposit money.

        #region withdraw money

        [HttpPost]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> WithdrawMoney() =>
            await Task.Run(() => this.AccountOperationView(
                operation: nameof(this.WithdrawMoneyOperation),
                operationName: "Withdraw money operation",
                viewName: "DepositOrWithdraw"));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> WithdrawMoneyOperation(DepositOrWithdrawViewModel operationData)
        {
            var result = await Task.Run(() =>
                this.VerifyModelAndAccountFunds(operationData.AccountNumber, operationData.Sum));
            if (!ReferenceEquals(result, null))
            {
                return result;
            }

            return await Task.Run(() => this.DepositOrWithdrawOperation(operationData, _bankService.WithdrawMoney));
        }

        #endregion // !withdraw money.

        #region transfer funds

        [HttpPost]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> TransferFunds() =>
            await Task.Run(() => this.AccountOperationView(
                operation: nameof(this.TransferFundsOperation),
                operationName: "Transfer funds operation",
                viewName: "TransferFunds"));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> TransferFundsOperation(TransferFundsViewModel transferFundsData)
        {
            var result = await Task.Run(() =>
                this.VerifyModelAndAccountFunds(transferFundsData.FromAccountNumber, transferFundsData.Sum));
            if (!ReferenceEquals(result, null))
            {
                return result;
            }

            string fromUserEmail = User.Identity.Name;
            await Task.Run(() => _bankService.TransferFunds(
                fromUserEmail,
                transferFundsData.FromAccountNumber,
                transferFundsData.ToEmail,
                transferFundsData.ToAccountNumber,
                transferFundsData.Sum));

            TempData["IsError"] = false;
            return RedirectToAction(nameof(this.AccountOperations));
        }

        #endregion // !transfer funds.

        #region close account

        [HttpPost]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> CloseAccount() =>
            await Task.Run(() => this.AccountOperationView(
                operation: nameof(this.CloseAccountOperation),
                operationName: "Close account operation",
                viewName: "CloseAccount"));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> CloseAccountOperation(CloseAccountViewModel closeAccountData)
        {
            if (!ModelState.IsValid)
            {
                return View("DepositOrWithdraw");
            }

            string userEmail = User.Identity.Name;
            await Task.Run(() => _bankService.CloseAccount(
                userEmail,
                closeAccountData.Password,
                closeAccountData.AccountNumber));

            TempData["IsError"] = false;
            return RedirectToAction(nameof(this.AccountOperations));
        }

        #endregion // !close account.

        #endregion // !account operations.

        #region admin actions

        [HttpGet]
        [ActionName("UsersInfo")]
        [Authorize(Roles = "admin")]
        public ActionResult GetUsersInfo() => View();

        [HttpPost]
        [ActionName("UsersInfo")]
        [Authorize(Roles = "admin")]
        [HandleError(ExceptionType = typeof(BankServiceException), View = "BankServiceError")]
        public async Task<ActionResult> GetUsersInfo(PasswordViewModel passwordData)
        {
            if (!ModelState.IsValid)
            {
                return View("UsersInfo");
            }

            try
            {
                string adminEmail = User.Identity.Name;
                var bankUsers = await Task.Run(() => _bankService.GetUsers(adminEmail, passwordData.Password));
                var userViewModels = bankUsers.Select(user => user.ToBankUserViewModel());
                return View("UsersInfoList", userViewModels);
            }
            catch (WrongCredentialsException)
            {
                ModelState.AddModelError(string.Empty, "Wrong password");
                return View("UsersInfo");
            }
        }

        #endregion // !admin actions.

        #region private

        private void UserAccountOrderBy(string order, ref IEnumerable<AccountInfoViewModel> accounts)
        {
            bool isReverseRequested = true;
            if (string.IsNullOrWhiteSpace(order))
            {
                order = "AccountNumber";
                isReverseRequested = false;
            }

            switch (order.ToLower())
            {
                case "accounttype":
                    accounts = accounts.OrderBy(account => account.AccountType);
                    break;
                case "sum":
                    accounts = accounts.OrderBy(account => account.Sum);
                    break;
                case "bonuspoints":
                    accounts = accounts.OrderBy(account => account.BonusPoints);
                    break;
                case "accountnumber":
                default:
                    accounts = accounts.OrderBy(account => account.AccountNumber);
                    break;
            }

            if (isReverseRequested)
            {
                this.ReverseOrder(order, ref accounts);
            }           
        }

        private void ReverseOrder(string order, ref IEnumerable<AccountInfoViewModel> accounts)
        {
            var previousOrder = TempData["PreviousOrder"] as string;
            if (!string.IsNullOrWhiteSpace(previousOrder) &&
                string.Equals(previousOrder, order, StringComparison.OrdinalIgnoreCase))
            {
                accounts = accounts.Reverse();
                TempData["PreviousOrder"] = null;
            }
            else
            {
                TempData["PreviousOrder"] = order;
            }
        }

        private IEnumerable<AccountInfoViewModel> GetAndSortAccounts(string order)
        {
            string userEmail = User.Identity.Name;
            var user = _bankService.GetUserInfo(userEmail);

            var userAccounts = user.Accounts.Select(account => account.ToInfoViewModel());
            this.UserAccountOrderBy(order, ref userAccounts);

            return userAccounts;
        }

        private ActionResult AccountOperationView(string operation, string operationName, string viewName)
        {
            try
            {
                ViewBag.AccountNumbers = this.GetAccountNumbersForCurrentUser();
            }
            catch (Exception)
            {
                if (Request.IsAjaxRequest())
                {
                    TempData["IsError"] = true;
                    return PartialView("_" + viewName);
                }

                throw;
            }

            ViewBag.Operation = operation;
            ViewBag.OperationName = operationName;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_" + viewName);
            }

            return View(viewName);
        }

        private ActionResult DepositOrWithdrawOperation(DepositOrWithdrawViewModel operationData, Action<string, string, decimal> operation)
        {
            if (!ModelState.IsValid)
            {
                return View("DepositOrWithdraw");
            }

            try
            {
                var userEmail = User.Identity.Name;
                operation(userEmail, operationData.AccountNumber, operationData.Sum);
                TempData["IsError"] = false;
            }
            catch (Exception)
            {
                TempData["IsError"] = true;
            }

            return RedirectToAction(nameof(this.AccountOperations));
        }

        private SelectList GetAccountNumbersForCurrentUser()
        {
            var userEmail = User.Identity.Name;
            var user = _bankService.GetUserInfo(userEmail);
            var accountNumbers = user.Accounts.Select(account => account.Id);
            return new SelectList(accountNumbers);
        }

        private bool IsEnoughFundsOnTheAccount(string accountNumber, decimal sum)
        {
            string userEmail = User.Identity.Name;
            var account = _bankService.GetAccountInfo(userEmail, accountNumber);
            return account.Sum >= sum;
        }

        private ActionResult VerifyModelAndAccountFunds(string accountNumber, decimal sum)
        {
            if (!ModelState.IsValid)
            {
                return View("DepositOrWithdraw");
            }

            if (!this.IsEnoughFundsOnTheAccount(accountNumber, sum))
            {
                TempData["NotEnoughFunds"] = true;
                return RedirectToAction(nameof(this.AccountOperations));
            }

            return null;
        }

        #endregion // !private.
    }
}