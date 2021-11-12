using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project1.Models;
namespace project1.Controllers
{
    public class MyprofileController : Controller
    {
        // GET: Myprofile
        //public ActionResult MyProfile(int id)
        //{
        //    project1Entities p = new project1Entities();
        //    basic b = p.basics.FirstOrDefault(a => a.empid == id);
        //    return View(b);
            
        //}
        public ActionResult MyProfile(login model)
        {
            project1Entities p = new project1Entities();
            basic b = (from cp in p.basics

                      where cp.email == model.emailid
                      select cp).FirstOrDefault();

            return View(b);
        }
    }
}