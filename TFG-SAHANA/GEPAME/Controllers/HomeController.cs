using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GEPAME.Models;
using Microsoft.EntityFrameworkCore;
using GEPAME.AppCode.LN;
using Microsoft.AspNetCore.Authorization;

namespace GEPAME.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly GEPAMEContext _context;

        public HomeController(GEPAMEContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gEPAMEContext = new LN_Utilidades().GetTopIncidences(_context);
            ViewData["Vehicle"] = new LN_Utilidades().GetActiveVehicles(_context);
            return View(await gEPAMEContext.ToListAsync());
        }

        public async Task<IActionResult> IndexVehicle()
        {
            var gEPAMEContext = new LN_Utilidades().GetActiveVehicles(_context);
            return PartialView(await gEPAMEContext.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
