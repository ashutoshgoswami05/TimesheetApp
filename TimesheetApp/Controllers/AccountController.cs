using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using TimesheetApp.Models;
using System.Web.Security;
using TimesheetApp.Context;
using System.Text;

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
            //string LastUserId = context.Users.OrderByDescending(x => x.Employee_Id).Select(x => x.Employee_Id).FirstOrDefault();
            //ViewBag.NewUserId =string.IsNullOrEmpty(LastUserId) ? "R01" : "R"+(Convert.ToInt32(LastUserId.Split('R')[1])+1);

           
            return View();
            
        }


        [HttpPost]
        public ActionResult Register(User user, List<int> TechnologyIds)
        {
         
            ViewBag.Roles = context.Roles;
            ViewBag.Technologies = context.Technologies;

            if(context.Users.Where(x => x.Employee_Id == user.Employee_Id).Count() > 0)
            {
                ModelState.AddModelError("Employee_Id", "User with this Id already Exists");
                return View();

            }
            else if (context.Users.Where(x => x.Email == user.Email).Count() > 0  )
            {
                
                ModelState.AddModelError("Email", "User with this email already Exists");
                return View();

            }           

            else if (ModelState.IsValid)
            {

                user.Updated_Date = DateTime.Now;
                user.is_Deleted = false;
                byte[] salt = GetSalt(user.Password);
                user.PasswordSalt=Convert.ToBase64String(salt);
                user.Password = Convert.ToBase64String(passwordhash(user.Password, salt));

                foreach (var id in TechnologyIds)
                {
                    User_Technologies t1 = new User_Technologies() { EmployeeId = user.Employee_Id, TechnologyId = id };
                    context.User_Technologies.Add(t1);
                }

                context.Users.Add(user);
                context.SaveChanges();
                return View("success");
                
            }

            else
            {
                return View();
            }

            
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                LoginViewModel model = new LoginViewModel();


                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            
            User Loggedin = context.Users.Find(model.Employee_Id);
            if (Loggedin == null)
            {
                ModelState.AddModelError("Wrong Password", "Invalid EmployeeId or Password");
                return View();
            }
            else
            {
                string passfromdb = Loggedin.Password;
                byte[] salt = Convert.FromBase64String(Loggedin.PasswordSalt);
                string pass = Convert.ToBase64String(passwordhash(model.Password, salt));
                int valid = pass == passfromdb ? 1 : 0;

                if (valid == 1)
                {
                    if (Loggedin.Status == true)
                    {
                        FormsAuthentication.SetAuthCookie(model.Employee_Id, false);

                        HttpCookie cookie = new HttpCookie("Validation");
                        cookie["Name"] = Loggedin.Employee_Name;
                        cookie["Role"] = Loggedin.Role_Id.ToString();
                        Response.Cookies.Add(cookie);



                        return RedirectToAction("Index", "User");
                    }

                    else
                    {
                        ModelState.AddModelError("Status", "Account Not Activated");
                        return View();
                    }


                }
                else
                {
                    ModelState.AddModelError("Wrong Password", "Invalid EmployeeId or Password");
                    return View();


                }

            }
           
        }

        //[HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();


            string[] mycookies = Request.Cookies.AllKeys;
            foreach (var item in mycookies)
            {
                Response.Cookies[item].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Login");
        }

        private byte[] GetSalt(string password)
        {
            const int salt_size = 24;
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[salt_size];
            provider.GetBytes(salt);

            return salt;

        }

        private byte[] passwordhash(string password,byte[] salt)
        {
            
            const int hash_size = 24;
            const int iterations = 10000;

           

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            return pbkdf2.GetBytes(hash_size);

        }

    }
}