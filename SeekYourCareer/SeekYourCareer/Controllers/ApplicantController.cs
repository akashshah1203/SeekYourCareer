using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeekYourCareer.Models;

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
        public ActionResult ApplyForJob()
        {
            List<string> CompanyNames=new List<string>();
            CompanyNames = new DataAccess.ApplicantDAL().GetCompanyNames();
            ViewBag.list = CompanyNames;
            return View();
        }

        [HttpPost]
        public ActionResult ApplyForJob(string CompanyName)
        {
            List<string> streams = new List<string>();
            streams = new DataAccess.ApplicantDAL().Stream(CompanyName);

            return Json(streams);
        }
        [HttpPost]
        public ActionResult ApplyForJob1 (string stream,string company)
        {
            string username=(string)Session["Username"];
            List<Job> jobdetails = new List<Job>();
            
            jobdetails = new DataAccess.ApplicantDAL().JobDescription(stream,company,username);
            return Json(jobdetails);
        }


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
