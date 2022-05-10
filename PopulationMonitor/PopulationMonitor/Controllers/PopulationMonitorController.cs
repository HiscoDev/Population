using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PopulationMontior.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PopulationMonitor.Controllers
{
    public class PopulationMonitorController : Controller
    {
        private readonly IPopulationService _populationService;
        public PopulationMonitorController(IPopulationService populationService)
        {
            _populationService = populationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoadPopulationData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = int.TryParse(Request.Form["start"].FirstOrDefault(), out int st) ? st : 0;
                var length = int.TryParse(Request.Form["length"].FirstOrDefault(), out int lt) ? lt: 0;
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length;
                int skip = start;
                int recordsTotal = 0;
                var populationData = _populationService.GetPopulationDataWithSearch(start, length, sortColumn, sortColumnDirection, searchValue);
                recordsTotal = populationData.Count();
                var data = populationData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(PopulationData populationData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _populationService.InsertPopulationData(populationData);
                    return RedirectToAction("Index", "PopulationMonitor");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return RedirectToAction("Index", "Population");
                }

                var record = _populationService.GetPopulationDataById(id);

                return View("Edit", record);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(PopulationData populationData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _populationService.UpdatePopulationData(populationData);
                    return RedirectToAction("Index", "PopulationMonitor");
                }
                return View("Edit", populationData);
            }
            catch (Exception ex)
            {
                return View("Edit", populationData);
            }
        }

        [HttpPost]
        public IActionResult DeletePopulationData(int id)
        {
            try
            {
                if (id == 0)
                {
                    return RedirectToAction("Index", "PopulationMonitor");
                }
                var result = _populationService.DeletePopulationData(id);
                return StatusCode(200);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "PopulationMonitor");
            }
        }
    }
}
