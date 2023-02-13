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

    public class AuthenticationController : Controller
    {
        // GET: Authentication
        private Models.Authentication.Authentication auth ;
        private DataTools dtl;
        public AuthenticationController() {
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
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            base.OnActionExecuting(filterContext);


        }
        public ActionResult Index()
        {
            //if (auth.isAuthenticated)
            //{
            //    return RedirectToAction()
            //}
            if (auth.isAuthenticated)
            {
                return RedirectToAction("AdminManage", "Home");
            }
            return View();
        }
     
        public ActionResult Login()
        {
             string error = "";

                ViewBag.userAllow = true;


                var f = new Models.Data.DataModels.users();


                var m = new
                {
                    username = "",
                    password = ""

                };
                m = Dtl.json_to_object(Dtl.json_request(), m);

                auth.LogIn(m.username, m.password, false, ref error);
                var output = new
                {
                    success = string.IsNullOrEmpty(error),
                    error
                };

            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult UsersRegister()
        {
            return View();
        }
       
        public ActionResult CreateNewUsers()
        {
            string error = "";

            DataTools dtl = new DataTools();
            ViewBag.userAllow = true;

            //if (auth.isAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            //Form Data
            var f = new Models.Data.DataModels.user();
          
           
            var m = new
            {
                users = f,

            };
            m = Dtl.json_to_object(Dtl.json_request(), m);
            //throw new Exception(JsonConvert.SerializeObject(m));

            //auth.CreateNewUsers(m.users, auth, ref error);

            var output = new
            {
                seccuss = string.IsNullOrEmpty(error),
                error,

            };
            return Content(JsonConvert.SerializeObject(output), "application/json");


        }
        public ActionResult RegisterAdmin()
        {
            string error = "";
            var f = new
            {
                user = new Models.Data.DataModels.user(),
                menu_permission=new List<Models.Data.DataModels.menu_permission>(),
                group_department=new List<Models.Data.DataModels.group_department>(),
                token = ""
            };
            f = Dtl.json_to_object(Dtl.json_request(), f);
           
                auth.registerAdmin(f.user,f.menu_permission,f.group_department, auth, ref error);
            //throw new Exception(JsonConvert.SerializeObject(f.user));
           
          
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,


            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        public ActionResult UpdateEditProfile()
        {
            string error = "";
            var f = new
            {
                user_manage_profile = new Models.Data.DataModels.user_manage_profile(),
                user_id = 0
            };
            f = Dtl.json_to_object(Dtl.json_request(), f);

            auth.UpdateEditProfile(f.user_manage_profile, f.user_id, auth, ref error);
            //throw new Exception(JsonConvert.SerializeObject(f.user));


            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,


            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        public ActionResult AdminList(int? role_id,string depart_id)
        {
            string error = "";
            //GetToken(ref error);
            role_id = (role_id == null) ? 1 : role_id;
            var data = auth.AdminList(role_id,depart_id,ref error);
            var student = auth.StudentList(ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data,
                student

            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        public ActionResult GetMenu()
        {
            string error = "";
            var data = auth.GetMenu(ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
            error,
            data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        public ActionResult CreateNewMenu()
        {
            string error = "";
            var f = new Models.Data.DataModels.admin_menu();
            f = Dtl.json_to_object(Dtl.json_request(), f);

            auth.CreateNewMenu(f, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        } public ActionResult DeleteMenu()
        {
            string error = "";
            var f = new Models.Data.DataModels.admin_menu();
            f = Dtl.json_to_object(Dtl.json_request(), f);

            auth.DeleteMenu(f, ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult GetDepartment()
        {
            string error = "";
            var data = auth.GetDepartment(ref error);
            var output = new { success=string.IsNullOrEmpty(error),error,data };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult ClearAppAuthen()
        {
            string error = "";
            auth.clearAppAuthen(ref error);
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult AuthenLogin()
        {
            string token = System.Web.HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();
            var output = new
            {
                auth
                ,token
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        public ActionResult LogOut()
        {
            string token = System.Web.HttpContext.Current.Request.Cookies["dpim_auth"]?.Value?.Trim();

            string error = "";

            auth.LogOut(auth, token, ref error);

         

            //token_text = token;
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,

            };
           
                return RedirectToAction("Index", "Home");
            
        }

    }
}