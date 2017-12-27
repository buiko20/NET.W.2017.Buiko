using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Interface.AccountIdService;
using BLL.Interface.AccountService;
using PL.ASP_NET_MVC.Models.ViewModels;

namespace PL.ASP_NET_MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAccountIdService _accountIdService;

        public AccountController(IAccountService accountService, IAccountIdService accountIdService)
        {
            _accountService = accountService;
            _accountIdService = accountIdService; 
        }

        [HttpGet]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> Home()
        {
            var ownerAccountsInfo = await Task.Run(() => _accountService.GetOwnerAccounts(User.Identity.Name));
            var ownerAccounts = ownerAccountsInfo.Select(GetAccountStatusModel).ToArray();
            return View(ownerAccounts);
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

            await Task.Run(() => _accountService.OpenAccount(account.Type, account.OwnerFirstName,
                account.OwnerSecondName, account.Sum, User.Identity.Name, _accountIdService));

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
            var result = await Task.Run(() =>
                this.DepositWithdrawOperation(data, (s, d) => s.DepositMoney(d.AccountNumber, d.Sum)));

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
            var result = await Task.Run(() =>
                this.DepositWithdrawOperation(data, (s, d) => s.WithdrawMoney(d.AccountNumber, d.Sum)));

            return result;
        }

        #endregion // !withdraw money.

        #region get account status

        [HttpPost]
        public ActionResult GetAccountStatus() =>
            AccountOperationView(
                operation: nameof(this.GetAccountStatusOperation),
                operationName: "Get account status operation",
                viewName: "AccountStatusOrClose");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> GetAccountStatusOperation(AccountNumberViewModel accountId)
        {
            if (!ModelState.IsValid)
            {
                TempData["isError"] = true;
                return View("AccountStatusOrClose");
            }

            try
            {
                var accountInfo = await Task.Run(() => _accountService.GetAccountStatus(accountId.AccountNumber));
                TempData["isError"] = false;
                ViewBag.IsAccountStatus = true;
                return View(nameof(this.AccountOperations), GetAccountStatusModel(accountInfo));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(accountId.AccountNumber), e.Message);
                TempData["isError"] = true;
                return View("AccountStatusOrClose");
            }           
        }

        #endregion // !get account status.

        #region transfer funds

        [HttpPost]
        public ActionResult TransferFunds() =>
            AccountOperationView(
                operation: nameof(this.TransferFundsOperation),
                operationName: "Transfer funds operation",
                viewName: "TransferFunds");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(AccountServiceException), View = "AccountServiceError")]
        public async Task<ActionResult> TransferFundsOperation(TransferFundsViewModel transferData)
        {
            if (!ModelState.IsValid)
            {
                TempData["isError"] = true;
                return View("TransferFunds");
            }

            try
            {
                await Task.Run(() => _accountService.TransferFunds(
                    sourceAccountId: transferData.FromAccountNumber,
                    destinationAccountId: transferData.ToAccountNumber,
                    transferSum: transferData.Sum));

                TempData["isError"] = false;
                return RedirectToAction(nameof(this.AccountOperations));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                TempData["isError"] = true;
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
                TempData["isError"] = true;
                return View("AccountStatusOrClose");
            }

            try
            {
                await Task.Run(() => _accountService.CloseAccount(accountId.AccountNumber));

                TempData["isError"] = false;
                return RedirectToAction(nameof(this.AccountOperations));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(accountId.AccountNumber), e.Message);
                TempData["isError"] = true;
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

        private ActionResult AccountOperationView(string operation, string operationName, string viewName)
        {
            TempData["isError"] = false;
            ViewBag.Operation = operation;
            ViewBag.OperationName = operationName;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_" + viewName);
            }
                       
            return View(viewName);
        }

        private ActionResult DepositWithdrawOperation(
            AccountOperationViewModel data, 
            Action<IAccountService, AccountOperationViewModel> operation)
        {
            if (!ModelState.IsValid)
            {
                TempData["isError"] = true;
                return View("DepositOrWithdrawMoney");
            }

            try
            {
                operation(_accountService, data);
                TempData["isError"] = false;
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(data.AccountNumber), e.Message);
                TempData["isError"] = true;
                return View("DepositOrWithdrawMoney");
            }

            return RedirectToAction(nameof(this.AccountOperations));
        }

        #endregion // !private.
    }
}