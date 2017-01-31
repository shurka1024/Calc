using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDomainDoc.Models;
using DomainModel.Helpers;

namespace ModelDomainDoc.Services
{
    public class UserRepository : IUserRepository
    {
        public User Create()
        {
            return new User { Id = 0 };
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            using (var session = NHibernateHelper.OpenSession())
            {
                var criterion = session.CreateCriteria(typeof(User));
                users = criterion.List<User>().ToList();
            }
            return users;
        }

        public User GetUserById(int id)
        {
            return GetAll().Where(u => u.Id == id).FirstOrDefault();
        }

        public string Update(User user)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(user);
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        return ex.Message;
                    }
                    transaction.Commit();
                    return "Пользователь успешно добавлен";
                }
            }
        }

        public bool CheckUser(string login, string password)
        {
            int userscount = 0;
            using (var session = NHibernateHelper.OpenSession())
            {
                var criterion = session.CreateCriteria(typeof(User));
                userscount = criterion.List<User>().Where(u => u.Login == login).Where(u => u.Password == password).Count();
            }
            if (userscount > 0)
                return true;
            else
                return false;
        }

        public User GetUserByLogin(string login)
        {
            return GetAll().Where(u => u.Login == login).FirstOrDefault();
        }
    }
}
