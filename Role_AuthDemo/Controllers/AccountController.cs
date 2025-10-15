using Microsoft.AspNetCore.Mvc;
using Role_AuthDemo.Models;

namespace Role_AuthDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly LeaveDbContext context;

        public AccountController(LeaveDbContext leaveRequest)
        {
            context = leaveRequest;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Shows us the sign up page
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        //this method takes in the whole user model class (that has been populated by the user as a parameter)
        public IActionResult Signup(User model, string confirmPassword) 
        {
            if (model.PasswordHash != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match!";
                return View();
            }

            else
            {
                context.Users.Add(model);
                context.SaveChanges();

                TempData["Message"] = "Signup successful! Please login.";
                return RedirectToAction("Login");
            }
        }
        [HttpGet]

        //This method is getting the role as the parameter based on what button the user selected (Manager/Employee)
        public IActionResult Login(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string role)
        {
            //get the object from Users table that matches the password entered
            var user = context.Users
                .FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user != null)
            {    
                // store info in a session instead of a View Bag (the users data is now available in each request)
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetInt32("UserId", user.UserId);

                // redirect based on role
                if (user.Role == "Employee")
                {
                    return RedirectToAction("EmployeeDashboard", "Employee"); // action name/controller
                }
                else if (user.Role == "Manager")
                {
                    return RedirectToAction("ManagerDashboard", "Manager");
                }
            }
            ViewBag.Error = "Invalid login attempt.";
            return View();
        }
    }
}
