using Microsoft.AspNetCore.Mvc;

namespace Role_AuthDemo.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult ManagerDashboard()
        {
            //Get the current logged in persons role and email (if you want to show the users email as well in the view)
            var role = HttpContext.Session.GetString("UserRole");
            var email = HttpContext.Session.GetString("UserEmail");

            /*The Manager dashboard should ONLY be visible if the logged in persons session role is Manager
               If it is not then redirect them to the login page */
            if (role != "Manager" || role == null)
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
