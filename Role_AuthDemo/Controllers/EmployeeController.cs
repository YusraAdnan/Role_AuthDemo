using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Role_AuthDemo.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult EmployeeDashboard()
        {
            //Get the current logged in persons role and email (if you want to show the users email as well in the view)
            var role = HttpContext.Session.GetString("UserRole");
            var email = HttpContext.Session.GetString("UserEmail");

            /*The employee dashboard should ONLY be visible if the logged in persons session role is Employee
             If it is not then redirect them to the login page */
            if (role != "Employee" || role == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.EmployeeEmail = email;
            }
            ViewBag.Email = email;
            return View();
        }

    }
}
