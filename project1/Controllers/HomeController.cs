using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CaptchaMvc.HtmlHelpers;
using project1.Models;
namespace project1.Controllers
{
    public class HomeController : Controller
    {
        private project1Entities m = new project1Entities();

        //index is the login page
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(login log)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                bool isvaliduser = m.signups.Any(x => x.emailid == log.emailid && x.password == log.password);
                if (isvaliduser)
                {
                    FormsAuthentication.SetAuthCookie(log.emailid, false);
                    m.logins.Add(log);
                    m.SaveChanges();
                    int id = log.id;
                    return RedirectToAction("basiclist", "Register");  //Sasmithaa will put the employee path 


                }
                else
                {
                    ModelState.AddModelError("", "Invalid emailid or Password");
                }
            }
            ViewBag.ErrMessage = "Error: Invalid Captcha";


            return View();



        }

        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signup(signup sign)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                bool isvalid = m.signups.Any(x => x.emailid != sign.emailid);
                if (isvalid)
                {
                    FormsAuthentication.SetAuthCookie(sign.emailid, false);
                    m.signups.Add(sign);
                    m.SaveChanges();
                    int id = sign.id;

                    return RedirectToAction("Index");
                   
                }
                else
                {
                    ModelState.AddModelError("", "Email already Exists");

                }
            }
            ViewBag.ErrMessage = "Error: Invalid Captcha";
            ViewBag.Message = "Thanks for accepting Terms and Conditions";
            return View();
        }

        public ActionResult forgot()
        {

            return View();
        }
        [HttpPost]
        public ActionResult forgot(forgot forgot)
        {
            bool isvalid = m.signups.Any(x => x.emailid == forgot.emailid);
            bool valid = m.signups.Any(x => x.securityquestion == forgot.securityquestion && x.securityanswer.ToLower() == forgot.securityanswer.ToLower());
            if (valid)
            {
                if (isvalid)
                {
                    var pass = m.signups.Where(x => x.emailid.Equals(forgot.emailid));
                    foreach (signup si in pass)
                    {
                        si.password = forgot.newpassword;
                        si.confirmpassword = forgot.confirmpassword;
                    }
                    m.forgots.Add(forgot);
                    m.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Email doesn't exists");

            }
            ModelState.AddModelError("", "Answered Wrong for the Security Question");



            return View();
        }
    }
}