using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EkzoPlugin.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home page";
            return View();
        }

        public ActionResult Changelog()
        {
            ViewBag.Title = "Change log";
            return View();
        }

        public ActionResult RoadMap()
        {
            ViewBag.Title = "Roadmap";
            return View();
        }
    }
}
