using AutoMapper;
using Func.BLL.DTO;
using Func.BLL.Interfaces;
using Func.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Func.Web.Controllers
{
    public class ChartController : Controller
    {
        IFuncService funcService;
        public ChartController(IFuncService func)
        {
            funcService = func;
        }
        public ActionResult Index()
        {
            ViewBag.Block = "none";
            IEnumerable<ChartDTO> chartDtos = funcService.GetCharts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartDTO, ChartViewModel>()).CreateMapper();
            var _chartDtos = mapper.Map<IEnumerable<ChartDTO>, List<ChartViewModel>>(chartDtos);

            return View(_chartDtos);
        }

        public ActionResult Details(int? id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _chartDtos = funcService.GetChart(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartDTO, ChartViewModel>()).CreateMapper();
            var chartDtos = mapper.Map<ChartDTO, ChartViewModel>(_chartDtos);

            if (chartDtos == null)
            {
                return HttpNotFound();
            }
            return View(chartDtos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChartViewModel chart)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartViewModel, ChartDTO>()).CreateMapper();
                var _chart = mapper.Map<ChartViewModel, ChartDTO>(chart);

                funcService.CreateC(_chart);
                return RedirectToAction("Index");
            }

            return View(chart);
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chartDtos = funcService.GetChart(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartDTO, ChartViewModel>()).CreateMapper();
            var _chartDtos = mapper.Map<ChartDTO, ChartViewModel>(chartDtos);


            if (_chartDtos == null)
            {
                return HttpNotFound();
            }
            return View(_chartDtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChartViewModel chartView)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartViewModel, ChartDTO>()).CreateMapper();
                var _chartDtos = mapper.Map<ChartViewModel, ChartDTO>(chartView);
                funcService.Edit(_chartDtos);

                return RedirectToAction("Index");
            }
            return View(chartView);
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chartDtos = funcService.GetChart(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartDTO, ChartViewModel>()).CreateMapper();
            var _chartDtos = mapper.Map<ChartDTO, ChartViewModel>(chartDtos);
            if (_chartDtos == null)
            {
                return HttpNotFound();
            }
            return View(_chartDtos);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var chartDtos = funcService.GetChart(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartDTO, ChartViewModel>()).CreateMapper();
            var _chartDtos = mapper.Map<ChartDTO, ChartViewModel>(chartDtos);

            funcService.DeleteC(_chartDtos.Id);
            return RedirectToAction("Index");
        }

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