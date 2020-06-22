using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimesheetApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        Timesheet_projectEntities context = new Timesheet_projectEntities();
        // GET: User
       
       [HttpGet]
        public ActionResult Index(string Employee_Id)
        {
            User user=context.Users.Find(Employee_Id);
            return View(user);
        }

        [HttpGet]
        public ActionResult New_Timesheet()
        {
            ViewBag.Projects = context.Projects.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult New_Timesheet(Timesheets timesheet)
        {
            context.Timesheets.Add(timesheet);
            context.SaveChanges();
            return Json(new { Message = "Success", JsonRequestBehavior.AllowGet });
        }

        [HttpGet]
        public ActionResult View_Timesheet()
        {
            List<Timesheets> timesheets= context.Timesheets.Where(x => x.User == HttpContext.User.Identity.Name).ToList();
            return View(timesheets);
        }
    }
}