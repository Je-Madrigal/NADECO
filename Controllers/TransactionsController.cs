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
    public class TransactionsController : Controller
    {
        TRANSACTIONS_CON Transact = new TRANSACTIONS_CON();
        // GET: Transactions
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostTransaction(Transactions trcss)
        {

            Transact.InsertRegistration(trcss);
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Members") });

            //return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }
    }
}