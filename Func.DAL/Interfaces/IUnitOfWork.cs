using Func.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Chart> Charts { get; }
        IRepository<UserData> UserDatas { get; }
        void Save();
    }
}
