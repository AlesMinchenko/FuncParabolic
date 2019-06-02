using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Func.DAL.Entities;
using System.Linq;

namespace UnitTest.Stubs
{
    public class StubUserDataRepository1
    {
        readonly List<UserData> userDatas;

        public StubUserDataRepository1()
        {
            userDatas = new List<UserData>()
            {
                new UserData(){A = 1, B = 3, C =3, Charts = new List<Chart>(), Id = 1, RangeFrom = 1, RangeTo = 4, Step= 1},
                new UserData(){A = 2, B = 5, C =2, Charts = new List<Chart>(), Id = 2, RangeFrom = 1, RangeTo = 4, Step= 1},
                new UserData(){A = 3, B = 4, C =1, Charts = new List<Chart>(), Id = 3, RangeFrom = 1, RangeTo = 4, Step= 1},

            };
        }

        public IEnumerable<UserData> GetAll()
        {
            return userDatas.ToList();
        }

        public UserData Get(int id)
        {
            return userDatas.FirstOrDefault(p => p.Id == id);
        }

        public void Create(UserData userData)
        {
            userDatas.Add(userData);
        }

        public void Update(UserData userData)
        {
            var tempUserdata = userDatas.FirstOrDefault(x => x.Id == userData.Id);
            if (tempUserdata != null)
            {
                tempUserdata.A = userData.A;
                tempUserdata.B = userData.B;
                tempUserdata.C = userData.C;
                tempUserdata.Id = userData.Id;
                tempUserdata.Charts = userData.Charts;
                tempUserdata.RangeFrom = userData.RangeFrom;
                tempUserdata.RangeTo = userData.RangeTo;
                tempUserdata.Step = userData.Step;
            }
        }

        public IEnumerable<UserData> Find(Func<UserData, Boolean> predicate)
        {
            return userDatas.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            UserData userData = userDatas.FirstOrDefault(x => x.Id == id);
            if (userData != null)
            {
                userDatas.Remove(userData);
            }
        }
    }
}
