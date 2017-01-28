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

        public void Update(User user)
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
                        return;
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
