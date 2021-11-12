using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project1.Models;
namespace project1.Controllers
{
    public class dept_employeeController : Controller
    {
        // GET: dept_employee
        public ActionResult Index(int dept_id)
        {
            project1Entities db = new project1Entities();
            List<basic> st = db.basics.Where(s => s.deptid == dept_id).ToList();
            return View(st);
        }
        public ActionResult Details(int id)
        {
            project1Entities db = new project1Entities();
            basic st = db.basics.FirstOrDefault(x => x.empid == id);
            return View(st);
        }
    }
}