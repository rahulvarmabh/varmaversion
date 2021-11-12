using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project1.Models;
namespace project1.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        private project1Entities p = new project1Entities();
        // GET: Sample




        //STAFF MANUAL
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\msasmithaa\Desktop\power_bi\power_bi docs\Day 1&2-PowerBI.pdf");
            string filename = "staff manual.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }




        //LEAVE APPLICATION
        public ActionResult leave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult leave(leave obj, string Yes)
        {
            if (Yes == "true")
            {
                obj.ApplyforLeave = "On leave";
            }
            else
            {
                obj.ApplyforLeave = "Not on leave";
            }
            
            if (ModelState.IsValid)
            {
                p.leaves.Add(obj);
                p.SaveChanges();
                return RedirectToAction("leave");
            }
            return View(obj);

        }

        //leave list
        public ActionResult leavelist()
        {
            project1Entities p = new project1Entities();
            List<leave> l = p.leaves.ToList();
            return View(l);
        }



        //RAISE QUERY
        public ActionResult raise()
        {
            return View();
        }
        [HttpPost]
        public ActionResult raise(raise r)
        {
            p.raises.Add(r);
            p.SaveChanges();
            ViewBag.Message = "Data insert successfully";
            return View();

        }



    }
}