using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingPlannerContext dbContext;
    
        // here we can "inject" our context service into the constructor
        public HomeController(WeddingPlannerContext context)
        {
            dbContext = context;
        }
    
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<User> AllUsers = dbContext.Users.ToList();
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
