﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class BentController : Controller
    {
        // GET: Bent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bar()
        {
            return View();
        }
    }
}