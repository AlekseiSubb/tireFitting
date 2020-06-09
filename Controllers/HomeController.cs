using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using tireFitting.Models;

namespace tireFitting.Controllers
{
    public class HomeController : Controller
    {
        TireFittingContext db;

        public HomeController(TireFittingContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var orders = db.Orders.Include(p => p.People);
            return View(orders);
        }

        public IActionResult Contacts()
        {
            return View();
        }
        
    }
}
