using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using DpimProject.Models.Data;
using DpimProject.Security;
using System.Data;
using System.Collections.Specialized;
using Newtonsoft.Json;
using DpimProject.Models.DataTools;
namespace DpimProject.Models.Authentication
{
    public class Authentication
    {
        public int? user_id { get; set; } = 0;
        public int? student_id { get; set; } = 0;
        public string username { get; set; } = "";
        //public string password { get; set; } = "";
        public string email { get; set; } = "";
        public bool isAuthenticated { get; set; } = false;
        //public string comp_id { get; set; } = "";
        public bool IsAdmin { get; set; } = false;
        //public bool IsSuperAdmin { get; set; } = false;
        //public bool IsInstructor { get; set; } = false;
        //public bool isStudent { get; set; } = false;
        //public dynamic info { get; set; } = null;
        //public string session_id { get; set; } = "";
        public string profile_path { get; set; } = "";
        public string name_th { get; set; } = "";

        public Authentication()
        {
            //req = new DataRequest();
            isAuthenticated = false;
        }
        public dynamic TEST(string username, string password)
        {
            var output = new { username, password };
            return output;
        }
        //public bool LogIn(string username, string password, bool remember, ref string error)
        //{
        //    try
        //    {
        //        //throw new Exception("Test Exception Error");
        //        isAuthenticated = false;
        //        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //        {
        //            return false;
        //        }

        //        string newSession = "";
        //        Security.Password pwd = new Password();
        //        newSession = pwd.CreatePassword(16);


        //        DataContext db = new DataContext();
        //        //var new_user=   DataTools.Dtl.CreateUserID(db);
        //        //throw new Exception(new_user);
        //        var data = db.user
        //                .Where(x =>
        //                x.username == username || x.email == username
        //                );

        //        if (data.Count() == 0)
        //        {
        //            error = "ไม่พบผู้ใช้งานนี้ในระบบ";
        //            return false;
        //        }
        //        var token = new Token();

        //        var m = data.FirstOrDefault();
        //        var _password = m?.password.Trim();
        //        if (_password.IndexOf("/") > 0)
        //        {
        //            token.CheckToken(_password, out _password);
        //            var dataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(_password);
        //            _password = dataJson["password"].ToString().Trim();

        //        }
        //        if (password != _password)
        //        {
        //            error = "รหัสผ่านไม่ถูกต้อง";
        //            return false;
        //        }

        //        if (m.is_deleted != 0)
        //        {
        //            error = "ไม่ได้รับสิทธิให้เข้าระบบ โปรดติดต่อผู้บริหารระบบ";
        //            return false;
        //        }
        //        if (m.role_id !=3)
        //        {
        //            error = "ไม่ได้รับสิทธิให้เข้าระบบ โปรดติดต่อผู้บริหารระบบ";
        //            return false;
        //        }
        //        //var app_auth = db.app_auth.Where(x => x.user_id == m.id && (x.end_dt > DateTime.Now||(x.active_dt??DateTime.Now)>DateTime.Now.AddHours(6))).FirstOrDefault();
        //        //if (app_auth != null)
        //        //{
        //        //    error = "ท่านลงชื่อเข้าใช้ในระบยเรียบร้อยแล้ว กรุณาลงชื่อออกจากอุปกรณ์ก่อนหน้า";

        //        //}

        //        //password expire

        //        db.SaveChanges();
        //        username = (m.username == username) ? m.username : m.email;
        //        Dictionary<string, object> authData = new Dictionary<string, object>();
        //        authData.Add("session1", newSession);
        //        authData.Add("username", username);
        //        authData.Add("permission", "Admin");

        //        string authDataJson = JsonConvert.SerializeObject(authData);
        //        //if (remember)
        //        //{
        //        authDataJson = token.CreateToken(authDataJson);

        //        HttpContext.Current.Response.Cookies.Add(new HttpCookie("dpim_auth") { Value = authDataJson, Expires = DateTime.UtcNow.AddYears(1) });
        //        //}
        //        this.isAuthenticated = true;
        //        DateTime expire_date = DateTime.Now.AddHours(6);
        //        string session_ref = pwd.CreatePassword(16);
        //        HttpContext.Current.Response.Cookies.Add(new HttpCookie("dpim_auth_session_ref") { Value = session_ref, Expires = DateTime.UtcNow.AddYears(1) });
        //        db.app_auth.RemoveRange(db.app_auth.Where(x => x.end_dt < DateTime.Now || (x.active_dt ?? DateTime.Now) > expire_date).ToList());
        //        db.SaveChanges();
        //        var app = new Data.DataModels.app_auth
        //        {
        //            user_id = m.id,
        //            session_id = newSession,
        //            start_dt = DateTime.Now,
        //            end_dt = DateTime.Now.AddMinutes(1440),
        //            active_dt = DateTime.Now,
        //            passcode = authDataJson
        //        };
        //        db.app_auth.Add(app);
        //        db.SaveChanges();
        //        this.isAuthenticated = true;
        //        this.user_id = m.id;
        //        this.username = m.username;


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.Message;

        //        return false;
        //    }

        //}
        public bool LogIn(string username, string password, bool remember, ref string error)
        {
            string ipAdd = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() ?? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            var UserAgent = HttpContext.Current.Request.UserAgent;

            try
            {
                //throw new Exception("Test Exception Error");
                isAuthenticated = false;
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                string newSession = "";
                Security.Password pwd = new Password();
                newSession = pwd.CreatePassword(16);


                DataContext db = new DataContext();
                //var new_user=   DataTools.Dtl.CreateUserID(db);
                //throw new Exception(new_user);
                var data = db.user
                        .Where(x =>
                        x.username == username || x.email == username
                        );

                if (data.Count() == 0)
                {

                    //error = "ไม่พบผู้ใช้งานนี้ในระบบ";
                    throw new Exception("ไม่พบผู้ใช้งานนี้ในระบบ");
                    //return false;

                }
                var token = new Token();

                var m = data.FirstOrDefault();
                var _password = m?.password.Trim();
                if (_password.IndexOf("/") > 0 || _password.IndexOf("=") > 0 || _password.IndexOf(".") > 0)
                {
                    token.CheckToken(_password, out _password);
                    var dataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(_password);
                    _password = dataJson["password"].ToString().Trim();

                }
                if (password != _password)
                {
                    //error = "รหัสผ่านไม่ถูกต้อง";
                    //return false;
                    throw new Exception("รหัสผ่านไม่ถูกต้อง");
                }

                if (m.is_deleted != 0)
                {

                    //error = "ไม่สามารถลงชื่อเข้าใช้งานระบบ โปรดติดต่อผู้บริหารระบบ";
                    throw new Exception("ไม่สามารถลงชื่อเข้าใช้งานระบบ โปรดติดต่อผู้บริหารระบบ");
                    //return false;

                }
                if (IsAdmin)
                {
                    if (m.role_id == 2)
                    {

                        //error = "ไม่ได้รับสิทธิให้เข้าระบบ โปรดติดต่อผู้บริหารระบบ";
                        //return false;

                        throw new Exception("ไม่ได้รับสิทธิให้เข้าระบบ โปรดติดต่อผู้บริหารระบบ");

                    }
                }
                //var app_auth = db.app_auth.Where(x => x.user_id == m.id && (x.end_dt > DateTime.Now||(x.active_dt??DateTime.Now)>DateTime.Now.AddHours(6))).FirstOrDefault();
                //if (app_auth != null)
                //{
                //    error = "ท่านลงชื่อเข้าใช้ในระบยเรียบร้อยแล้ว กรุณาลงชื่อออกจากอุปกรณ์ก่อนหน้า";

                //}

                //password expire

                db.SaveChanges();
                username = (m.username == username) ? m.username : m.email;
                Dictionary<string, object> authData = new Dictionary<string, object>();
                authData.Add("session1", newSession);
                authData.Add("username", username);
                authData.Add("permission", "Admin");

                string authDataJson = JsonConvert.SerializeObject(authData);
                //if (remember)
                //{
                authDataJson = token.CreateToken(authDataJson);
                //token_text = authDataJson;

                HttpContext.Current.Response.Cookies.Add(new HttpCookie("dpim_admin") { Value = authDataJson, Expires = DateTime.UtcNow.AddYears(1) });
                //}
                this.isAuthenticated = true;
                DateTime? expire_date = DateTime.Now.AddHours(6);
                string session_ref = pwd.CreatePassword(16);
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("dpim_admin_session_ref") { Value = session_ref, Expires = DateTime.UtcNow.AddYears(1) });

                db.app_auth.RemoveRange(db.app_auth.Where(x => x.active_dt > x.end_dt).ToList());
                db.SaveChanges();
                var app = new Data.DataModels.app_auth
                {
                    user_id = m.id,
                    session_id = newSession,
                    start_dt = DateTime.Now,
                    end_dt = DateTime.Now.AddHours(6),
                    active_dt = DateTime.Now,
                    passcode = authDataJson,
                    //ip_address = ipAdd,
                    //device = UserAgent
                };
                var d = db.app_auth.Where(x => x.user_id == m.id).FirstOrDefault();
                if (d == null)
                {
                    db.app_auth.Add(app);
                    db.SaveChanges();
                }
                else
                {
                    d.passcode = authDataJson;
                    d.active_dt = DateTime.Now;
                    d.end_dt = DateTime.Now.AddHours(6);
                    //d.ip_address = ipAdd;
                    //d.device = UserAgent;

                    db.SaveChanges();
                    //throw new Exception("ท่านลงชื่อเข้าใช้ในอุปกรณ์อื่นแล้ว กรุณา Log Out ออกจากระบบก่อน");

                }
                var permiss = db.admin_menu.ToList().Select(a => new
                {
                    a.menu_id,
                    a.menu_name,
                    a.menu_type,
                    status = db.menu_permission.Where(b => b.menu_id == a.menu_id && b.user_id == m.id.ToString()).Select(b => b.status).FirstOrDefault(),
                    disabled = (db.menu_permission.Where(b => b.menu_id == a.menu_id && b.user_id == m.id.ToString()).FirstOrDefault() != null) ? false : true
                }).ToList();
                //this.menu_permission=      db.menu_permission.Where(a => a.user_id == m.id.ToString()).ToList();
                //this.menu_permission = permiss;
                this.isAuthenticated = true;
                this.user_id = m.id;
                this.username = m.username;
                //this.group_department = db.group_department.Where(a => a.user_id == m.id.ToString()).ToList();

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                //ErrorList(ex, HttpStatusCode.OK);
                return false;
            }

        }

        public bool StundenLogin(string username, string password, bool remember, ref string token_text, ref string error)
        {
            try
            {
                isAuthenticated = false;
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                string newSession = "";
                Security.Password pwd = new Password();
                newSession = pwd.CreatePassword(16);
                var token = new Token();


                DataContext db = new DataContext();
                //var new_user=   DataTools.Dtl.CreateUserID(db);
                //throw new Exception(new_user);
                var data = db.user
                        .Where(x =>
                        x.username == username || x.email == username
                        );
                if (data.Count() == 0)
                {
                    error = "ไม่พบผู้ใช้งานนี้ในระบบ";
                    return false;
                }

                var m = data.FirstOrDefault();
                var _password = m?.password.Trim();
                if (_password.IndexOf("/") > 0)
                {
                    token.CheckToken(_password, out _password);
                    var dataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(_password);
                    _password = dataJson["password"].ToString().Trim();

                }

                if (password != _password)
                {
                    error = "รหัสผ่านไม่ถูกต้อง";
                    return false;
                }

                if (m.is_deleted != 0)
                {
                    error = "ไม่ได้รับสิทธิให้เข้าระบบ โปรดติดต่อผู้บริหารระบบ";
                    return false;
                }

                //var app_auth = db.app_auth.Where(x => x.user_id == m.id && (x.end_dt > DateTime.Now||(x.active_dt??DateTime.Now)>DateTime.Now.AddHours(6))).FirstOrDefault();
                //if (app_auth != null)
                //{
                //    error = "ท่านลงชื่อเข้าใช้ในระบยเรียบร้อยแล้ว กรุณาลงชื่อออกจากอุปกรณ์ก่อนหน้า";

                //}

                //password expire

                db.SaveChanges();

                Dictionary<string, object> authData = new Dictionary<string, object>();
                authData.Add("session1", newSession);
                authData.Add("username", username);
                authData.Add("permission", "Student");
                string authDataJson = JsonConvert.SerializeObject(authData);
                //if (remember)
                //{
                authDataJson = token.CreateToken(authDataJson);
                token_text = authDataJson;
                HttpCookie cookie = new HttpCookie("Authorization");
                cookie.Value = authDataJson;
                cookie.Secure = false;
                cookie.HttpOnly = false;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                token_text = authDataJson;
                //}
                this.isAuthenticated = true;
                DateTime expire_date = DateTime.Now.AddHours(6);
                string session_ref = pwd.CreatePassword(16);
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("dpim_admin_session_ref") { Value = session_ref, Expires = DateTime.UtcNow.AddYears(1) });
                db.app_auth.RemoveRange(db.app_auth.Where(x => x.end_dt < DateTime.Now || (x.active_dt ?? DateTime.Now) > expire_date).ToList());
                db.SaveChanges();
                var app = new Data.DataModels.app_auth
                {
                    user_id = m.id,
                    session_id = newSession,
                    start_dt = DateTime.Now,
                    end_dt = DateTime.Now.AddMinutes(1440),
                    active_dt = DateTime.Now,
                    passcode = authDataJson
                };
                db.app_auth.Add(app);
                db.SaveChanges();
                var stu = db.student.Where(x => x.user_id == m.id).FirstOrDefault();

                this.isAuthenticated = true;
                this.user_id = m?.id ?? 0;
                this.student_id = stu?.student_id;
                this.username = m?.username ?? "";
                this.profile_path = stu?.profile_image ?? "";
                this.name_th = $"{stu?.title_name ?? ""}{stu?.firstname ?? ""} {stu?.lastname ?? ""}";
                this.email = m?.email;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                
                return false;
            }

        }

        public void LogOut(Authentication auth, string token_text, ref string error)
        {
            try
            {

                DataContext db = new DataContext();

                db.app_auth.RemoveRange(db.app_auth.Where(x => x.user_id == auth.user_id && x.passcode == token_text).ToList());
                db.SaveChanges();
                db.Dispose();
                //db.hr_emp.Where(x => x.maincode == maincode && x.empno == empno).ToList().ForEach(x => x.session1 = null);
                //db.SaveChanges();
                //db.online_user.RemoveRange(db.online_user.Where(x => x.empno == empno));
                //db.SaveChanges();
                //db.Dispose();

                this.isAuthenticated = false;
                //this.IsSuperAdmin = false;
                this.IsAdmin = false;
                //this.IsInstructor = false;
                this.user_id = null;
                this.username = "";
                HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["dpim_admin"];
                currentUserCookie.Value = null;

                HttpContext.Current.Response.Cookies.Remove("dpim_admin");
                HttpContext.Current.Response.SetCookie(currentUserCookie);



                //this.info = null;
                //HttpContext.Current.Response.Cookies.Clear();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                
            }

        }


        public dynamic UpdateUsers(Data.DataModels.user u, Authentication auth, ref string error_massage)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    if (u != null)
                    {
                        var users = db.user.Where(x => x.username == u.username).FirstOrDefault();
                        //if (data != null)
                        //{
                        //    throw new Exception("มีชื่ออยู่ในระบบเรียบร้อยแล้ว ไม่สามารถสมัครสมาชิกได้");
                        //}
                        if (users != null)
                        {
                            users.username = u.username;
                            users.password = u.password;
                            users.email = u.email;
                            users.is_deleted = u.is_deleted;
                            users.update_at = DateTime.Now;
                            users.update_by = auth.user_id;
                            db.SaveChanges();
                        }
                        output = new
                        {
                            data = users
                        };


                    }
                }
                catch (Exception ex)
                {
                    error_massage = ex.Message;

                    


                }
            }
            return output;
        }
        public void GetAuthentication()
        {
            string mg_auth_token = HttpContext.Current.Request.Cookies["dpim_admin"]?.Value?.Trim();
            if (string.IsNullOrEmpty(mg_auth_token))
            {
                return;
            }
            GetAuthentication(mg_auth_token);
        }
        public void ChangePassword(Models.Data.DataModels.user u, Authentication auth, ref string errorMsg)
        {
            using (var db = new DataContext())
            {
                try
                {
                    var data = db.user.Where(x => x.id == u.id).FirstOrDefault();
                    if (data != null)
                    {
                        data.password = u.password;
                        data.update_at = DateTime.Now;
                        data.update_by = auth.user_id;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;

                    
                }

            }
        }
        public dynamic StudentList(ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var data = (from a in db.user
                                join b1 in db.student on new { a.id } equals new { id = b1.user_id } into b2
                                from b in b2.DefaultIfEmpty()
                                where a.role_id == 2 && !a.email.Contains("@dpim.go.th")
                                select new
                                {
                                    a.id,
                                    a.email,
                                    b.firstname,
                                    b.lastname
                                }).ToList().Select(x => new
                                {
                                    user_id=x.id,
                                    x.email,
                                    x.firstname,
                                    x.lastname,
                                    manage_profile = db.user_manage_profile.Where(a => a.user_id == x.id).FirstOrDefault()
                                }).ToList() ;
                    output = new
                    {
                        data
                    };
                }catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
            return output;
        }
        public dynamic AdminList(int? role_id,string depart_id,ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var password = "";
                    var token = new Token();
                    var data = db.user.ToList().Select(x => new
                    {
                        x.id,
                        x.username,
                        x.email,
                        x.role_id,
password=(x.password.Length>20)?checkTokenPassword(x.password):x.password,
                        group_depart = db.group_department.Where(a => a.user_id == x.id.ToString()).ToList(),
                        menu_permission = db.menu_permission.Where(a => a.user_id == x.id.ToString()).ToList()
                    }).ToList();

                    if (role_id == 2)
                    {
                        data = data.Where(a => a.email.Contains("@dpim.go.th")).ToList();
                    }

                    //if (data == null)
                    //{
                    //    throw new Exception("ไม่มีข้อมูลใน Database");
                    //}
                    //if (!string.IsNullOrEmpty(depart_id))
                    //{

                    //    data = (from a in db.user
                    //            join b1 in db.group_department on new { id = a.id.ToString() } equals new { id = b1.depart_id } into b2
                    //            from b in b2.DefaultIfEmpty()
                    //            where a.role_id == role_id && b.depart_id == depart_id
                    //            select new
                    //            {
                    //                a.id,
                    //                a.username,
                    //                a.email,
                    //                a.password,
                    //                a.role_id

                    //            }).ToList().Select(x => new
                    //            {
                    //                x.id,
                    //                x.username,
                    //                x.email,
                    //                x.role_id,
                    //                password = checkTokenPassword(x.password),
                    //                group_department = db.group_department.Where(a => a.user_id == x.id.ToString()).ToList(),
                    //                menu_permission = db.menu_permission.Where(a => a.user_id == x.id.ToString()).ToList()
                    //            }).ToList();
                    //}
                    ////if (role_id == 2)
                    //{
                    //    data = data.ToList();
                    //}
                    output = new
                    {
                        data
                    };
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }

            }
            return output;
        }
        private string checkTokenPassword(string hashP)
        {
            Token token = new Token();
            var token_ = "";
            var password = "";
            if (hashP.IndexOf(".") > 0 || hashP.IndexOf("/") > 0 || hashP.IndexOf("+") > 0) { 
            var dataJson = token.CheckToken(hashP, out token_);
            var d = JsonConvert.DeserializeObject<Dictionary<string, object>>(token_);
            password = d["password"].ToString().Trim();
            }
            else
            {
                password = hashP;
            }
            return password;
        }
        public void UpdateEditProfile(Data.DataModels.user_manage_profile manage_profile,int? user_id,Authentication auth,ref string errorMsg)
        {
            using (var db = new DataContext())
            {
                //try
                //{
                var data = db.user_manage_profile.Where(a => a.user_id == user_id).FirstOrDefault();
                if (data != null)
                {
                    data.is_edit_address = manage_profile.is_edit_address;
                    data.is_edit_business = manage_profile.is_edit_business;
                    data.is_edit_business_document = manage_profile.is_edit_business_document;
                    data.is_edit_career = manage_profile.is_edit_career;
                    data.is_edit_educational = manage_profile.is_edit_educational;
                    data.is_edit_email = manage_profile.is_edit_email;
                    data.is_edit_front_back_document = manage_profile.is_edit_front_back_document;
                    data.is_edit_front_id_document = manage_profile.is_edit_front_id_document;
                    data.is_edit_know_channal = manage_profile.is_edit_know_channal;
                    data.is_edit_personal_info = manage_profile.is_edit_personal_info;
                    data.is_edit_phone = manage_profile.is_edit_phone;
                    data.is_edit_selfie_document = manage_profile.is_edit_selfie_document;
                    data.update_by = auth.user_id;
                    data.update_dt = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    manage_profile.create_by = auth.user_id;
                    manage_profile.create_dt = DateTime.Now;
                    manage_profile.user_id = user_id;
                    db.user_manage_profile.Add(manage_profile);
                    db.SaveChanges();
                }
                //}catch(Exception ex)
                //{
                //    errorMsg = ex.Message;
                //}
            }
        }
        public void registerAdmin(Data.DataModels.user u,List<Data.DataModels.menu_permission> menu, List<Data.DataModels.group_department> group, Authentication auth, ref string errorMsg)
        {
            using (var db = new DataContext())
            {
                try
                {
                    Token token = new Token();

                    var password = u.password.Trim();
                    Dictionary<string, object> dataJson = new Dictionary<string, object>();
                    dataJson.Add("password", password);
                    var passJson = JsonConvert.SerializeObject(dataJson);
                    password = token.CreateToken(passJson);
                    var data = db.user.Where(x => x.username == u.username || x.email == u.email).FirstOrDefault();
                    if (data != null)
                    {
                        data.is_deleted = 0;
                        data.update_by = auth.user_id;
                        data.role_id = u.role_id;
                        data.password = password;
                        data.update_at = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        var itemno = (db.user.Select(x => x.id).Max() ?? 0);
                        u.id = ++itemno;
                        u.created_at = DateTime.Now;
                        u.password = password;
                        u.created_by = auth.user_id;
                        //u.role_id = 1;
                        u.is_deleted = 0;

                        db.user.Add(u);
                        db.SaveChanges();
                    }
                    //if (string.IsNullOrEmpty(u.password))
                    //{

                    //    var data = db.user.Where(x => x.id == u.id).FirstOrDefault();
                    //    if (data != null)
                    //    {
                    //        //throw new Exception(u.role_id.ToString());
                    //        data.email = u.email;
                    //        data.is_deleted = 0;
                    //        data.update_by = auth.user_id;
                    //        data.role_id = u.role_id;
                    //        data.update_at = DateTime.Now;
                    //        db.SaveChanges();


                    //}
                    //else
                    //{

                    //    Dictionary<string, object> dataJson = new Dictionary<string, object>();
                    //    dataJson.Add("password", password);
                    //    var passJson = JsonConvert.SerializeObject(dataJson);
                    //    password = token.CreateToken(passJson);
                    //        //throw new Exception(u.role_id.ToString());

                    //        var data1 = db.user.Where(x => x.email == u.email||x.username==u.username).FirstOrDefault();
                    //    if (data1 != null)
                    //    {

                    //            data1.username = u?.username;
                    //        data1.password = password;
                    //        data1.is_deleted = 0;
                    //            data1.role_id = u.role_id;

                    //            data1.update_by = auth.user_id;
                    //        data1.update_at = DateTime.Now;
                    //        db.SaveChanges();
                    //        }

                    //        var itemno = (db.user.Select(x => x.id).Max() ?? 0);
                    //        u.id = ++itemno;
                    //        u.created_at = DateTime.Now;
                    //        u.password = password;
                    //        u.created_by = auth.user_id;
                    //        //u.role_id = 1;
                    //        u.is_deleted = 0;

                    //        db.user.Add(u);
                    //        db.SaveChanges();

                    //    }

                    //    }
                    if (menu != null)
                    {
                        db.menu_permission.RemoveRange(db.menu_permission.Where(a => a.user_id == u.id.ToString()));
                        foreach (var m in menu)
                        {
                            m.user_id = u.id.ToString();
                            db.menu_permission.Add(m);
                            db.SaveChanges();

                        }
                    }
                    if (group != null)
                    {
                        db.group_department.RemoveRange(db.group_department.Where(a => a.user_id == u.id.ToString()));
                        foreach(var g in group)
                        {
                            g.user_id = u.id.ToString();
                            db.group_department.Add(g);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
        }
        public dynamic GetDepartment(ref string errorMsg)
        {
            dynamic output = null;

            using (var db = new DataContext())
            {
                try {
                    var data = db.department.ToList();
                    output = new
                    {
                        data
                    };

                } catch(Exception ex) { errorMsg = ex.Message; }
            }
            return output;
        }

        public void DeleteMenu(Data.DataModels.admin_menu admin, ref string errorMsg)
        {
            using (var db = new DataContext())
            {
                try
                {
                    if (admin.menu_type == "H")
                    {
                        db.admin_menu.RemoveRange(db.admin_menu.Where(a => a.menu_id.Contains(admin.menu_id.Substring(0,2))));
                        db.SaveChanges();
                    }
                    else
                    {
                        db.admin_menu.Remove(db.admin_menu.Where(x => x.menu_id == admin.menu_id).FirstOrDefault());

                        db.SaveChanges();
                    }
                }catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
        }
        public void CreateNewMenu(Data.DataModels.admin_menu admin, ref string errorMsg)
        {
            using (var db = new DataContext())
            {
                try
                {

                    var data = db.admin_menu.Where(x => x.menu_id == admin.menu_id).FirstOrDefault();
                    if (data == null)
                    {
                        //if (admin.menu_type == "D")
                        //{
                        //    var itemno = db.admin_menu.Where(x => x.menu_header == admin.menu_header).ToList().Count() + 1;

                        //    admin.menu_id =(itemno<10)?admin.menu_header+"0"+itemno: admin.menu_header + itemno;
                        //}
                        //else
                        //{
                        //    var itemno = db.admin_menu.Where(x => x.menu_type == "H").ToList().Count() + 1;
                        //    admin.menu_id = itemno + "000";
                        //}
                        db.admin_menu.Add(admin);
                        db.SaveChanges();
                    }
                    else
                    {
                        data.menu_name = admin.menu_name;
                        data.menu_type = admin.menu_type;
                        data.menu_header = admin.menu_header;
                        db.SaveChanges();
                    }
                }catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
        }
        public void GetAuthentication(string mg_auth_token)
        {

            isAuthenticated = false;
            if (mg_auth_token != null)
            {
                Token token = new Token();
                Dictionary<string, object> authData = null;
                try
                {
                    token.CheckToken(mg_auth_token, out mg_auth_token);
                    authData = JsonConvert.DeserializeObject<Dictionary<string, object>>(mg_auth_token);
                }
                catch (Exception ex) { }

                if (authData == null || authData.Count == 0)
                {
                    return;
                }
                DataContext db = new DataContext();

                string username = authData["username"].ToString();
                string permiissiion = authData["username"].ToString();
                int? user_id = db.user.Where(x => x.username == username || x.email == username).Select(x => x.id).FirstOrDefault();
                DateTime expire_active = DateTime.Now.AddHours(6);

                var app_auth = db.app_auth.Where(x => x.end_dt < DateTime.Now || (x.active_dt ?? DateTime.Now) > expire_active).FirstOrDefault();
                if (app_auth != null)
                {
                    return;
                }
                var data = db.user.Where(x => x.id == user_id).FirstOrDefault();

                if (data.role_id != 3)
                {
                    throw new Exception("คุณไม่ได้รับสิทธิเข้าใช้งานหน้านี้ กรุณาติดต่อผู้ดูแลระบบ");

                }

                if (data == null) return;
                if (data != null)
                {



                    var stu = db.student.Where(x => x.user_id == data.id).FirstOrDefault();
                    this.profile_path = stu?.profile_image;
                    this.name_th = $"{stu?.title_name}{stu?.firstname} {stu?.lastname}";

                    this.isAuthenticated = true;
                    this.user_id = data?.id;
                    this.username = data?.username ?? "";
                    this.student_id = stu?.student_id;

                    //this.comp_id = comp_id ?? "";
                    //this.info = data;
                    //var info = new
                    //{
                    //    auth = data,

                    //};
                    //this.info = info;


                    var app = db.app_auth.Where(x => x.user_id == user_id).FirstOrDefault();
                    app.active_dt = DateTime.Now;


                    db.SaveChanges();

                    db.app_auth.RemoveRange(db.app_auth.Where(x => (x.end_dt < DateTime.Now || (x.active_dt ?? DateTime.Now) > expire_active)).ToList());
                    db.SaveChanges();

                }
            }

        }
        public void clearAppAuthen(ref string errorMsg)
        {
            using (var db = new DataContext())
            {
                try {

                    db.app_auth.RemoveRange(db.app_auth.ToList());
                    db.SaveChanges();
                    HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["dpim_admin"];
                    currentUserCookie.Value = null;

                    HttpContext.Current.Response.Cookies.Remove("dpim_admin");
                    HttpContext.Current.Response.SetCookie(currentUserCookie);
                } catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
        }
        public bool CheckAuthentication(string mg_auth_token)
        {
            isAuthenticated = false;

            Token token = new Token();
            Dictionary<string, object> authData = null;
            try
            {
                token.CheckToken(mg_auth_token, out mg_auth_token);
                authData = JsonConvert.DeserializeObject<Dictionary<string, object>>(mg_auth_token);
            }
            catch (Exception ex)
            {
                
                return false;

            }

            if (authData == null || authData.Count == 0)
            {
                return false;
            }
            DataContext db = new DataContext();

            string username = authData["username"].ToString();
            string permiissiion = authData["username"].ToString();
            int? user_id = db.user.Where(x => x.username == username || x.email == username).Select(x => x.id).FirstOrDefault();
            DateTime expire_active = DateTime.Now.AddHours(6);

            var app_auth = db.app_auth.Where(x => x.end_dt < DateTime.Now || (x.active_dt ?? DateTime.Now) > expire_active).FirstOrDefault();
            if (app_auth != null)
            {
                return false;
            }
            var data = db.user.Where(x => x.id == user_id).FirstOrDefault();
            if (this.IsAdmin)
            {
                if (data.role_id == 2)
                {
                    return false;

                }
            }
            if (data == null) return false;
            ;
            if (data != null)
            {

                this.isAuthenticated = true;


                var stu = db.student.Where(x => x.user_id == data.id).FirstOrDefault();
                if (stu != null)
                {
                    this.profile_path = stu?.profile_image;
                    this.name_th = $"{stu?.title_name}{stu?.firstname} {stu?.lastname}";
                    this.student_id = stu?.student_id;

                    this.user_id = data?.id;
                    this.username = data?.username ?? "";
                }
                //this.comp_id = comp_id ?? "";
                //this.info = data;
                //var info = new
                //{
                //    auth = data,

                //};
                //this.info = info;


                var app = db.app_auth.Where(x => x.user_id == user_id).FirstOrDefault();
                app.active_dt = DateTime.Now;


                db.SaveChanges();

                db.app_auth.RemoveRange(db.app_auth.Where(x => (x.end_dt < DateTime.Now || (x.active_dt ?? DateTime.Now) > expire_active)).ToList());
                db.SaveChanges();
            }
            return true;

        }
        public dynamic GetMenu(ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var data = db.admin_menu.Where(a=>!a.menu_id.Contains("10")).ToList().OrderBy(x=>x.menu_id);
                    output = new
                    {
                        data
                    };
                }catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
            return output;
        }
    
    }
    public class UserOnline
    {
        public UserOnline()
        {
        }

        public int UserOnlineCount()
        {
          DataContext db = new DataContext();
            var c = db.user_online.Select(x => x.user_id).Distinct().Count();
            db.Dispose();
            return c;
        }

        public void RemoveUserOnline(string user_id)
        {
            DataContext db = new DataContext();
            db.user_online.RemoveRange(db.user_online.Where(x => x.user_id == user_id).ToList());
            db.SaveChanges();
            db.Dispose();
        }
    }

}