using ModelDomainDoc.Models;

namespace ModelDomainDoc.Services
{
    public interface IUserRepository : IEntitiRepository<User>
    {
        User GetUserById(int id);
        User GetUserByLogin(string login);
        bool CheckUser(string login, string password);
    }
}
