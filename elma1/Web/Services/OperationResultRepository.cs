using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Services
{
    public class OperationResultRepository : IOperationResultRepository
    {
        private CalcContext db { get; set; }
        public OperationResult Create()
        {
            using (var db = new CalcContext())
            {
                return db.OperationResults.Create();
            }
        }

        public bool Delete(int Id)
        {
            using (var db = new CalcContext())
            {
                var item = Load(Id);
                if (item == null)
                    return false;
                db.OperationResults.Remove(item);
                db.SaveChanges();
                return true;
            }
        }

        public OperationResult Load(int Id)
        {
            using (var db = new CalcContext())
            {
                return db.OperationResults.FirstOrDefault(o => o.Id == Id);
                throw new NotImplementedException();
            }
        }

        public void Update(OperationResult operResult)
        {
            using (var db = new CalcContext())
            {
                db.Entry<OperationResult>(operResult).State = operResult.Id == 0 ? EntityState.Added : EntityState.Modified; // Привязываем к текущему контексту
                db.SaveChanges();
            }
        }

        public IEnumerable<OperationResult> GetAll()
        {
            var operations = new List<OperationResult>();
            using (var db = new CalcContext())
            {
                operations = db.OperationResults
                    .Include("Operation")
                    .AsNoTracking()         // Не следить за изменениями. Снижает нарузку
                    .ToList();

                // Вытаскивает все данные, а потом уже фильтрует
                //IEnumerable<OperationResult> ops = db.OperationResults;
                //var result = ops.Where(o => o.Id > 3);

                // Данные фильтрует в БД
                //IQueryable<OperationResult> qops = db.OperationResults;
                //var qresult = qops.Where(o => o.Id > 3);
            }
            return operations;
        }

        public IEnumerable<OperationResult> GetByFilter(int MinExecTime_ms)
        {
            var operations = new List<OperationResult>();
            using (var db = new CalcContext())
            {
                //Данные фильтрует в БД
                IQueryable<OperationResult> qops = db.OperationResults.AsNoTracking().Include("Operation");
                operations = qops.Where(o => o.ExecTime_ms > MinExecTime_ms).ToList();
            }
            return operations;
        }

        public Operation FindOperByName(string Name)
        {
            Operation oper;
            using (var db = new CalcContext())
            {
                oper = db.Operations.Where(o => o.Name == Name).First();
            }
            return oper;
        }
    }
}