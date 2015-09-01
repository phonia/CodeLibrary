using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private DAL.SchoolContext db = new DAL.SchoolContext();
        //
        // GET: /Student/

        //public ActionResult Index()
        //{
        //    return View(db.Students.ToList());
        //}

        //public ViewResult Index(string sortOrder)
        //{
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
        //    var students = from s in db.Students
        //                   select s;
        //    switch (sortOrder)
        //    {
        //        case "Name desc":
        //            students = students.OrderByDescending(s => s.LastName);
        //            break;
        //        case "Date":
        //            students = students.OrderBy(s => s.EnrollmentDate);
        //            break;
        //        case "Date desc":
        //            students = students.OrderByDescending(s => s.EnrollmentDate);
        //            break;
        //        default:
        //            students = students.OrderBy(s => s.EnrollmentDate);
        //            break;
        //    }
        //    return View(students.ToList());
        //}



        //
        //GET:


        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstMidName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "Date desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            return View(students.ToList());
        }

        public ActionResult Details(int id)
        {

            Student student = db.Students.Find(id);
            EntityState statebefore = db.Entry(student).State; //通过find取出来得到的状态是  Unchanged
            return View(student);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    EntityState statebefore = db.Entry(student).State;  //Detached 
                    db.Students.Add(student);
                    EntityState stateAdd = db.Entry(student).State; //Added
                    db.SaveChanges();
                    EntityState stateafter = db.Entry(student).State;//Unchanged
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }

        //public ActionResult Delete(int id, bool? saveChangesError)
        //{
        //    if (saveChangesError.GetValueOrDefault())
        //    {
        //        ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
        //    }
        //    return View(db.Students.Find(id));
        //}

        //
        // POST: /Student/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here
        //        Student student = db.Students.Find(id);
        //        EntityState statebefore = db.Entry(student).State; //UnChange状态
        //        db.Students.Remove(student);
        //        EntityState stateafter = db.Entry(student).State;//Deleted状态
        //        db.SaveChanges();
        //        EntityState stateaOk = db.Entry(student).State;//Detached状态
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here

                if (Request.IsAjaxRequest())
                {
                    Student student = db.Students.Find(id);
                    db.Students.Remove(student);
                    EntityState stateafter = db.Entry(student).State;//Deleted状态
                    int result = db.SaveChanges();
                    return Content(result.ToString());
                }
                else
                {
                    return Content("-1");
                }
            }
            catch
            {
                return Content("-1");
            }
        }

        public ActionResult Edit(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                EntityState statebefore = db.Entry(student).State;
                db.Entry(student).State = EntityState.Modified;
                int i = db.SaveChanges();
                EntityState stateafter = db.Entry(student).State;
                return RedirectToAction("Index");
            }
            return View(student);
        }

    }
}
