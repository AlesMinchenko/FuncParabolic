using Func.DAL.EF;
using Func.DAL.Entities;
using Func.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private UserContext db;
        private ChartRepository chartRepository;
        private UserDataRepository userDataRepository;

        public EFUnitOfWork()
        {
            db = new UserContext();
        }
        public IRepository<Chart> Charts
        {
            get
            {
                if (chartRepository == null)
                    chartRepository = new ChartRepository(db);
                return chartRepository;
            }
        }

        public IRepository<UserData> UserDatas
        {
            get
            {
                if (userDataRepository == null)
                    userDataRepository = new UserDataRepository(db);
                return userDataRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
