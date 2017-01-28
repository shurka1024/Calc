using System.Collections.Generic;

namespace ModelDomainDoc.Services
{
    public interface IEntitiRepository<T> where T:class
    {
        T Create();
        //T Load(int Id);
        //bool Delete(int Id);
        string Update(T clobject);
        IEnumerable<T> GetAll();
    }
}
