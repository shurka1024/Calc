using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Services
{
    public interface IEntityRepository<T>  where T:class
    {
        T Create();
        T Load(int Id);

        bool Delete(int Id);

        void Update(T operResult);

        IEnumerable<T> GetAll();

    }
}