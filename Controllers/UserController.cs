using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using newProject.Models;

namespace newProject.Controllers
{
    public class UserController : Controller
    {
        private ActivityContext _context;
        public UserController(ActivityContext context)
		{
			_context = context;
		}

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUserModel newUser) {
            if (ModelState.IsValid) {
                //check to see whether a user is already registered with this email
                var existing = _context.Users.Where(u => u.Email == newUser.Email).SingleOrDefault();
                if (existing == null) {
                    //hash pw
                    PasswordHasher<RegisterUserModel> Hasher = new PasswordHasher<RegisterUserModel>();
                    string hashedp = Hasher.HashPassword(newUser, newUser.Password);

                    //create user object with hashed password
                    User newU = new User {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Email = newUser.Email,
                        Password = hashedp
                    };

                    //add new user to db
                    _context.Users.Add(newU);
                    _context.SaveChanges();

                    //set session variables
                    HttpContext.Session.SetInt32("UserId", newU.UserId);
                    HttpContext.Session.SetString("Username", newU.FirstName);
                    return RedirectToAction("Dashboard", "Event");
                }
                else {
                    ViewBag.errors = "A user with that email aready exists.";
                    return View("Index");
                }
            }
            //return view to show asp-validation errors
            return View("Index");
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password) {
            //get the user with the submitted email
            var existing = _context.Users.Where(u => u.Email == email).SingleOrDefault();
            if (existing != null) {
                var Hasher = new PasswordHasher<User>();
                //check the submitted password against the saved hashed password
                if (0 != Hasher.VerifyHashedPassword(existing, existing.Password, password)) {
                    HttpContext.Session.SetInt32("UserId", existing.UserId);
                    HttpContext.Session.SetString("Username", existing.FirstName);
                    return RedirectToAction("Dashboard", "Event");
                }
                else {
                    ViewBag.pwerror = "Password is incorrect.";
                    return View("Index");
                }
            }
            else {
                ViewBag.emailerror = "This email has not been registered.";
                return View("Index");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}







