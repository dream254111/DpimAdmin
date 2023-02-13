using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DpimProject.Models.DataTools;
namespace DpimProject.Controllers
{
    public class HomeController : Controller
    {
        private Models.Authentication.Authentication auth;
        private DataTools dtl;
        public HomeController()
        {
            dtl = new DataTools();
            auth = new Models.Authentication.Authentication();
            string token = System.Web.HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();

            //if (!auth.CheckAuthentication(token))
            //{
            //     new HttpStatusCodeResult(403,"Access Denied");
            //}
            if (token != null)
            {
                auth.GetAuthentication(token);
            }
        }
        public ActionResult Index()
        {
            //string token = System.Web.HttpContext.Current.Request.Cookies["dpim_auth"]?.Value?.Trim();

            ////if (!auth.CheckAuthentication(token))
            ////{
            ////     new HttpStatusCodeResult(403,"Access Denied");
            ////}
            //if (token != null)
            //{
            //    auth.GetAuthentication(token);
            //}
            if (auth.isAuthenticated)
            {
                return RedirectToAction("AdminManage", "Home");

            }
            return View();
        }
        public ActionResult AdminManage()
        {
            //string token = System.Web.HttpContext.Current.Request.Cookies["dpim_auth"]?.Value?.Trim();

            ////if (!auth.CheckAuthentication(token))
            ////{
            ////     new HttpStatusCodeResult(403,"Access Denied");
            ////}
            //if (token != null)
            //{
            //    auth.GetAuthentication(token);
            //}
            if (!auth.isAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Certificate()
        {
           
            return View();
        }
        public ActionResult SendingEmail()
        {
            //string token = System.Web.HttpContext.Current.Request.Cookies["dpim_auth"]?.Value?.Trim();

            //if (!auth.CheckAuthentication(token))
            //{
            //    new HttpStatusCodeResult(403, "Access Denied");
            //}
            //if (token != null)
            //{
            //    auth.GetAuthentication(token);
            //}
            if (!auth.isAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }
    }
}