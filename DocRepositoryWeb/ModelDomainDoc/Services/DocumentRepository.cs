using DomainModel.Helpers;
using ModelDomainDoc.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDomainDoc.Services
{
    public class DocumentRepository : IDocumentRepository
    {
        public Document Create()
        {
            return new Document() { Id = 0 };
        }

        public void Update(Document doc)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(doc);
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

        public IEnumerable<Document> GetAll()
        {
            var documents = new List<Document>();
            using (var session = NHibernateHelper.OpenSession())
            {
                var criterion = session.CreateCriteria(typeof(Document));
                documents = criterion.List<Document>().ToList();
            }
            return documents;
        }

        public IEnumerable<Document> SearchByName(string name)
        {
            var documents = new List<Document>();
            using (var sesson = NHibernateHelper.OpenSession())
            {
                documents = sesson.CreateCriteria<Document>()
                    .Add(Restrictions.Like("Name", $"%{name}%")).List<Document>().ToList();
            }
            return documents;   
        }
    }
}
