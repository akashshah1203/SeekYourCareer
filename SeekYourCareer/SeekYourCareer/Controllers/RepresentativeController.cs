using SeekYourCareer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Controllers
{
    public class RepresentativeController : Controller
    {
        //
        // GET: /Representative/

        public ActionResult Index()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Representative") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            int repid=(int)Session["UserID"];
            int job = new DataAccess.DataObj().representativeTotalJobApplication(repid);
            int training = new DataAccess.DataObj().representativeTotalTrainingApplication(repid);
            int workshop = new DataAccess.DataObj().representativeTotalWorkshopApplication(repid);
            ViewBag.jobs = job;
            ViewBag.train = training;
            ViewBag.work = workshop;

            return View();

        }

        //
        // GET: /Representative/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Representative/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Representative/Create

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
        // GET: /Representative/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Representative/Edit/5

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
        // GET: /Representative/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Representative/Delete/5

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


        //  /Representative/addJobsTODb
        public ActionResult addJobsTODb()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Representative") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            RepresentativeAddJobOffer obj = new RepresentativeAddJobOffer();

            List<string> jobtypes = new List<string>();
            jobtypes.Add("Full-Time");
            jobtypes.Add("Part-Time");
            ViewBag.jobtype = jobtypes;
            ViewBag.companyname = (string)Session["UserName"];

            List<string> stream = new List<string>();
            stream.Add("Engineering");
            stream.Add("Finance");
            ViewBag.streams = stream;
            List<string> location = new List<string>();
            location.Add("Bangalore");
            location.Add("Delhi");
            location.Add("Pune");
            location.Add("Kolkata");
            location.Add("Jaipur");
            ViewBag.locations = location;


            //obj.Items

            return View();
        }

        [HttpPost]
        public ActionResult addJobsTODb(RepresentativeAddJobOffer obj)
        {
            int id=(int)Session["UserID"];
            int val=new DataAccess.RepresentativeDAL().addJobOffer(obj,id);
            return View("Index");
        }

        public ActionResult addWorkshopTODb()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Representative") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
           
            List<string> location = new List<string>();
            location.Add("Bangalore");
            location.Add("Delhi");
            location.Add("Pune");
            location.Add("Kolkata");
            location.Add("Jaipur"); 
            ViewBag.locations = location;

            ViewBag.companyname=(string)Session["UserName"];

            List<string> stream = new List<string>();
            stream.Add("Engineering");
            stream.Add("Finance");
            ViewBag.streams = stream;
            return View();
        }

        [HttpPost]
        public ActionResult addWorkshopTODb(RepresentativeAddWorkshopOffer obj)
        {
            int id = (int)Session["UserID"];
            int val = new DataAccess.RepresentativeDAL().addWorkshopOffer(obj, id);
            return View("Index");
        }

        public ActionResult addTrainingTODb()
        {
            string type = (string)Session["TypeOfUser"];
            if (type == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (type.CompareTo("Representative") != 0)
            {
                return RedirectToAction("Index", "Home");
            }
           

            List<string> location = new List<string>();
            location.Add("Bangalore");
            location.Add("Delhi");
            location.Add("Pune");
            location.Add("Kolkata");
            location.Add("Jaipur");
            ViewBag.locations = location;
            ViewBag.companyname = (string)Session["UserName"];

            List<string> stream = new List<string>();
            stream.Add("Engineering");
            stream.Add("Finance");
            ViewBag.streams = stream;
            return View();
        }

        [HttpPost]
        public ActionResult addTrainingTODb(RepresentativeAddTrainingOffer obj)
        {
            int id = (int)Session["UserID"];
            string comp = (string)Session["UserName"];
            obj.Company = comp;
            int val = new DataAccess.RepresentativeDAL().addTrainingOffer(obj, id);

            return View("Index");
        }
    
    }
}
