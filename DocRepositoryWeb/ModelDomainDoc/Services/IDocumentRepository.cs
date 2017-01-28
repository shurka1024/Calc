using ModelDomainDoc.Models;
using System.Collections;
using System.Collections.Generic;

namespace ModelDomainDoc.Services
{
    public interface IDocumentRepository : IEntitiRepository<Document>
    {
        IEnumerable<Document> SearchByName(string name);
    }
}
