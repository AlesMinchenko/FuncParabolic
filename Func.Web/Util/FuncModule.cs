using Func.BLL.Interfaces;
using Func.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Func.Web.Util
{
    public class FuncModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFuncService>().To<FucnService>();
        }
    }
}