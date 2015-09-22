using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeekYourCareer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            string usertype=(string)Session["TypeOfUser"];
            Console.WriteLine(Session["TypeOfUser"]);
            if (Session["TypeOfUser"] == null)
            { 
            
            }
            else if (usertype.CompareTo("Admin") == 0)
            {
                //return View("AdminLoginPage");
                return RedirectToAction("Index","Admin");
            }
            else if (usertype.CompareTo("Applicant") == 0)
            {
                return RedirectToAction("Index", "Applicant");

                //return View("ApplicantLoginPage");
            }
            else if (usertype.CompareTo("Representative") == 0)
            {
                return RedirectToAction("Index", "Representative");
            }
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
