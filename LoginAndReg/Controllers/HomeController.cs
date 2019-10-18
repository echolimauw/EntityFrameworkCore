using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using LoginAndReg.Models;

namespace LoginAndReg.Controllers
{
    public class HomeController : Controller
    {
        private LogRegContext dbContext;
    
        // here we can "inject" our context service into the constructor
        public HomeController(LogRegContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost("register")]
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
                        FirstName = NewUser.FirstName,
                        LastName = NewUser.LastName,
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

        [HttpGet("LoginRender")]
        public IActionResult LoginRender()
        {
            return View("Login");
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
                    return Redirect ("Success");
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

        [HttpGet("Success")]
        public IActionResult Success()
        {   
            if(HttpContext.Session.GetInt32("Id") == null)
            {
                return View("Index");
            }
            else
            {
            int? UserId = HttpContext.Session.GetInt32("Id");
            User LoggedInUser = dbContext.Users.FirstOrDefault(u => u.UserId == UserId);
            ViewBag.SUser = LoggedInUser;
            return View("Success");
            }
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
        HttpContext.Session.Clear();
        return RedirectToAction ("Index");
        }
    }
}
