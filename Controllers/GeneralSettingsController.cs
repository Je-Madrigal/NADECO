using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DataLibrary;
using DataLibrary.Models;
using DataLibrary.ModelView;

namespace NADECO.Controllers
{
    public class GeneralSettingsController : Controller
    {
        // GET: GeneralSettings
        MEMBERSHIPFEE_CON memFee = new MEMBERSHIPFEE_CON();
        public ActionResult Index()
        {

            List<MembershipFee> mLists = memFee.FetchAll();
            List<MembershipFee> mListHeader = memFee.Filter();

            GENERALSETTINGSVIEW genset = new GENERALSETTINGSVIEW
            {
                membershipLists = mLists,
                membership = mListHeader.FirstOrDefault()
            };
            return View();
        }
    }
}