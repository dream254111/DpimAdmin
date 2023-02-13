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
    public class ReportingController : Controller
    {
        // GET: Reporting
        private Models.Authentication.Authentication auth;
        private Models.Report.ReportStudent reportStudent;
        private DataTools dtl;
        public ReportingController()
        {
            dtl = new DataTools();
            auth = new Models.Authentication.Authentication();
            reportStudent = new Models.Report.ReportStudent();

            string token = System.Web.HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();

            //if (!auth.CheckAuthentication(token))
            //{
            //    RedirectToAction("Index", "Home");
            //}
            auth.GetAuthentication(token);
        }
        //รายงานรายชื่อนักเรียน
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Auth = auth;
            Response.AppendHeader("Access-Control-Allow-Origin", "*");



        }
        public ActionResult StudentOfCourse(string search_text, int skip)
        {
            int take = 16;
            skip = (skip > 0) ? skip : 0;
            string error = "";
            var data = reportStudent.ReportStudentCourse(search_text, take,skip,ref error);
            if (data == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");

        }
        //Report
        //แบบประเมิน
        public ActionResult EvaluationReport(string search_text,int skip)
        {
            int take = 16; skip = (skip > 0) ? skip : 0;

            string error = "";
            var data = reportStudent.EvaluationReport(search_text,take,skip,ref error);
            if (data == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        //Report
        //นักเรียน
        public ActionResult StudentReport(string search_text, string course_id ,int skip)
        {
            int take = 16; skip = (skip > 0) ? skip : 0;

            string error = "";
            var data = reportStudent.StudentReport(search_text, course_id, take, skip, ref error);
            if (data == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        //Report
        //การทำข้อสอบ
        public ActionResult StudentExamPressReport(string search_text ,int skip)
        {
            int take = 16; skip = (skip > 0) ? skip : 0;

            string error = "";
            var data = reportStudent.StudentExamPressReport(take,skip,ref error);
            if (data == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        //Report
        //พืมพ์ Certificate Category
        public ActionResult CertificateCategoryPrintReport(string search_text ,int skip)
        {
            int take = 16; skip = (skip > 0) ? skip : 0;

            string error = "";
            var data = reportStudent.CertificateCategoryPrintReport(search_text,take,skip,ref error);
            if (data == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        //Report
        //พืมพ์ Certificate Course
        public ActionResult CertificateCoursePrintReport(string search_text ,int skip)
        {
            int take = 16; skip = (skip > 0) ? skip : 0;

            string error = "";
            var data = reportStudent.CertificateCoursePrintReport(search_text,take,skip,ref error);

            if (data == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };

            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        //Report
        //พื้นใน Harddisk 
        public ActionResult DriveInfoReport()
        {
            string error = "";
            var data = reportStudent.DriveSpecReport(ref error);
            if (data == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var output = new
            {
                success = string.IsNullOrEmpty(error),
                error,
                data
            };
            return Content(JsonConvert.SerializeObject(output), "application/json");
        }
        //Report
        //Video On demain View
        public ActionResult ViewCountVideoReport(string search_text, int skip)
        {
            string error = "";
            int take = 16; skip = (skip > 0) ? skip : 0;

            var data = reportStudent.ViewCountVideoReport(search_text, take, skip,ref error);
            if (data == null)
            {
                return new HttpStatusCodeResult(404);
            }
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