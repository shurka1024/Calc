using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Services
{
    public class OperationRepository : IOperationRepository
    {
        private CalcContext db { get; set; }
        public Operation Create()
        {
            using (var db = new CalcContext())
            {
                return db.Operations.Create();
            }
        }

        public bool Delete(int Id)
        {
            using (var db = new CalcContext())
            {
                var item = Load(Id);
                if (item == null)
                    return false;
                db.Operations.Remove(item);
                db.SaveChanges();
                return true;
            }
        }

        public Operation Load(int Id)
        {
            using (var db = new CalcContext())
            {
                return db.Operations.FirstOrDefault(o => o.Id == Id);
                throw new NotImplementedException();
            }
        }

        public void Update(Operation operResult)
        {
            using (var db = new CalcContext())
            {
                db.Entry<Operation>(operResult).State = EntityState.Modified; // Привязываем к текущему контексту
                db.SaveChanges();
            }
        }

        public IEnumerable<Operation> GetAll()
        {
            var operations = new List<Operation>();
            using (var db = new CalcContext())
            {
                operations = db.Operations.ToList();
            }
            return operations;
        }
    }
}