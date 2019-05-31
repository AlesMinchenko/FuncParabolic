using Func.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func.BLL.Interfaces
{
    public interface IFuncService
    {
        IEnumerable<UserDataDTO> GetUserDatas();
        UserDataDTO GetUserData(int? id);
        void Create(UserDataDTO userData);
        void Edit(UserDataDTO userData);
        void DeleteU(int? id);

        IEnumerable<ChartDTO> GetCharts();
        ChartDTO GetChart(int? id);
        void CreateC(ChartDTO chart);
        void Edit(ChartDTO chart);
        void DeleteC(int? id);


        void Dispose();

    }
}
