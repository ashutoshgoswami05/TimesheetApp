using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using TimesheetApp.Models;
using System.Web.Security;

namespace TimesheetApp.Controllers
{
    public class AccountController : Controller
    {
        Timesheet_projectEntities context = new Timesheet_projectEntities();
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {


            ViewBag.Roles = context.Roles;
            ViewBag.Technologies = context.Technologies;
            return View();
        }


        [HttpPost]
        public ActionResult Register(User user,List<int> TechnologyIds)
        {
            Random r = new Random();
            string password = BitConverter.ToString(passwordhash(r.Next().ToString()));
            int length = password.Length;
            user.Password = password;

            foreach (var id in TechnologyIds) 
            {
                User_Technologies t1 = new User_Technologies() { EmployeeId = user.Employee_Id, TechnologyId = id };
                context.User_Technologies.Add(t1);
            }

            context.Users.Add(user);
            context.SaveChanges();
            

            return Json(new {Message="Success",JsonRequestBehavior.AllowGet });
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();

          
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            int? valid = context.sp_ValidateUser(model.Employee_Id, model.Password).FirstOrDefault();
            string message = string.Empty;


            //string passfromdb = context.Users_table.Find(model.Employee_Id).Password;
            //string pass = BitConverter.ToString(passwordhash(model.Password));
            //int valid2 = pass == passfromdb ? 1 : 0;





            if(valid == 1)
            {
                FormsAuthentication.SetAuthCookie(model.Employee_Id, false);
                return RedirectToAction("Index","User",new {Employee_Id=model.Employee_Id });
            }

            else
            {
                message = "Account not activated";

                ViewBag.Message = message;


                return View(User);
            }


        }

        //[HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }


        private byte[] passwordhash(string password)
        {
            const int salt_size = 24;
            const int hash_size = 24;
            const int iterations = 10000;

            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[salt_size];
            provider.GetBytes(salt);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            return pbkdf2.GetBytes(hash_size);

        }

    }
}