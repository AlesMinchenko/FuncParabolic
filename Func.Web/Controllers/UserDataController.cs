using AutoMapper;
using Func.BLL.DTO;
using Func.BLL.Interfaces;
using Func.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Func.Web.Controllers
{
    public class UserDataController : Controller
    {
        IFuncService funcService;
        public UserDataController(IFuncService func)
        {
            funcService = func;
        }
        #region Default Function
        private UserDataViewModel UserData()
        {
            UserDataViewModel data = new UserDataViewModel
            {
                A = 0,
                B = 0,
                C = 0,
                RangeFrom = 0,
                RangeTo = 0,
                Step = 0
            };
            return data;
        }
        #endregion
        public ActionResult Index()
        {
            //IEnumerable<UserDataDTO> userDtos = funcService.GetUserDatas();
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserDataViewModel>()).CreateMapper();
            //var _userDtos = mapper.Map<IEnumerable<UserDataDTO>, List<UserDataViewModel>>(userDtos);
            ViewBag.Block = "none";
            return View(UserData());
        }
        [HttpPost]
        public ActionResult Index(UserDataViewModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {//проверка всех данных_покрытие тестами_тестирование
                    if (data.A == 0)
                    {
                        ViewBag.Message = "Коэффициент 'a' не может быть равен нулю! ";
                        return View(UserData());
                    }
                    else if (data.Step <= 0)
                    {
                        ViewBag.Message = "Коэффициент 'step' не может быть равен или меньше нуля! ";
                        return View(UserData());
                    }
                    else if (data.RangeTo == 0)
                    {
                        ViewBag.Message = "Коэффициент 'rangeTo' не должен быть равен нулю! ";
                        return View(UserData());
                    }
                    else if (data.RangeFrom == 0)
                    {
                        ViewBag.Message = "Коэффициент 'rangeFrom' не должен быть равен нулю! ";
                        return View(UserData());
                    }
                    else
                    {
                        ViewBag.Block = "block";
                        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataViewModel, UserDataDTO>()).CreateMapper();
                        var _chart = mapper.Map<UserDataViewModel, UserDataDTO>(data);
                        funcService.Create(_chart);

                        int count = (-data.RangeFrom) + (data.RangeTo);
                        string[] arrx = new string[count];
                        string[] arry = new string[count];
                        int ii = 0;
                        var x = -(data.B) / 2 * data.A;
                        var y = data.A * Math.Pow(x, 2) + data.B * x + data.C;
                        for (decimal i = data.RangeFrom; i < data.RangeTo; i += data.Step)
                        {
                            if (data.A < 0)
                            {//проверка направления ветвей
                                arrx[ii] = (x + (i * data.A)).ToString() + " ";
                                arry[ii] = ((y - Math.Pow((double)i, 2))).ToString() + " ";
                                ii++;
                            }
                            else
                            {
                                arrx[ii] = (x + (i * data.A)).ToString() + " ";
                                arry[ii] = (-(y - Math.Pow((double)i, 2))).ToString() + " ";
                                ii++;
                            }
                        }
                        if (data.A < 0)
                        {
                            Array.Reverse(arrx);
                        }

                        string dataY = JsonConvert.SerializeObject(arrx, Formatting.None);
                        ViewBag.DataY = new HtmlString(dataY);

                        string dataX = JsonConvert.SerializeObject(arry, Formatting.None);
                        ViewBag.DataX = new HtmlString(dataX);
                        return View(data);
                    }
                }
                ViewBag.Block = "none";
            }
            catch (Exception p)
            {
                ViewBag.Message = p.Message;
                ViewBag.Block = "none";
            }

            return View(data);
        }

        [HttpGet]
        public ActionResult ShowChart()
        {
            ViewBag.G = new SelectList(funcService.GetCharts(), "Id", "Name");

            return View(UserData());
        }
        [HttpPost]
        public ActionResult ShowChart(int? G)
        {
            ViewBag.G = new SelectList(funcService.GetCharts(), "Id", "Name");
            if (G == null)
            {
                return View(UserData());

            }
            UserDataViewModel userData = null;
            try
            {
                var chart = funcService.GetChart(G);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartDTO, ChartViewModel>()).CreateMapper();
                var _chart = mapper.Map<ChartDTO, ChartViewModel>(chart);


                var _userData = funcService.GetUserData(_chart.UserData.Id);
                var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserDataViewModel>()).CreateMapper();
                userData = _mapper.Map<UserDataDTO, UserDataViewModel>(_userData);

                int count = (-userData.RangeFrom) + (userData.RangeTo);
                string[] arrx = new string[count];
                string[] arry = new string[count];
                int ii = 0;
                var x = -(userData.B) / 2 * userData.A;
                var y = userData.A * Math.Pow(x, 2) + userData.B * x + userData.C;
                for (decimal i = userData.RangeFrom; i < userData.RangeTo; i += userData.Step)
                {
                    if (userData.A < 0)
                    {//проверка направления ветвей
                        arrx[ii] = (x + (i * userData.A)).ToString() + " ";
                        arry[ii] = ((y - Math.Pow((double)i, 2))).ToString() + " ";
                        ii++;
                    }
                    else
                    {
                        arrx[ii] = (x + (i * userData.A)).ToString() + " ";
                        arry[ii] = (-(y - Math.Pow((double)i, 2))).ToString() + " ";
                        ii++;
                    }
                }
                if (userData.A < 0)
                {
                    Array.Reverse(arrx);
                }

                string dataY = JsonConvert.SerializeObject(arrx, Formatting.None);
                ViewBag.DataY = new HtmlString(dataY);

                string dataX = JsonConvert.SerializeObject(arry, Formatting.None);
                ViewBag.DataX = new HtmlString(dataX);
            }
            catch (Exception p)
            {
                ViewBag.message = p.Message;
            }
            return View(userData);

        }
        [HttpPost]
        public ActionResult Save(ChartViewModel chart)
        {
            if (chart.Name == null)
            {
                ViewBag.Message = "Данные графика не введены. График не сохранен";

                return View("Index", UserData());
            }
            //int id = (from m in db.UserDatas select m.Id).ToList().Last();
            int id = (from m in funcService.GetUserDatas() select m.Id).ToList().Last();

            var c = funcService.GetUserData(id);
            var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserDataViewModel>()).CreateMapper();
            chart.UserData = _mapper.Map<UserDataDTO, UserDataViewModel>(c);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartViewModel, ChartDTO>()).CreateMapper();
            var _chart = mapper.Map<ChartViewModel, ChartDTO>(chart);

            funcService.CreateC(_chart);

            return View();
        }


        #region Дописать 
        //public ActionResult Details(int? id)
        //{
        //    if (id == 0)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var _userDtos = funcService.GetUserData(id);
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserDataViewModel>()).CreateMapper();
        //    var userDtos = mapper.Map<UserDataDTO, UserDataViewModel>(_userDtos);

        //    if (userDtos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userDtos);
        //}

        //// GET: Classes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Subject")]UserDataViewModel userData)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataViewModel, UserDataDTO>()).CreateMapper();
        //        var _userData = mapper.Map<UserDataViewModel, UserDataDTO>(userData);

        //        funcService.Create(_userData);
        //        return RedirectToAction("Index");
        //    }

        //    return View(userData);
        //}

        //public ActionResult Edit(int id)
        //{
        //    if (id == 0)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var userDtos = funcService.GetUserData(id);
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserDataViewModel>()).CreateMapper();
        //    var _userDtos = mapper.Map<UserDataDTO, UserDataViewModel>(userDtos);


        //    if (_userDtos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(_userDtos);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit( UserDataViewModel userData)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataViewModel, UserDataDTO>()).CreateMapper();
        //        var _userData = mapper.Map<UserDataViewModel, UserDataDTO>(userData);
        //        funcService.Edit(_userData);

        //        return RedirectToAction("Index");
        //    }
        //    return View(userData);
        //}

        //public ActionResult Delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var userDtos = funcService.GetUserData(id);
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserDataViewModel>()).CreateMapper();
        //    var _userDtos = mapper.Map<UserDataDTO, UserDataViewModel>(userDtos);
        //    if (_userDtos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(_userDtos);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var userDtos = funcService.GetUserData(id);
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDataDTO, UserDataViewModel>()).CreateMapper();
        //    var _userDtos = mapper.Map<UserDataDTO, UserDataViewModel>(userDtos);

        //    funcService.DeleteU(_userDtos.Id);
        //    return RedirectToAction("Index");
        //}
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                funcService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}