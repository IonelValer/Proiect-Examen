using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domnita_Ionel_Proiect.Models;
using Domnita_Ionel_Proiect.Data;
using Domnita_Ionel_Proiect.Models.StoreViewModels;
using Microsoft.EntityFrameworkCore; 

namespace Domnita_Ionel_Proiect.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreContext _context;
        public HomeController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
            from order in _context.Orders
            group order by order.OrderDate into dateGroup
            select new OrderGroup()
            {
                OrderDate = dateGroup.Key,
                PhoneCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Chat()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
