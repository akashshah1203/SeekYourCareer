﻿using System;
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
            if (Session["TypeOfUser"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
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
    }
}
