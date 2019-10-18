using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;


namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private CRUDContext dbContext;
    
        // here we can "inject" our context service into the constructor
        public HomeController(CRUDContext context)
        {
            dbContext = context;
        }
    
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes.ToList();
            ViewBag.List = AllDishes;
            
            return View("Index");
        }
        
    
        [HttpGet]
        [Route("New")]
        public IActionResult New()
        {
            List<Dish> AllDishes = dbContext.Dishes.ToList();
                        
            return View("New");
        }
        
        [HttpPost("create")]
        public IActionResult CreateDish(Dish NewDish)
        {
        if(ModelState.IsValid)
            {
                Dish newDish = new Dish()
                {
                    Name = NewDish.Name,
                    Chef = NewDish.Chef,
                    Tastiness = NewDish.Tastiness,
                    Calories = NewDish.Calories,
                    Description = NewDish.Description,
                };
            dbContext.Add(newDish);
            dbContext.SaveChanges();
            return Redirect ("/");
            }
            else
            {
                return View ("New");
            }
        }

        // Inside HomeController

        [HttpGet("{dishId}")]
        public IActionResult DisplayDish(int dishId)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            // Then we may modify properties of this tracked model object
                       
            // Other code - Add a return path
            return View("DishDetails", RetrievedDish);
        }
        [HttpGet("edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            // Then we may modify properties of this tracked model object
                       
            // Other code - Add a return path
            return View("Edit", RetrievedDish);
        }

        [HttpPost("/update/{dishId}")]
        public IActionResult UpdateDish(Dish editdish, int dishId)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            // Then we may modify properties of this tracked model object
            RetrievedDish.Name = editdish.Name;
            RetrievedDish.Chef = editdish.Chef;
            RetrievedDish.Calories = editdish.Calories;
            RetrievedDish.Tastiness = editdish.Tastiness;
            RetrievedDish.Description = editdish.Description;
            RetrievedDish.UpdatedAt = DateTime.Now;
           
            // Finally, .SaveChanges() will update the DB with these new values
            dbContext.SaveChanges();            
            // Other code - Add a return path
            return Redirect("/");
        }

        // Inside HomeController
        [HttpGet("delete/{dishId}")]
        public IActionResult DeleteDish(int dishId)
        {
            // Like Update, we will need to query for a single user from our Context object
            Dish RetrievedDish = dbContext.Dishes.SingleOrDefault(dish => dish.DishId == dishId);
            
            // Then pass the object we queried for to .Remove() on Users
            dbContext.Dishes.Remove(RetrievedDish);
            
            // Finally, .SaveChanges() will remove the corresponding row representing this User from DB 
            dbContext.SaveChanges();
            // Other code
            return Redirect("/");

        }

        // public IActionResult Privacy()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }

        // [HttpPost("create")]
        // public IActionResult CreateDish(Dish newDish)
        // {
        //     // We can take the new object created from a form submission
        //     // And pass this object to the .Add() method
        //     dbContext.Add(newDish);
        //     Dish TestDish = new Dish()
        //     {
        //     Name = "Hot Dog",
        //     Chef = "Swedish_Chef",
        //     Tastiness = 3,
        //     Calories = 250,
        //     Description = "This is a hot dog.",
        //     };
        //     dbContext.SaveChanges();
        //     System.Console.WriteLine("New dish created.");
            
        //     // Other code - Add a return path
        //     return Redirect("Index");
        // }
    }
}
