﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingSite.Controllers
{
    public class ChiefController : Controller
    {
        // GET: Chief
        public ActionResult OrderStatus()
        {
            return View();
        }

        public ActionResult StoreRequest()
        {
            return View();
        }

        public ActionResult ItemExtradition()
        {
            return View();
        }
    }
}