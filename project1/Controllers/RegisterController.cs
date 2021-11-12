using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using project1.Models;
namespace project1.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        private project1Entities p = new project1Entities();


        //BASIC DETAILS TABLE
        //Basic details create template
        public ActionResult basic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult basic(basic obj)
        {
            string filename = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
            string extension = Path.GetExtension(obj.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            obj.emp_photo_path = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            obj.ImageFile.SaveAs(filename);


            using (project1Entities p = new project1Entities())
            {
                if (ModelState.IsValid)
                {
                    p.basics.Add(obj);
                    p.SaveChanges();
                    return RedirectToAction("education","Register");
                }
            }
            ModelState.Clear();

            return View(obj);
        }


        //Basic details in list template
        public ActionResult basiclist()
        {
            project1Entities p = new project1Entities();
            List<basic> b = p.basics.ToList();
            return View(b);
        }


        //PAGE FILTERING ,SORTING,SEARCHING using list template
        public ViewResult Pagefilter(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //branch1 is branch1 model ....Since emplist1 and branch1 tables are linked
            var ModelList = p.basics.Include(d => d.department);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var model = from s in p.basics
                        select s;
            //Search and match data, if search string is not null or empty
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.emptype.Contains(searchString)
                                       || s.empname.Contains(searchString)||s.gender.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    ModelList = model.OrderByDescending(s => s.empid);
                    break;

                default:
                    ModelList = model.OrderBy(s => s.empid);
                    break;
            }
            //indicates the size of list
            int pageSize = 3;
            //set page to one is there is no value, ??  is called the null-coalescing operator.
            int pageNumber = (page ?? 1);
            //return the Model data with paged
            return View(ModelList.ToPagedList(pageNumber, pageSize));

        }





        //basic details in details template
        public ActionResult Details(int id)
        {
            project1Entities p = new project1Entities();
            basic b = p.basics.FirstOrDefault(a => a.empid == id);
            return View(b);
        }



        //Basic details in edit template
        public ActionResult Edit(int? id)
        {
            project1Entities p = new project1Entities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            basic basic = p.basics.Find(id);
            if (basic == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptid = new SelectList(p.departments, "dept_id", "dept_name", basic.deptid);
            return View(basic);
        }

        // POST: basics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empid,empname,emptype,deptid,dob,email,mobile,address,dateofjoin,gender,emp_photo_path")] basic basic)
        {
            project1Entities p = new project1Entities();
            if (ModelState.IsValid)
            {
                p.Entry(basic).State = EntityState.Modified;
                p.SaveChanges();
                return RedirectToAction("basiclist", "Register");
            }
            ViewBag.deptid = new SelectList(p.departments, "dept_id", "dept_name", basic.deptid);
            return View(basic);
        }



        //Basic details in delete template
        public ActionResult Delete(int? id)
        {
            project1Entities p = new project1Entities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            basic basic = p.basics.Find(id);
            if (basic == null)
            {
                return HttpNotFound();
            }
            return View(basic);
        }

        // POST: basics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            project1Entities p = new project1Entities();
            basic basic = p.basics.Find(id);
            p.basics.Remove(basic);
            p.SaveChanges();
            return RedirectToAction("basiclist", "Register");
        }




                
                                    //EDUCATION DETAILS TABLE
        //education details in create template
        public ActionResult education()
        {
            return View();
        }
        [HttpPost]
        public ActionResult education(education obj)
        {
            project1Entities p = new project1Entities();
            if (ModelState.IsValid)
            {
                p.educations.Add(obj);
                p.SaveChanges();
                return RedirectToAction("work", "Register");
            }
            return View(obj);
        }




        //education details in details template
        public ActionResult edudetails(int id)
        {
            project1Entities p = new project1Entities();
            education e = p.educations.FirstOrDefault(a => a.empid == id);
            return View(e);
        }




        //education details in edit template
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            education education = p.educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            ViewBag.empid = new SelectList(p.basics, "empid", "empname", education.empid);
            return View(education);
        }

        // POST: educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "id,empid,Tenth_percentage,Tenth_yop,Twelfth_percentage,Twelfth_yop,UG_percentage,UG_yop,PG_percentage,PG_yop")] education education)
        {
            if (ModelState.IsValid)
            {
                p.Entry(education).State = EntityState.Modified;
                p.SaveChanges();
                return RedirectToAction("basiclist", "Register");
            }
            ViewBag.empid = new SelectList(p.basics, "empid", "empname", education.empid);
            return View(education);
        }



        //education details in delete template

        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            education education = p.educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: educations/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id)
        {
            education education = p.educations.Find(id);
            p.educations.Remove(education);
            p.SaveChanges();
            return RedirectToAction("basiclist", "Register");
        }





        //WORK DETAILS TABLE


        //Work details in create template
        public ActionResult work()
        {
            return View();
        }
        [HttpPost]
        public ActionResult work(work obj)
        {
            project1Entities p = new project1Entities();
            if (ModelState.IsValid)
            {
                p.works.Add(obj);
                p.SaveChanges();
                return RedirectToAction("skill", "Register");
            }
            return View(obj);
        }



        //workdetails in details template
        public ActionResult workdetails(int id)
        {
            project1Entities p = new project1Entities();
            work w = p.works.FirstOrDefault(a => a.empid == id);
            return View(w);
        }



        //Work details in edit template
        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            work work = p.works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            ViewBag.empid = new SelectList(p.basics, "empid", "empname", work.empid);
            return View(work);
        }

        // POST: works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "id,empid,previous_emp_comp_name,year_exp,work_domain")] work work)
        {
            if (ModelState.IsValid)
            {
                p.Entry(work).State = EntityState.Modified;
                p.SaveChanges();
                return RedirectToAction("basiclist", "Register");
            }
            ViewBag.empid = new SelectList(p.basics, "empid", "empname", work.empid);
            return View(work);
        }



        //work details in delete template
        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            work work = p.works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        // POST: works/Delete/5
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            work work = p.works.Find(id);
            p.works.Remove(work);
            p.SaveChanges();
            return RedirectToAction("basiclist", "Register");
        }






        //SKILL DETAILS TABLE

        //skill details in create template
        public ActionResult skill()
        {
            return View();
        }
        [HttpPost]
        public ActionResult skill(skill obj)
        {
            project1Entities p = new project1Entities();
            if (ModelState.IsValid)
            {
                p.skills.Add(obj);
                p.SaveChanges();
                return RedirectToAction("basic","Register");
            }
            return View(obj);
        }




        //skill details in details template
        public ActionResult skilldetails(int id)
        {
            project1Entities p = new project1Entities();
            skill s = p.skills.FirstOrDefault(a => a.empid == id);
            return View(s);
        }



        //skill details in edit template
        public ActionResult Edit3(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skill skill = p.skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            ViewBag.empid = new SelectList(p.basics, "empid", "empname", skill.empid);
            return View(skill);
        }

        // POST: skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit3([Bind(Include = "id,empid,primary_skills,other_skills")] skill skill)
        {
            if (ModelState.IsValid)
            {
                p.Entry(skill).State = EntityState.Modified;
                p.SaveChanges();
                return RedirectToAction("basiclist","Register");
            }
            ViewBag.empid = new SelectList(p.basics, "empid", "empname", skill.empid);
            return View(skill);
        }




        //skill details in delete template
        public ActionResult Delete3(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skill skill = p.skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: skills/Delete/5
        [HttpPost, ActionName("Delete3")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed3(int id)
        {
            skill skill = p.skills.Find(id);
            p.skills.Remove(skill);
            p.SaveChanges();
            return RedirectToAction("basiclist","Register");
        }


    }
}