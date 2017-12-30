using System;
using System.Data.Entity;
using System.Linq;
using DAL.Interface;
using DAL.Interface.DTO;
using ORM.Model;

namespace DAL.EF
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            if (ReferenceEquals(dbContext, null))
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public void AddUser(DalUser user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException(nameof(user));
            }

            _dbContext.Set<User>().Add(ToOrmUser(user));
            _dbContext.SaveChanges();
        }

        public DalUser GetUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"{nameof(email)} is invalid.", nameof(email));
            }

            var ormUser = _dbContext.Set<User>()
                .FirstOrDefault(user => user.Email == email);

            if (ReferenceEquals(ormUser, null))
            {
                return null;
            }

            return ToDalUser(ormUser);
        }

        private static User ToOrmUser(DalUser dalUser) =>
            new User
            {
                Email = dalUser.Email,
                Password = dalUser.Password
            };

        private static DalUser ToDalUser(User user) =>
            new DalUser
            {
                Email = user.Email,
                Password = user.Password
            };
    }
}
