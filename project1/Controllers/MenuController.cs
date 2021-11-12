using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project1.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Insights()
        {
            return View();
        }
        public ActionResult Industries()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Careers()
        {
            return View();
        }
        public ActionResult Aboutus()
        {
            return View();
        }
    }
}