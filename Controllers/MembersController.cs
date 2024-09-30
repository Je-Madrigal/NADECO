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
    public class MembersController : Controller
    {
        
        MEMBER_CON memberCon = new MEMBER_CON(); ACCOUNT_CON accountCon = new ACCOUNT_CON();

        // GET: Members
        public ActionResult Index()
        {
            List<Members> members_ = memberCon.FetchAll();

            return View(members_);
        }

        public ActionResult Profile_(string No_)
        {

            MEMBERS_ACCOUNT_VIEW memAcct = new MEMBERS_ACCOUNT_VIEW();
 
            if (string.IsNullOrWhiteSpace(No_))
            {
                memAcct.members_ = new Members();
                memAcct.acct = new Account();
                //if(memAcct) it suppose to be okay if null
                return View(memAcct);
            }


            List<Members> members = memberCon.Filter(No_);
            List<Account> accts = accountCon.Filter(No_);


            memAcct.members_ = members.FirstOrDefault();
            memAcct.acct = accts.FirstOrDefault();

            return View(memAcct);
        }


        [HttpPost]
        public ActionResult Create(Members member)
        {
            if (ModelState.IsValid)
            {
                memberCon.InsertMember(member);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Members") });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }


        [HttpPost]
        public ActionResult Update(Members member)
        {
            if (ModelState.IsValid)
            {
                memberCon.UpdateMember(member);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Members") });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }


    }
}