using Func.DAL.EF;
using Func.DAL.Entities;
using Func.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func.DAL.Repositories
{
    public class ChartRepository : IRepository<Chart>
    {
        private UserContext db;

        public ChartRepository(UserContext context)
        {
            this.db = context;
        }

        public IEnumerable<Chart> GetAll()
        {
            return db.Charts;
        }

        public Chart Get(int id)
        {
            return db.Charts.Find(id);
        }

        public void Create(Chart chart)
        {
            db.Charts.Add(chart);
        }

        public void Update(Chart chart)
        {
            db.Entry(chart).State = EntityState.Modified;
        }

        public IEnumerable<Chart> Find(Func<Chart, Boolean> predicate)
        {
            return db.Charts.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Chart chart = db.Charts.Find(id);
            if (chart != null)
                db.Charts.Remove(chart);
        }
    }
}
