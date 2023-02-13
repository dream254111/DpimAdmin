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
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]

    public class SettingController : Controller
    {
        private Models.Authentication.Authentication auth;
        private Models.Authentication.UserOnline userOn;
        private string token = System.Web.HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();

        public

            SettingController()
        {


            auth = new Models.Authentication.Authentication();
            auth.GetAuthentication();
            //throw new Exception(JsonConvert.SerializeObject(auth));
            userOn = new Models.Authentication.UserOnline();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Auth = auth;


            base.OnActionExecuting(filterContext);

        }

        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateStructure()
        {
            if (!auth.isAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();

        }

        public ActionResult OnlineUsers()
        {
            return View();
        }

        //public ActionResult OnlineUsers_Data()
        //{
        //    var dtl = new DataTools();
        //    dynamic data = null;
        //    using (var db = new DataContext())
        //    {
        //        var _data = (from a in db.user_online
        //                     join b1 in db.user on new { a.user_id } equals new { b1.user_id } into b2
        //                     from b in b2.DefaultIfEmpty()

        //                     select new { a, b }).OrderBy(x => x.a.session_start).ToList().Select(x => dtl.MergeObject(x.a, new { userid = x.b?.user_id, empfullname_t = x.b?.first_name, })

        //                     //select a

        //                     ).ToList();

        //        data = new
        //        {
        //            data = _data,
        //            count = _data.Count,
        //            max = Licence.MaxUser,
        //            is_admin = auth.IsAdmin
        //        };
        //    }

        //    return Content(JsonConvert.SerializeObject(data), "application/json");
        //}

        public ActionResult UpdateStructureSql(int? skip, int? take)
        {
            var res_l = new List<Dictionary<string, object>>();
            skip = skip ?? 0;
            take = take ?? 0;
            int total = 0;

            //            if (ConfigurationManager.ConnectionStrings["dbProvider"]?.ConnectionString.ToLower() == "sap")
            //            {
            //                //Sybase//
            //                List<string> cmd = new List<string>();
            //                //in ERP
            //                //chq_proj_business 
            //                cmd.Add(@" CREATE TABLE online_user (
            //session_id varchar(50),
            //    user_id varchar(10) NOT NULL,
            //    session_start Datetime ,
            //    session_expire Datetime,
            //    session_active Datetime,
            //ip_address varchar(20),
            //email varchar(255),
            //menu_id varchar(50),
            //menu_name varchar(50),
            //    CONSTRAINT PK_online_user PRIMARY KEY (user_id));"
            //                            );

            //                //cm_cust_contact_address

            //                total = cmd.Count;

            //                SAConnection conn = new SAConnection(Licence.ConnectionString);
            //                SACommand comm = new SACommand();
            //                comm.Connection = conn;

            //                conn.Open();


            //                var c_l = cmd.Skip((int)skip).Take((int)take).ToList();

            //                if (c_l != null && c_l.Count() > 0)
            //                {
            //                    foreach (var c in c_l)
            //                    {
            //                        var res = new Dictionary<string, object>();
            //                        try
            //                        {
            //                            res.Add("command", c.Trim());
            //                            comm.CommandText = c;
            //                            comm.ExecuteNonQuery();
            //                            res.Add("success", true);
            //                            res.Add("error", null);
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            res.Add("success", false);
            //                            res.Add("error", ex.Message);
            //                        }

            //                        res.Add("time", DateTime.Now);
            //                        res_l.Add(res);
            //                    }
            //                }

            //                conn.Close();
            //                conn.Dispose();
            //            }
            //            else
            //            {

            // SQL
            List<string> cmd = new List<string>();


            //chq_proj_business 
            cmd.Add(@"create table test (test_id varchar(255) not null,
                        user_name varchar(255),
                        user_pass varchar(255),
                        CONSTRAINT PK_test PRIMARY KEY (test_id))"
                        );


            cmd.Add(@"Drop table Test");
            //cmd.Add(@"Alter table app_auth add ip_address varchar(10)");
            //cmd.Add(@"Alter table app_auth drop column ip_address ");
            //cmd.Add(@"Alter table app_auth add ip_address varchar(50)");

            //cmd.Add(@"Alter table app_auth add device varchar(255)");
            cmd.Add(@"Create table file_upload (itemno int not null,filepath  text  ,main_id varchar(255),seconde_id varchar(255),third_id varchar(255),
                      CONSTRAINT pk_file_upload primary key(itemno))
                    ");  
                cmd.Add(@"create table certificate (id int,manager_name nvarchar(255), path text,create_dt datetime,create_by int,is_delete int, CONSTRAINT pk_certificate PRIMARY key (id))");
                cmd.Add(@"alter table email_sending add problem_id int");
                cmd.Add(@"alter table course add department_id varchar(10)");
                cmd.Add(@"alter table department add department_name_short nvarchar(255)");
                cmd.Add(@"create table department (id varchar(2) not null,department_name nvarchar(255),department_name_short nvarchar(255), create_by int,create_dt datetime,update_by int, update_dt datetime, CONSTRAINT pk_department primary key (id))");
                cmd.Add(@"alter table email_sending add token nvarchar(MAX)");
                cmd.Add(@"alter table student_forget_password_token add is_use int");
                cmd.Add(@"create table user_manage_profile (user_id int not null,is_edit_address int,is_edit_business int,is_edit_business_document int,is_edit_career int,is_edit_educational int
                        ,is_edit_email int,is_edit_front_back_document int,is_edit_front_id_document int,is_edit_know_channal int,is_edit_personal_info int,is_edit_phone int,is_edit_selfie_document int,
                        create_by int,create_dt Datetime,update_by int,update_dt Datetime,CONSTRAINT pk_user_manage_profile PRIMARY key(user_id))");
            
            //            cmd.Add(@"Drop table course_category");
            //            cmd.Add(@"Create table course_category (id varchar(2) not null,name nvarchar(255),color nvarchar(255) ,CONSTRAINT pk_course_category PRIMARY key (id))");

            //            cmd.Add(@"Drop table course");
            //            cmd.Add(@"Create table course (id varchar(10) not null,
            //                        course_category_id varchar(2) not null,
            //                        department_id varchar(2) not null,
            //                        name nvarchar(255),
            //                        cover_pic text,
            //                        info_cover text,
            //                        is_learning_online int,
            //                        is_has_cost int,
            //                        cost decimal,
            //                        overview_course nvarchar(MAX),
            //                        objective_course nvarchar(MAX),
            //                        print_count int,benefits nvarchar(MAX),
            //                        batch int,
            //                        hasCertificate int,
            //                        isAlwaysRegister int,
            //                        register_start_date datetime,
            //                        register_end_date datetime,
            //                        is_always_learning int,
            //                        learning_startdate datetime,
            //                        learning_enddate datetime,
            //                        video_sample text,
            //                        p_480 text,
            //                        p_720 text,
            //                        p_1080 text,
            //                        p_original text,
            //                        cover_video text,
            //                        contact_name nvarchar(255),
            //                        contact_phone nvarchar(255),
            //                        contact_email nvarchar(255),
            //                        is_deleted int,
            //                        created_by int,
            //                        created_dt datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //                        passed_percent int,
            //                        CONSTRAINT pk_course PRIMARY key (id,course_category_id,department_id))
            //                        ");



            //            cmd.Add(@"Drop table course_lesson");
            //            cmd.Add(@"create table course_lesson (id INT  IDENTITY (1, 1),
            //                        course_id varchar(10) not null,
            //                        instructor_id int,
            //                        [order] int,
            //                        name nvarchar(MAX),
            //                        main_video text,
            //                        main_p_480 text,
            //                        main_p_720 text,
            //                        main_p_1080 text,
            //                        main_p_original text,
            //                        main_cover_video text,
            //                        count_view int,
            //                        is_interactive int,
            //                        interactive_time nvarchar(MAX),
            //                        interactive_video_1 nvarchar(MAX),
            //                        interactive_1_p_480 nvarchar(MAX),
            //                        interactive_1_p_720 nvarchar(MAX),
            //                        interactive_1_p_1080 nvarchar(MAX),
            //                        interactive_1_p_original nvarchar(MAX),
            //                        interactive_1_cover_video nvarchar(MAX),
            //                        interactive_video_2 nvarchar(MAX),
            //                        interactive_2_p_480 nvarchar(MAX),
            //                        interactive_2_p_720 nvarchar(MAX),
            //                        interactive_2_p_1080 nvarchar(MAX),
            //                        interactive_2_p_original nvarchar(MAX),
            //                        interactive_2_cover_video nvarchar(MAX),
            //                        description nvarchar(MAX),
            //                        lesson_time int,
            //                        is_deleted int, 
            //                        created_by int,
            //                        created_dt datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //CONSTRAINT pk_course_lesson PRIMARY key(id,course_id)

            //)
            //                     ");
            //            cmd.Add(@"Drop table course_lesson_exercise");
            //            cmd.Add(@"create table course_lesson_exercise (id INT  IDENTITY (1, 1),
            //                        course_id varchar(10) not null,
            //                        course_lesson_id int not null ,
            //                        [order] int,
            //                        question nvarchar(MAX),
            //                        image nvarchar(MAX),
            //                        video nvarchar(MAX),
            //                        p_480 nvarchar(MAX),
            //                        p_720 nvarchar(MAX),
            //                        p_1080 nvarchar(MAX),
            //                        p_original nvarchar(MAX),
            //                        cover_video nvarchar(MAX),
            //                        is_answer_match int,
            //                        is_answer_choice int,
            //                        is_deleted int,
            //                        created_by int,
            //                        created_dt datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //CONSTRAINT pk_course_lesson_exercise PRIMARY key(id,course_id,course_lesson_id)
            //)
            //                     ");

            //            cmd.Add(@"Drop table course_exam");
            //            cmd.Add(@"create table course_exam (id INT  IDENTITY (1, 1),
            //                        course_id varchar(10)  not null,
            //                        [order] int,
            //                        question nvarchar(MAX),
            //                        percent_pass decimal,
            //                        score int,
            //                        answer int,
            //                        image nvarchar(MAX),
            //                        video nvarchar(MAX),
            //                        p_480 nvarchar(MAX),
            //                        p_720 nvarchar(MAX),
            //                        p_1080 nvarchar(MAX),
            //                        p_original nvarchar(MAX),
            //                        cover_video nvarchar(MAX),
            //                        question_random varchar(1),
            //                        is_deleted int,
            //                        created_by int,
            //                        created_dt datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //CONSTRAINT pk_course_exam PRIMARY key(id,course_id)
            //)
            //                     ");
            //            cmd.Add(@"Drop table student_course_info");
            //            cmd.Add(@"create table student_course_info (id INT  IDENTITY (1, 1),
            //                        course_id varchar(10)  not null,
            //                        student_id int not null ,
            //                        course_lesson_id int  not null ,
            //                        cert_print_count int,
            //                        is_extend_study_time int,
            //                        learning_startdate datetime,
            //                        learning_enddate datetime,
            //                        is_deleted int,
            //                        created_by int,
            //                        created_dt datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //CONSTRAINT pk_student_course_info PRIMARY key(id,course_id,student_id,course_lesson_id
            //))
            //                     ");
            //            cmd.Add(@"Drop table student_course_lesson_progress");
            //            cmd.Add(@"create table student_course_lesson_progress (id INT PRIMARY KEY IDENTITY (1, 1),
            //                       course_id varchar(10) PRIMARY KEY not null,
            //                       student_id int not null ,
            //                       course_lesson_id int not null ,
            //                       is_done_lesson int,
            //                       is_done_exercise int)
            //                     ");
            //            cmd.Add(@"Drop table course_exam_answer");
            //            cmd.Add(@"create table course_exam_answer (id INT  IDENTITY (1, 1),
            //                        course_exam_id int   not null ,
            //                        course_id varchar(10)  not null,
            //                        correct int,
            //                        [order] int,
            //                        answer nvarchar(MAX) ,
            //CONSTRAINT pk_course_exam_answer PRIMARY key(id,course_exam_id,course_id)
            //)
            //                     ");
            //            cmd.Add(@"Drop table course_exam_logging");
            //            cmd.Add(@"create table course_exam_logging (id  INT  IDENTITY (1, 1),
            //                        course_exam_id int  not null ,
            //                        student_id int not null ,
            //                        course_id varchar(10)   not null,
            //                        is_pretest int,
            //                        score int,
            //                        course_exam_answer_id int ,
            //                        is_deleted int,
            //                        created_by int,
            //                        created_dt datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //CONSTRAINT pk_course_exam_logging PRIMARY key(id,course_exam_id,student_id,course_id
            //))

            //                     ");
            //            cmd.Add(@"Drop table course_evaluation");
            //            cmd.Add(@"create table course_evaluation (id INT IDENTITY (1, 1),
            //                        course_id varchar(10)   not null,
            //                        [order] int,
            //                        question nvarchar(MAX),            
            //                        is_free_fill_text int,
            //                        free_fill_text nvarchar(MAX),            
            //                        is_deleted int,
            //                        created_by int,
            //                        created_dt datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //CONSTRAINT pk_course_evaluation PRIMARY key(id,course_id)
            //)
            //                     ");
            //            cmd.Add(@"Drop table course_evaluation_result");
            //            cmd.Add(@"create table course_evaluation_result (id INT  IDENTITY (1, 1),
            //                        course_evaluation_id int not null ,
            //                        student_id int not null ,
            //                        course_id varchar(10) not null,
            //                        course_evaluation_choices_id int,
            //                        answer nvarchar(MAX),            
            //                        is_deleted int,
            //                        created_by int,
            //                        created_dt datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //CONSTRAINT pk_course_evaluation_result PRIMARY key(id,course_evaluation_id,student_id,course_id)
            //)
            //                     ");
            //            cmd.Add(@"Drop table video_on_demand");
            //            cmd.Add(@"create table video_on_demand (id INT IDENTITY (1, 1),
            //                       course_category_id varchar(2) ,
            //                       name nvarchar(255),
            //                       video nvarchar(MAX),
            //                       count_view int,
            //                       description nvarchar(MAX),
            //                       producer_name nvarchar(500),
            //                       phone nvarchar(500),
            //                       email nvarchar(500),
            //                       attachment nvarchar(MAX),
            //                       cover_thumbnail text,
            //                       is_deleted int,
            //                       created_by int,
            //                       created_dt datetime,
            //                       update_by int,
            //                       update_dt datetime,
            //CONSTRAINT pk_video_on_demand PRIMARY key(id)
            //)
            //                     ");
            //            cmd.Add(@"Drop table certificate_info ");
            //cmd.Add(@"create table certificate_info  (
            //                       certificate_id varchar(255) not null ,
            //student_id INT PRIMARY KEY IDENTITY (1, 1),
            //                       course_id varchar(10) not null ,
            //                       certificate_dt datetime,
            //                      path text,
            //                       created_by int,
            //                       created_dt datetime,
            //                       update_by int,
            //                       update_dt datetime,
            //                       cert_status varchar(1),
            //                       CONSTRAINT PK_certificate_info PRIMARY KEY (certificate_id,student_id,course_id))
                                 //");
            //cmd.Add(@"Drop table certificate_print ");
            //cmd.Add(@"create table certificate_print  (id INT IDENTITY (1, 1),

            ////itemno int not null,
            ////student_id int not null ,
            ////                      certificate_type nvarchar(50) ,
            ////                       print_dt datetime,                
            ////certificate_dt datetime,

            ////                      print_by int,
            ////certificate_id varchar(255) ,
            ////course_id varchar(10),
            ////                       CONSTRAINT PK_certificate_print PRIMARY KEY (id,itemno,student_id))
            ////)
            ////                     ");
            //            cmd.Add(@"Drop table email_sending ");
            //            cmd.Add(@"create table email_sending  (email_id INT not null,

            //student_id int not null ,
            //                      from_email nvarchar(255) ,
            //                      subject nvarchar(255) ,
            //                      body_email nvarchar(MAX) ,
            //                       send_dt datetime,                
            //                      send_by int,
            //email_type varchar(10) ,
            //course_id varchar(255) ,
            //problem_id int,

            //                       CONSTRAINT PK_email_sending PRIMARY KEY (email_id,student_id))
            //                     ");
            //            cmd.Add(@"Drop table interactive_question ");
            //            cmd.Add(@"create table interactive_question  (id INT IDENTITY (1, 1),
            //course_id varchar(255) ,
            //course_lesson_id int,
            //[order] int,

            //                      name nvarchar(MAX) ,
            //                      interactive_time nvarchar(MAX)  ,
            //                       is_deleted int,
            //                        created_by int,
            //                        created_at datetime,
            //                        update_by int,
            //                        update_at datetime,
            //                       CONSTRAINT PK_interactive_question PRIMARY KEY (id)
            //)
            //                     ");
            //            cmd.Add(@"Drop table student_video_progress ");
            //            cmd.Add(@"create table student_video_progress  (id INT  IDENTITY (1, 1),
            //student_id int ,
            //course_id varchar(10) ,
            //course_lesson_id int,
            //video_path text,
            //video_position decimal,
            //video_progress decimal,   
            //                        create_by int,
            //                        create_at datetime,
            //                        update_by int,
            //                        update_dt datetime,
            //                       CONSTRAINT PK_student_video_progress PRIMARY KEY (id)
            //)
            //                     "); 
//            cmd.Add(@"create table course_voucher (voucher_id varchar(10) not null,
//course_id varchar(10) not null,
//start_dt Datetime,
//end_dt Datetime,
//is_delete int,
//created_by int,
//created_at Datetime,
//update_by int,
//update_dt Datetime,
//CONSTRAINT pk_course_voucher PRIMARY key(id,course_id))
//                     ");
//            cmd.Add(@"alter table student_course_info add voucher_id varchar(10)
//                     ");
            //cmd.Add(@"Alter table video_status drop column course_id  ");
            ////cmd.Add(@"Alter table video_status add course_id INT PRIMARY KEY IDENTITY (1, 1)");
            //cmd.Add(@"Alter table video_status  drop column course_lesson_id ");
            //cmd.Add(@"Alter table video_status add course_lesson_id INT PRIMARY KEY IDENTITY (1, 1)");
            //cmd.Add(SqlServerDropPK("video_status"));
            //cmd.Add($@" alter table video_status add constraint pk_video_status primary key (course_id,course_lesson_id,filename)");

            //cm_cust_contact_address

            //cmd.Add(@" CREATE TABLE users (
            //            user_id varchar(255) not null,
            //            email varchar(255) not null,
            //            firstname varchar(255),
            //            lastname varchar(255),
            //            username varchar(255),
            //            password varchar(255),
            //            path_pic text,
            //            active varchar(1),
            //            sessions varchar(100),
            //            add_dt Datetime,
            //            edit_dt Datetime,
            //            del_dt Datetime,
            //            edit_by varchar(255),
            //            add_by varchar(255),
            //            del_by varchar(255),

            //            CONSTRAINT PK_users PRIMARY KEY (user_id,email))"
            // );

            total = cmd.Count;
            ViewBag.total = cmd.Count();
            SqlConnection conn = new SqlConnection(Licence.ConnectionString);
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;

            conn.Open();

            var c_l = cmd.Skip((int)skip).Take((int)take).ToList();

            if (c_l != null && c_l.Count() > 0)
            {
                foreach (var c in c_l)
                {
                    var res = new Dictionary<string, object>();
                    try
                    {
                        res.Add("command", c.Trim());
                        comm.CommandText = c;
                        comm.ExecuteNonQuery();
                        res.Add("success", true);
                        res.Add("error", null);
                    }
                    catch (Exception ex)
                    {
                        res.Add("success", false);
                        res.Add("error", ex.Message);
                    }

                    res.Add("time", DateTime.Now);
                    res_l.Add(res);
                }
            }
            //}

            var o = new
            {
                total = total,
                result = res_l
            };

            return Content(JsonConvert.SerializeObject(o), "application/json");
        }


        public ActionResult ShowLog()
        {

            return View();
        }

        //public ActionResult ShowLog_Data()
        //{
        //    var dtl = new DataTools();
        //    string error = "";

        //    var cond = new
        //    {
        //        skip = 0,
        //        take = 0,
        //        start = "",
        //        end = "",
        //        text = "",
        //        userid = ""
        //    };

        //    try
        //    {
        //        string json = dtl.JsonRequest();
        //        cond = JsonConvert.DeserializeAnonymousType(json, cond);
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.Message;
        //    }

        //    int skip = cond.skip;
        //    int take = cond.take;
        //    DateTime? start = dtl.ObjToDateTime(cond.start);
        //    DateTime? end = dtl.ObjToDateTime(cond.end);
        //    string userid = cond.userid;
        //    string text = cond.text;

        //    var _data = new LogManager(auth).ReadList(skip, take, start, end, userid, text);

        //    return Content(JsonConvert.SerializeObject(new { data = _data?.data, total = _data?.total ?? 0, cond, error, start, end }), "application/json");
        //}

        public ActionResult RestartApp()
        {
            return View();
        }

        public ActionResult RestartApp_do()
        {
            System.Web.HttpRuntime.UnloadAppDomain();
            return Content("true");
        }

        public ActionResult RestartApp_check()
        {
            return Content("true");
        }

        public string SqlServerDropPK(string table_name)
        {
            string tmp = $@"DECLARE @SQL VARCHAR(4000)
SET @SQL = 'ALTER TABLE {table_name} DROP CONSTRAINT |ConstraintName| '
SET @SQL = REPLACE(@SQL, '|ConstraintName|', (SELECT name FROM sysobjects WHERE xtype = 'PK' AND parent_obj = OBJECT_ID('{table_name}')))
EXEC(@SQL)";

            return tmp;
        }

        public ActionResult SybasePatch()
        {
            return Content("");
        }

        public ActionResult Patch()
        {
            return View();
        }

        public ActionResult Patch_EmailAndPhone()
        {
            string error = "";
            dynamic resp = null as object;
            using (var db = new DataContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //patch address


                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        error = ex.Message;
                        if (ex.InnerException != null)
                        {
                            error = ex.InnerException.GetBaseException().Message;
                        }
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(new
            {
                success = string.IsNullOrEmpty(error),
                error,
                detail = resp
            }), "application/json");
        }

        //public ActionResult Fix_UnitStatus()
        //{
        //    using (var db = new DataContext())
        //    {
        //        using (var tr = db.Database.BeginTransaction())
        //        {
        //            try
        //            {

        //                var unit = db.chq_proj.OrderBy(x => x.maincode).ThenBy(x => x.pre_event).ToList();
        //                var q1 = (from a in db.rd_trn_presale_header
        //                          join b in db.rd_trn_presale_detail on new { a.maincode, a.docno } equals new { b.maincode, b.docno }
        //                          where (a.active ?? "Y") == "Y" && (b.active ?? "Y") == "Y"
        //                          select new
        //                          {
        //                              a.maincode,
        //                              a.pre_event2,
        //                              b.pre_event,
        //                              status = "P"
        //                          });

        //                var q2 = (from a in db.rd_trn_header
        //                          where (a.active ?? "Y") == "Y"
        //                          orderby a.docno
        //                          select new
        //                          {
        //                              a.maincode,
        //                              a.pre_event2,
        //                              a.pre_event,
        //                              status = (a.transfer_status ?? "Y") == "Y" ? "T" :
        //                              (a.down_status ?? "Y") == "Y" ? "D" :
        //                              (a.contract_status ?? "Y") == "Y" ? "C" : "B"
        //                          });

        //                var ex1 = q1.Union(q2).OrderBy(x => x.maincode).ThenBy(x => x.pre_event).ToList();

        //                unit.ForEach(x => x.unitstatus = "0");
        //                ex1.ForEach(x => {
        //                    unit.Where(z => z.maincode == x.maincode && z.pre_event == x.pre_event).ToList().ForEach(z => z.unitstatus = x.status);
        //                });
        //                db.SaveChanges();

        //                tr.Commit();
        //            }
        //            catch
        //            {
        //                tr.Rollback();
        //            }

        //        }
        //    }

        //    return Content("done");
        //}
    }
}