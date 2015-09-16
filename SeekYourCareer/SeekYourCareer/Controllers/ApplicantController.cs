using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Controllers
{
    public class ApplicantController : Controller
    {
        //
        // GET: /Applicant/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Applicant/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Applicant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Applicant/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Applicant/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Applicant/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Applicant/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Applicant/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
