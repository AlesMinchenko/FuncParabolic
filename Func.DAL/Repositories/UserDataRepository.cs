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
    public class UserDataRepository : IRepository<UserData>
    {
        private UserContext db;

        public UserDataRepository(UserContext context)
        {
            this.db = context;
        }

        public IEnumerable<UserData> GetAll()
        {
            return db.UserDatas;
        }

        public UserData Get(int id)
        {
            return db.UserDatas.Find(id);
        }

        public void Create(UserData data)
        {
            db.UserDatas.Add(data);
        }

        public void Update(UserData data)
        {
            db.Entry(data).State = EntityState.Modified;
        }

        public IEnumerable<UserData> Find(Func<UserData, Boolean> predicate)
        {
            return db.UserDatas.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            UserData data = db.UserDatas.Find(id);
            if (data != null)
                db.UserDatas.Remove(data);
        }
    }
}
