using Login_page.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_page.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string btnSubmit,BaseAccount baseaccount)
        {
            string LoginMsg = "";
            if(btnSubmit== "Login")
            {
                bool verifyStutus=baseaccount.VerifyLogin();
                if(verifyStutus)
                {
                    Session["user"] = baseaccount.UserName;
                    LoginMsg = "Log in Success";
                   // return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    LoginMsg = "Login Failed,Username or Password is not Match";
                }
            }
            else if(btnSubmit == "Forget Password")
            {
                return RedirectToAction("forget", "Account");
            }
            ViewBag.LoginMsg = LoginMsg;
            return View();
        }
        /*public ActionResult ForgetPassword(string btnSubmit)
        {
            if(btnSubmit == "Forget Password")
            {
                return RedirectToAction("forget", "Account");
            }
            return RedirectToAction("Login","Account");
        }*/
        public ActionResult forget() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logout() 
        {
            Session["user"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
} 