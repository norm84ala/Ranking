using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ranking.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WebsiteRanking()
        {
            return View();
        }

        public ActionResult WebsiteVisitCount()
        {
            return View();
        }
    }
}