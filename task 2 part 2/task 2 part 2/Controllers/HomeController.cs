using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_2_part_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Display()
        {
            return View();
        }

        public ActionResult GetFunctions()
        {
            return View();
        }
    }
}
