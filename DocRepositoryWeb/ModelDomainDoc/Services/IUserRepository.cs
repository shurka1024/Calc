using ModelDomainDoc.Models;

namespace ModelDomainDoc.Services
{
    public interface IUserRepository : IEntitiRepository<User>
    {
        User GetUserById(int id);
    }
}
