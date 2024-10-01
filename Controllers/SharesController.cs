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
    public class SharesController : Controller
    {
 
        SHAREHEADER_CON SCon = new SHAREHEADER_CON();
        SHARELINE_CON SConLine = new SHARELINE_CON();

        // GET: Shares
        public ActionResult Index()
        {
            List<Shares> shares_ = SCon.FetchAll();

            return View(shares_);
        }


        public ActionResult Details(string No_)
        {
            SHARESVIEW shrs_ = new SHARESVIEW();

            if (string.IsNullOrWhiteSpace(No_))
            {
                shrs_.shareHeader = new Shares();
                shrs_.shareLine = new List<Shares>();
                //if(memAcct) it suppose to be okay if null
                return View(shrs_);
            }

            Shares shrHEADER = SCon.Filter(No_);
            List<Shares> shrLINE = SConLine.Filter(No_);


            shrs_.shareHeader = shrHEADER;
            shrs_.shareLine = shrLINE;


            return View(shrs_);
        }
    }
}