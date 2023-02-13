using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DpimProject.Models.DataTools;
using DpimProject.Models.Data;
using DpimProject.Models.Data.DataModels;
using static DpimProject.Models.Course.Course;

namespace DpimProject.Controllers
{
    public class CourseController : Controller
    {
        // GET: Authentication
        private Models.Authentication.Authentication auth;
        private Models.Course.Course course;
        private Models.Authentication.UserOnline userOn;
        private string token = System.Web.HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();
        private readonly DataContext db;

        public CourseController()
        {
            auth = new Models.Authentication.Authentication();
            course = new Models.Course.Course();
            auth.GetAuthentication();
            db = new DataContext();
            userOn = new Models.Authentication.UserOnline();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Auth = auth;
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

        }
        
        public ActionResult all_course()
        {
            string error = "";
            var data = course.get_all_course(ref error);
            return Content(JsonConvert.SerializeObject(data), "application/json");
        }
        
        public ActionResult remove_course()
        {
            string error = "";
            var f = new
            {
                id = ""
            };

            var get_body = Dtl.json_to_object(Dtl.json_request(), f);
            if (get_body == null)
                return new HttpStatusCodeResult(404);

            var data = course.remove_course(get_body.id, auth, ref error);
            if (data == null)
                return new HttpStatusCodeResult(404);
            
            return Content(JsonConvert.SerializeObject(data), "application/json");
        }

    }
}