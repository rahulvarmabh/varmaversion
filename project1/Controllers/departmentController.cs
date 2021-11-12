using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project1.Models;
namespace project1.Controllers
{
    public class departmentController : Controller
    {
        // GET: department
        public ActionResult Index()
        {
            project1Entities db = new project1Entities();
            List<department> ld = db.departments.ToList();

            return View(ld);
        }
    }
}