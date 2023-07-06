using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Models;
using ProjetoMVC.Services;

namespace ProjetoMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? initial, DateTime? final)
        {
            if(!initial.HasValue)
            {
                initial = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if(!final.HasValue)
            {
                final = DateTime.Now;
            }
            ViewData["initial"] = initial.Value.ToString("yyyy-MM-dd");
            ViewData["final"] = final.Value.ToString("yyyy-MM-dd");
            ICollection<SalesRecord> sales = await _salesRecordService.FindByDateAsync(initial, final);
            return View();
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
