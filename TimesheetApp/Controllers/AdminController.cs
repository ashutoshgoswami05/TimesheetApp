using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimesheetApp.Context;
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

            return View("success");
        }

        [HttpGet]
        public ActionResult Display_Projects()
        {
                       
            return View();
        }

        [HttpGet]
        public ActionResult Edit_Projects()
        {

            return View();
        }

        [HttpGet]
        public ActionResult ProjectDetail(string Project_Name)
        {
            ViewBag.Managers = context.Users.Where(x => x.Role_Id == 2);
            ViewBag.Technologies = context.Technologies;
            Project project = context.Projects.Find(Project_Name);
            CreatProjectViewModel model = new CreatProjectViewModel();
            model.Project_Name = project.Project_Name;
            model.Project_Manager_Id = project.Project_Manager_Id;
            model.Deadline = project.Deadline;
            model.Client = project.Client;
         
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult Edit_Projects(CreatProjectViewModel project)
        {
            Project fromdb = context.Projects.Find(project.Project_Name);
            fromdb.Project_Manager_Id = project.Project_Manager_Id;
            fromdb.Client = project.Client;
            context.SaveChanges();

            return RedirectToAction("GetProjectsByName");
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
            if (context.Project_Members.Where(x => x.Project_Name == project && x.Member_Id == Member).Count() > 0 || project==null || project==string.Empty || Member==null || Member==string.Empty)
            {
                ViewBag.added = true;
                return PartialView("Error");
            }
            else
            {
                Project_Members member = new Project_Members();
                member.Member_Id = Member;
                member.Project_Name = project;
                member.is_Deleted = false;
                member.Updated_Date = DateTime.Now;
                context.Project_Members.Add(member);
                context.SaveChanges();
                return PartialView("DisplayMembers", GetMembers(project));

            }

          
        }

        [HttpGet]
        public ActionResult DisplayMembers(string Project_Name)
        {

            return PartialView(GetMembers(Project_Name));
        }

        [HttpGet]
        public ActionResult UpdateUserDetails()
        {

            return View();
        }

        [HttpGet]
        public ActionResult EmployeeDetail(string EmployeeId)
        {

            ViewBag.Roles = context.Roles;
            ViewBag.Technologies = context.Technologies;
            User user = context.Users.Find(EmployeeId);
            if (user == null)
            {
                return PartialView("Error");
            }
            else
            {

                return PartialView(user);
            }
        }

        [HttpPost]
        public ActionResult EmployeeDetail(RegisterViewModel model)
        {
            User user = context.Users.Find(model.Employee_Id);

            if (user != null)
            {
                user.Employee_Name = model.Employee_Name;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.Role_Id = model.Role_Id;
                user.Status = model.Status;
                user.Updated_Date = DateTime.Now;
                context.SaveChanges();
                return View("success");
            }

            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteEmployee(string EmployeeId)
        {
            User user = context.Users.Find(EmployeeId);

            if (user != null)
            {
                user.is_Deleted = true;
                context.SaveChanges();
                return PartialView("success");
            }

            else
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult Create_Subtask()
        {
            ViewBag.Projects = context.Projects;
           
            return View();
        }

        [HttpPost]
        public ActionResult Create_Subtask(string Subtask,string Project)
        {
            Subtasks subtask = new Subtasks();
            subtask.Project_Name = Project;
            subtask.Subtask = Subtask;
            context.Subtasks.Add(subtask);
            context.SaveChanges();


            return PartialView("Display_Subtask", GetSutask(Project));
        }

        [HttpGet]
        public ActionResult DisplaySubtask(string project)
        {
                      
            return PartialView("Display_Subtask",GetSutask(project));
        }

       

        [HttpPost]
        public ActionResult RemoveMember(string Project_Name,string EmployeeId)
        {
            Project_Members p1 = context.Project_Members.Where(x => x.Project_Name == Project_Name && x.Member_Id == EmployeeId).FirstOrDefault();
            p1.is_Deleted = true;
            p1.Updated_Date = DateTime.Now;
            context.SaveChanges();


            return PartialView("DisplayMembers",GetMembers(Project_Name));



        }



        public IEnumerable<Subtasks> GetSutask(string Project_Name)
        {
            IEnumerable<Subtasks> subtasks = context.Subtasks.Where(x => x.Project_Name == Project_Name).ToList();
            return subtasks;
        }


        public IEnumerable<ProjectMembersViewModel>  GetMembers(string Project_Name)
        {
            IEnumerable<ProjectMembersViewModel> members = context.Project_Members.Where(x => x.Project_Name == Project_Name).Join(context.Users, pm => pm.Member_Id, u => u.Employee_Id, (pm, u) => new ProjectMembersViewModel() { Employee_Id = u.Employee_Id, Employee_Name = u.Employee_Name ,Project_Name=pm.Project_Name,IsDeleted=pm.is_Deleted});
            return members;
        }




    }
}