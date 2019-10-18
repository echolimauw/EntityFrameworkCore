using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ActivityCenter.Models;
using Microsoft.AspNetCore.Http;

namespace ActivityCenter.Controllers
{
    public class HomeController : Controller
    {
        private ACContext dbContext;
    
        // here we can "inject" our context service into the constructor
        public HomeController(ACContext context)
        {
            dbContext = context;
        }
    

        /////////////////////////////////////
        ///     All RENDER ROUTES HERE    ///
        /////////////////////////////////////


        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // List<User> AllUsers = dbContext.Users.ToList();
            if(HttpContext.Session.GetInt32("Id") == null)
            {
                return View("LogReg");
            }
            else
            {
            return View("Home");
            }
        }
        
        [HttpGet("LogRegRender")]
        public IActionResult LogRegRender()
        {
            if(HttpContext.Session.GetInt32("Id") == null)
            {
                return View("LogReg");
            }
            else
            {
            return View("LogReg");
            }
        }

        [HttpGet("Home")]
        public IActionResult Home()
        {   
            if(HttpContext.Session.GetInt32("Id") == null)
            {
                return View("LogReg");
            }
            else
            {
            int? UserId = HttpContext.Session.GetInt32("Id");
            User LoggedInUser = dbContext.Users.FirstOrDefault(u => u.UserId == UserId);
            ViewBag.SUser = LoggedInUser;
            List<Plan> Scheduled = dbContext.Plans.ToList();
            List<User> AllUsers = dbContext.Users.ToList();
            ViewBag.scheduled = Scheduled;
            ViewBag.allusers = AllUsers;
            return View("Home");
            }
        }
    
    
        /////////////////////////////////////
        ///    All PROCESS ROUTES HERE    ///
        /////////////////////////////////////


        [HttpPost("Register")]
        public IActionResult Register(User NewUser)
        {
            if(ModelState.IsValid)
            {
                // If a User exists with provided email
                if(dbContext.Users.Any(u => u.Email == NewUser.Email))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View ("Index");                
                    // You may consider returning to the View at this point
                }
                else
                {
                    User newUser = new User()
                    {
                        Name = NewUser.Name,
                        Email = NewUser.Email,
                        Password = NewUser.Password,
                    };
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    return Redirect ("LoginRender");
                }
            }
            else
            {
                System.Console.WriteLine("This form was not valid.");
                return View ("Index");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                System.Console.WriteLine("Login Model State is Valid");
                // If inital ModelState is valid, query for a user with provided email
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
                // If no user exists with provided email
                if(userInDb == null)
                {
                    System.Console.WriteLine("Login Email not found in DB.");
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Login");
                }
                
                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();
                
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);
                
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    System.Console.WriteLine("Password check failed.");
                     // Add an error to ModelState and return to View!
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Login");
                }
                
                else
                {  
                System.Console.WriteLine("Login form passed. Requesting Session");
                    
                    HttpContext.Session.SetInt32("Id", userInDb.UserId);
                    System.Console.WriteLine("Your Login works!"); 
                    return Redirect ("Home");
                }
            }
            else
            {
                System.Console.WriteLine("Original model requirements.");
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Login");
            }
        }
        
        public IActionResult Privacy()
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
