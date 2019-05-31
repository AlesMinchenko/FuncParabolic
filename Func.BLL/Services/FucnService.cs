using AutoMapper;
using Func.BLL.DTO;
using Func.BLL.Infrastructure;
using Func.BLL.Interfaces;
using Func.DAL.Entities;
using Func.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func.BLL.Services
{
    public class FucnService : IFuncService
    {
        IUnitOfWork Database { get; set; }


        public FucnService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public IEnumerable<UserDataDTO> GetUserDatas()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserData, UserDataDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<UserData>, List<UserDataDTO>>(Database.UserDatas.GetAll());
        }
        public UserDataDTO GetUserData(int? id)
        {
            if (id == 0)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserData, UserDataDTO>()).CreateMapper();
            return mapper.Map<UserData, UserDataDTO>(Database.UserDatas.Get(id.Value));

        }
        public void Create(UserDataDTO userData)
        {
            if (userData == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserData>()).CreateMapper();
            var _userData = mapper.Map<UserDataDTO, UserData>(userData);
            Database.UserDatas.Create(_userData);
            Database.Save();
        }
        public void Edit(UserDataDTO userData)
        {
            if (userData == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserData>()).CreateMapper();
            var _userData = mapper.Map<UserDataDTO, UserData>(userData);
            Database.UserDatas.Update(_userData);
            Database.Save();
        }
        public void DeleteU(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id класса", "");
            Database.UserDatas.Delete(id.Value);
            Database.Save();
        }


        public IEnumerable<ChartDTO> GetCharts()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Chart, ChartDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Chart>, List<ChartDTO>>(Database.Charts.GetAll());
        }
        public ChartDTO GetChart(int? id)
        {
            if (id == 0)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Chart, ChartDTO>()).CreateMapper();
            return mapper.Map<Chart, ChartDTO>(Database.Charts.Get(id.Value));

        }
        public void CreateC(ChartDTO chart)
        {
            if (chart == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartDTO, Chart>()).CreateMapper();
            var _chart = mapper.Map<ChartDTO, Chart>(chart);
            Database.Charts.Create(_chart);
            Database.Save();
        }
        public void Edit(ChartDTO chart)
        {
            if (chart == null)
                throw new ValidationException("Не установлено id класса", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserData>()).CreateMapper();
            var _chart = mapper.Map<ChartDTO, Chart>(chart);
            Database.Charts.Update(_chart);
            Database.Save();
        }
        public void DeleteC(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id класса", "");
            Database.Charts.Delete(id.Value);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
