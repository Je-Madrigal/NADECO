using DataLibrary.Models;
using DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Antlr.Runtime.Misc;
using System.Data;


namespace NADECO.Controllers
{

    public class HomeController : Controller
    {
        ACCOUNT_CON accon = new ACCOUNT_CON();
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
   
        public ActionResult Login(Account acct)
        {
            string ipAddress = Request.UserHostAddress;
            string IdentityName = Environment.UserName;

            var comName = Environment.MachineName;

            if (ModelState.IsValid)
            {
                Account validUser = accon.ValidateUser(acct.Username, acct.Password, ipAddress, IdentityName, comName);

                if (validUser != null)
                {

                    Session["Pword"] = validUser.Password;
                    Session["UserName"] = validUser.Username;
                    Session["FullName"] = validUser.Name;
                    SetMemberNameInSession(validUser.Name);
                     Session["No_"] = validUser.No_;
                    // Example value

                    return RedirectToAction("Admin_Dashboard", "Home");
                }
                else
                {
                    ViewBag.InvalidLogin = true;
                    return View(acct);
                }
            }

            return View(acct);
        }

        public void SetMemberNameInSession(string name)
        {
            // Assuming name is like "DELACRUZ JUAN TAMAD"
            string fullName = name;

            if (!string.IsNullOrEmpty(fullName))
            {
                string[] words = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Ensure we have at least three words
                string middleName = string.Empty;

                if (words.Length >= 3)
                {
                    // Get the middle name (second word)
                    middleName = words[1]; // JUAN
                }
                else if (words.Length == 2)
                {
                    // If there are only two words, return the second one
                    middleName = words[1]; // JUAN
                }

                // Store the middle name in the session
                Session["FirstName"] = middleName;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(string userNo)
        {
            accon.Logout(userNo);
            // Clear the authentication cookie
            FormsAuthentication.SignOut();

            // Optionally clear session data
            Session.Clear();


            // Redirect to the Login page
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Admin_Dashboard()
        {
            var userName = Session["UserName"] as string;
            var fullName = Session["FullName"] as string;

            ViewBag.ActiveLink = "Home";

            return View();
        }

        public ActionResult Employee_Dashboard()
        {
            return View();
        }
   
    }
}