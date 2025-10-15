using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Role_AuthDemo.Models;
using System.Data;

namespace Role_AuthDemo.Controllers
{
    public class EmployeeController : Controller
    {
        //Just say now you have to add function to allow Employee to view their own leaves that they had applied for
        List<LeaveRequest> leave = new List<LeaveRequest>() {
         
        //For ease and demo purpose I haven't added this to the DB you can do this part
        new LeaveRequest {
                Id = Guid.NewGuid(),
                UserId = 2,
                StartDate = new DateTime(2025, 10, 1),
                EndDate = new DateTime(2025, 10, 5),
                Reason = "Medical Leave",
                Status = "Approved"
            },
        new LeaveRequest {
                Id = Guid.NewGuid(),
                UserId = 2,
                StartDate = new DateTime(2025, 10, 1),
                EndDate = new DateTime(2025, 10, 5),
                Reason = "Wedding Leave",
                Status = "Approved"
            },
            new LeaveRequest
            {
                Id = Guid.NewGuid(),
                UserId = 3,
                StartDate = new DateTime(2025, 10, 10),
                EndDate = new DateTime(2025, 10, 15),
                Reason = "Vacation Trip",
                Status = "Pending"
            }
        };
        public IActionResult EmployeeDashboard()
        {
            //Get the current logged in persons role and email (if you want to show the users email as well in the view)
            var role = HttpContext.Session.GetString("UserRole");
            var email = HttpContext.Session.GetString("UserEmail");

            /* The employee dashboard should ONLY be visible if the logged in persons session role is Employee
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

        public IActionResult ViewLeaves()
        { 
            //Get the current logged in sessions userId
            var userId = HttpContext.Session.GetInt32("UserId");


            //Filter the requests to view leaves based on that ID ONLY, ensuring user based info to be shown
            var requests = leave.Where(l => l.UserId == userId).ToList();

            return View(requests);
        }

    }
}
