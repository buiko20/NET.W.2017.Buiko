using DAL.Interface.DTO;

namespace DAL.Interface
{
    public interface IUserRepository
    {
        void AddUser(DalUser user);

        DalUser GetUser(string email);
    }
}
