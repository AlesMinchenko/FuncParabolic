using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Func.DAL.Entities;
using System.Linq;
using Func.DAL.Interfaces;

namespace UnitTest.Stubs
{

    public class StubChartRepository : IRepository<Chart>
    {
        readonly List<Chart> _charts;

        public StubChartRepository()
        {
            _charts = new List<Chart>()
            {
                new Chart(){ Id = 1, Name = "ChartName1", UserData = new UserData() },
                new Chart(){ Id = 2, Name = "ChartName2", UserData = new UserData()},
                new Chart(){ Id = 3, Name = "ChartName3", UserData = new UserData()},

            };
        }

        public IEnumerable<Chart> GetAll()
        {
            return _charts.ToList();
        }

        public Chart Get(int id)
        {
            return _charts.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Chart chart)
        {
            _charts.Add(chart);
        }

        public void Update(Chart chart)
        {
            var tempChart = _charts.FirstOrDefault(x => x.Id == chart.Id);
            if (tempChart != null)
            {
                tempChart.Id = chart.Id;
                tempChart.Name = chart.Name;
                tempChart.UserData = chart.UserData;
            }
        }

        public IEnumerable<Chart> Find(Func<Chart, Boolean> predicate)
        {
            return _charts.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Chart chart = _charts.FirstOrDefault(x => x.Id == id);
            if (chart != null)
            {
                _charts.Remove(chart);
            }
        }
    }
}
