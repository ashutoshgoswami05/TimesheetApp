using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimesheetApp.Controllers
{
    public class UserController : Controller
    {
        Timesheet_projectEntities context = new Timesheet_projectEntities();
        // GET: User
       
        [Authorize]
        public ActionResult Index(string Employee_Id)
        {
            Users_table user=context.Users_table.Find(Employee_Id);
            return View(user);
        }
    }
}