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
            string usertype=(string)Session["Typeof"];
            if (usertype.CompareTo("Admin") == 0)
            {
                return View("AdminLoginPage");
            }
            else if (usertype.CompareTo("Applicant") == 0)
            {
                return View("ApplicantLoginPage");
            }
            else if (usertype.CompareTo("Representative") == 0)
            {
                return View("RepresentativeLoginPage");
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
