using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DpimProject.Controllers
{
    public class CourseManageController : Controller
    {
        public ActionResult Instructor() => View();
        public ActionResult AddInstructor() => View();
    }
}