using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
//using Sap.Data.SQLAnywhere;
using DpimProject.Models.DataTools;
using DpimProject.Models.Data;

namespace DpimProject.Controllers
{
    public class InstuctorController : Controller
    {
        private Models.Authentication.Authentication auth;
        private Models.Instructor.Instructor instructor;
        private DataTools dtl;
        public InstuctorController()
        {
            dtl = new DataTools();
            auth = new Models.Authentication.Authentication();
            instructor = new Models.Instructor.Instructor();

            string token = System.Web.HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();

            //if (!auth.CheckAuthentication(token))
            //{
            //    RedirectToAction("Index", "Home");
            //}
            auth.GetAuthentication(token);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Auth = auth;

            Response.AppendHeader("Access-Control-Allow-Origin", "*");


        }
        // GET: Instuctor
        public ActionResult InstuctorReadList(string search_text,int skip)
        {
            int take = 16;
            skip = (skip > 0) ? skip : skip;
            string error = "";
            var data = instructor.InstructorReadlist(search_text,take, skip, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult InstructorRead(int? instructor_id)
        {
            string error = "";
            var data = instructor.InstructorRead(instructor_id, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult InstructorManage()
        {
            string error = "";


            //if (auth.isAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            //Form Data
            var f = new Models.Data.DataModels.users();

            ////List<Models.Data.DataModels.user_permission> n = new List<Models.Data.DataModels.user_permission>();

            var m = new
            {
                instructor = new Models.Data.DataModels.instructor()

            };
            m = Dtl.json_to_object(Dtl.json_request(), m);
            var data = instructor.InstructorManage(m.instructor, auth, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult CategoryReadList(string search_text,int skip) {
            int take = 16;
            skip = (skip > 0) ? skip : 0;
            string error = "";
            var data = instructor.CategoryReadList(search_text, skip, take, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        } public ActionResult CategoryRead(int? category_id) {

            string error = "";
            var data = instructor.CategoryRead(category_id, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult CategoryManage()
        {
            string error = "";


            //if (auth.isAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            //Form Data

            ////List<Models.Data.DataModels.user_permission> n = new List<Models.Data.DataModels.user_permission>();

            var m = new
            {
                course_category = new Models.Data.DataModels.course_category()

            };
            m = Dtl.json_to_object(Dtl.json_request(), m);
            var data = instructor.CategoryManage(m.course_category, auth, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
    }
}