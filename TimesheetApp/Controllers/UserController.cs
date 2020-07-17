using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimesheetApp.Context;
using TimesheetApp.Models;
using TimesheetApp.Controllers;

namespace TimesheetApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        Timesheet_projectEntities context = new Timesheet_projectEntities();
        // GET: User
       
       [HttpGet]
        public ActionResult Index()
        {
            
         
         
            return View();
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
            if (ModelState.IsValid)
            {
                timesheet.Updated_Date = DateTime.Now;
                timesheet.is_Deleted = false;
                timesheet.Status_Id = 3;
                context.Timesheets1.Add(timesheet);
                context.SaveChanges();
                ViewBag.Projects = context.Projects.ToList();
                return PartialView("_NewTimeSheet");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult View_Timesheet()
        {
            List<Timesheets> timesheets= context.Timesheets1.Where(x => x.User == HttpContext.User.Identity.Name && x.is_Deleted != true ).ToList();
            return View(timesheets);
        }

        [HttpGet]
        public ActionResult Edit_Timesheet(int Id)
        {
            Timesheets timesheet = context.Timesheets1.Find(Id);
            ViewBag.Projects = context.Projects.ToList();
            return PartialView(timesheet);
        }

        [HttpPost]
        public ActionResult Edit_Timesheet(Timesheets Updatedtimesheet)
        {
            Timesheets Savedtimesheet = context.Timesheets1.Find(Updatedtimesheet.Id);
            Savedtimesheet.Project_Name = Updatedtimesheet.Project_Name;
            Savedtimesheet.date = Updatedtimesheet.date;
            Savedtimesheet.TimeSpent = Updatedtimesheet.TimeSpent;
            Savedtimesheet.Workdone= Updatedtimesheet.Workdone;
            Savedtimesheet.Mode = Updatedtimesheet.Mode;
            Savedtimesheet.Updated_Date = DateTime.Now;

            context.SaveChanges();

            List<Timesheets> timesheets = context.Timesheets1.Where(x => x.User == HttpContext.User.Identity.Name && x.is_Deleted != true).ToList();
            return PartialView("_ViewTimesheet", timesheets);
                
         }

        [HttpPost]
        public ActionResult Delete_Timesheet(int Id)
        {
            Timesheets timesheet = context.Timesheets1.Find(Id);
            timesheet.Updated_Date = DateTime.Now;
            timesheet.is_Deleted = true;
            context.SaveChanges();
            List<Timesheets> timesheets = context.Timesheets1.Where(x => x.User == HttpContext.User.Identity.Name && x.is_Deleted != true).ToList();
            return PartialView("_ViewTimesheet", timesheets);
        }

        [HttpGet]
        public ActionResult Manage_Timesheet()
        {
                       
            return View(GetTimehsheets());
        }

        [HttpPost]
        public ActionResult Manage_Timesheet(int Id, int Status)
        {
            ChangeTimeSheetStatus(Id, Status);
            return PartialView("_MemberTimesheets", GetTimehsheets());
        }

       



        private IEnumerable<ManageViewModel> GetTimehsheets()
        {
            IEnumerable<Project> projects = context.Projects.Where(x => x.Project_Manager_Id == HttpContext.User.Identity.Name);

            IEnumerable<Timesheets> timesheets = context.Timesheets1;


            IEnumerable<TimesheetApp.Context.User> users = context.Users;

            IEnumerable<ManageViewModel> timesheetdetails = projects.Join(timesheets, p => p.Project_Name, t => t.Project_Name, (p, t) => new { p, t }).Join(users, ppc => ppc.t.User, c => c.Employee_Id, (ppc, c) => new { ppc, c }).Select(m => new ManageViewModel
            {
                Employee_Name = m.c.Employee_Name,
                Project_Name = m.ppc.t.Project_Name,
                date = m.ppc.t.date,
                Mode = m.ppc.t.Mode,
                TimeSpent = m.ppc.t.TimeSpent,
                Workdone = m.ppc.t.Workdone,
                Status_Id = m.ppc.t.Status_Id,
                Employee_Id = m.ppc.t.User,
                Id = m.ppc.t.Id,
                is_Deleted = m.ppc.t.is_Deleted

            }) ;

            return timesheetdetails;
        }

        private void ChangeTimeSheetStatus(int Id, int Status)
        {
            Timesheets timesheet = context.Timesheets1.Where(x => x.Id == Id).FirstOrDefault();
            timesheet.Status_Id = Status;
            context.SaveChanges();
        }

    }
}