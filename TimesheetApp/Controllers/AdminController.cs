using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimesheetApp.Models;

namespace TimesheetApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        Timesheet_projectEntities context = new Timesheet_projectEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create_Project()
        {
            ViewBag.Managers = context.Users.Where(x => x.Role_Id == 2);
            ViewBag.Technologies = context.Technologies;
            return View();
        }
        [HttpPost]
        public ActionResult Create_Project(CreatProjectViewModel model)
        {
            Project project = new Project();

            project.Project_Name = model.Project_Name;
            project.Project_Manager_Id = model.Project_Manager_Id;
            project.is_Deleted = false;
            project.Updated_Date = DateTime.Now;
            project.Client = model.Client;
            project.Deadline = model.Deadline;
            context.Projects.Add(project);
            context.SaveChanges();

            return Json(new { Message = "Success", JsonRequestBehavior.AllowGet });
        }

        [HttpGet]
        public ActionResult Display_Projects()
        {
            
           
            return View();
        }


       

        [HttpGet]
        public ActionResult GetProjectsByName(int? page,string Project_Name)
        {

            
            if (Project_Name == "" || Project_Name == null)
            {
                IEnumerable<DisplayProjectsViewModel> displayProjects = context.Projects.Join(context.Users, p => p.Project_Manager_Id, u => u.Employee_Id, (p, u) => new DisplayProjectsViewModel { Project_Name = p.Project_Name, Client = p.Client, Deadline = p.Deadline, Manager_Name = u.Employee_Name });
                
                
                
                return PartialView("GetProjectsByName", displayProjects);

            }
            else
            {
                IEnumerable<DisplayProjectsViewModel> displayProjects = context.Projects.Join(context.Users, p => p.Project_Manager_Id, u => u.Employee_Id, (p, u) => new DisplayProjectsViewModel { Project_Name = p.Project_Name, Client = p.Client, Deadline = p.Deadline, Manager_Name = u.Employee_Name }).Where(x => x.Project_Name.ToLower().Contains(Project_Name.ToLower()));
                return PartialView("GetProjectsByName", displayProjects);

            }
                
        }


        [HttpGet]
        public ActionResult Add_Users_To_Project()
        {

            ViewBag.Projects = context.Projects;
            ViewBag.Users = context.Users;

            return View();
        }

        [HttpPost]
        public ActionResult Add_Users_To_Project(string project,string Member)
        {
            Project_Members member = new Project_Members();
            member.Member_Id = Member;
            member.Project_Name = project;
            context.Project_Members.Add(member);
            context.SaveChanges();



            return PartialView("DisplayMembers", GetMembers(project));
        }

        [HttpGet]
        public ActionResult DisplayMembers(string Project_Name)
        {

            return PartialView(GetMembers(Project_Name));
        }

        public IEnumerable<ProjectMembersViewModel>  GetMembers(string Project_Name)
        {
            IEnumerable<ProjectMembersViewModel> members = context.Project_Members.Where(x => x.Project_Name == Project_Name).Join(context.Users, pm => pm.Member_Id, u => u.Employee_Id, (pm, u) => new ProjectMembersViewModel() { Employee_Id = u.Employee_Id, Employee_Name = u.Employee_Name });
            return members;
        }




    }
}