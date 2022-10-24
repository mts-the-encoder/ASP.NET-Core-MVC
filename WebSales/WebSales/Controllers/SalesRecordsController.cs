using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSales.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace WebSales.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordsService _salesRecordsService;

        public SalesRecordsController(SalesRecordsService salesRecordsService)
        {
            _salesRecordsService = salesRecordsService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            minDate ??= new DateTime(DateTime.Now.Year, 1, 1);

            maxDate ??= DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var res = _salesRecordsService.FindByDate(minDate, maxDate);
            return View(res);
        }
        public async Task<IActionResult> GroupingSearch(DateTime? minDate,DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year,1,1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordsService.FindByDateGroupingAsync(minDate,maxDate);
            return View(result);
        }
    }
}
