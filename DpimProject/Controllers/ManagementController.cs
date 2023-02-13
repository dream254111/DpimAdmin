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
    public class ManagementController : Controller
    {
        // GET: Management 
        private Models.Authentication.Authentication auth ;
        private Models.Management.Management manage;
        private DataTools dtl;
        public ManagementController()
        {
            dtl = new DataTools();
            auth = new Models.Authentication.Authentication();
            manage = new Models.Management.Management();
            string token = System.Web.HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();

            if (!auth.CheckAuthentication(token))
            {
                new HttpStatusCodeResult(403, "Access Denied");
            }
            auth.GetAuthentication(token);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Auth = auth;
            Response.AppendHeader("Access-Control-Allow-Origin", "*");



        }
       public ActionResult TutorialReadList()
        {
            string error = "";
            var data = manage.TutorialReadList(ref error);

            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        //public ActionResult TutorialManage()
        //{
        //    string error = "";

        //    var m = new
        //    {
        //        header = new Models.Data.DataModels.tutorial_header(),
        //        detail = new List<Models.Data.DataModels.tutorial_detail>()

        //    };
        //    m = Dtl.json_to_object(Dtl.json_request(), m);
        //    manage.TutorialManage(m.header, m.detail, auth, ref error);
        //    var output = new
        //    {
        //        success = string.IsNullOrEmpty(error),
        //        error
        //    };
        //    return Content(JsonConvert.SerializeObject(output), "application/json");

        //}
        public ActionResult TutorialDelete(int tutorial_id)
        {
            string error = "";
            manage.TutorialDelete(tutorial_id, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult EmailList()
        {
            string error = "";
          var data=  manage.EmailList(ref error);
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