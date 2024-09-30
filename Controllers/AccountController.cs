using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.Models;
using DataLibrary.ModelView;
using Microsoft.Ajax.Utilities;

namespace NADECO.Controllers
{
    public class AccountController : Controller
    {
        ACCOUNT_CON acctcon = new ACCOUNT_CON();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateAccountStatus(Account acc)
         {
            if (ModelState.IsValid)
            {
                acctcon.UpdateAccountStatus(acc);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Members") });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }
    }
}