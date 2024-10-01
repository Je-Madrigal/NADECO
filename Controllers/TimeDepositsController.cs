using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Models;
using DataLibrary;
using Antlr.Runtime.Misc;
using System.Security.Principal;
using DataLibrary.ModelView;
using Microsoft.Ajax.Utilities;

namespace NADECO.Controllers
{


    public class TimeDepositsController : Controller
    {

        TIMEDEPOSIT_CON TDCon = new TIMEDEPOSIT_CON();
        // GET: TimeDeposits
        public ActionResult Index()
        {
            List<TimeDeposits> tdeposits = TDCon.FetchAll();

            return View(tdeposits);
        }
    }
}