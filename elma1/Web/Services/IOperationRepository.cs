using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IOperationRepository : IEntityRepository<Operation>
    {
    }
}
