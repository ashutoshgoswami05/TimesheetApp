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


            ViewBag.Roles = context.Roles_table;
            ViewBag.Technologies = context.Technologies_table;
            return View();
        }


        [HttpPost]
        public ActionResult Register(Users_table user)
        {
            Random r = new Random();
            string password = BitConverter.ToString(passwordhash(r.Next().ToString()));
            int length = password.Length;
            user.Password = password;

            context.Users_table.Add(user);
            context.SaveChanges();
            ViewBag.Message = "Registraion Successful";

            return RedirectToAction("Login");
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


            string passfromdb = context.Users_table.Find(model.Employee_Id).Password;
            string pass = BitConverter.ToString(passwordhash(model.Password));
            int valid2 = pass == passfromdb ? 1 : 0;





            if(valid == 1)
            {
                FormsAuthentication.SetAuthCookie(model.Employee_Id, true);
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