using System;
using System.Web.Security;
using BLL.Interface.Services;

namespace PL.Web.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private static readonly IBankService BankService;

        static CustomRoleProvider()
        {
            BankService = (IBankService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IBankService));
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool result = false;

            var user = BankService.GetUserInfo(username);
            if (!ReferenceEquals(user, null))
            {
                if (string.Equals(user.Role, roleName, StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                }
            }

            return result;
        }

        public override string[] GetRolesForUser(string username)
        {
            var roles = new string[] { };

            var user = BankService.GetUserInfo(username);
            if (!ReferenceEquals(user, null))
            {
                roles = new[] { user.Role };
            }

            return roles;
        }

        #region stubs

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        #endregion // !stubs.
    }
}