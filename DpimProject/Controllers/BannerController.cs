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
    public class BannerController : Controller
    {
        // GET: Banner
        private Models.Authentication.Authentication auth;
        private Models.Banner.Banner banner;
        private DataTools dtl;

        public BannerController()
        {
            dtl = new DataTools();
            auth = new Models.Authentication.Authentication();
            banner = new Models.Banner.Banner();

            string token = System.Web.HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();

            //if (!auth.CheckAuthentication(token))
            //{
            //    RedirectToAction("Index", "Home");
            //}

            auth.GetAuthentication(token);
        }

        /* Test succeed (2020/11/23) */
        /*
        public ActionResult TestResult()
        {
            return Content("test555", "application/json");
        }
        */

        public ActionResult GetAllBanner()
        {
            var error = "";
            var data = banner.get_all_banner(ref error);
            var output = new {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };

            return Content(JsonConvert.SerializeObject(output), "application/json");
        }

        public ActionResult GetBanner(string id)
        {
            var error = "";
            var data = banner.get_banner(id, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };

            return Content(JsonConvert.SerializeObject(output), "application/json");
        }

        public ActionResult InsertBanner(string order, string image_pc, string image_mobile, string link)
        {
            var error = "";
            var data = banner.insert_banner(order, image_pc, image_mobile, link, ref error, auth);
            var output = new {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };

            return Content(JsonConvert.SerializeObject(output), "application/json");
        }

        public ActionResult UpdateBanner(string id, string order, string image_pc, string image_mobile, string link)
        {
            var error = "";
            var data = banner.update_banner(id, order, image_pc, image_mobile, link, ref error, auth);
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