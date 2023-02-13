using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using DpimProject.Models.Data;
using DpimProject.Models.Data.DataModels;
using DpimProject.Security;
using System.Data;
using System.Collections.Specialized;
using Newtonsoft.Json;
using DpimProject.Models.DataTools;
using System.IO;
namespace DpimProject.Models.Management
{
    public class Management
    {
        public dynamic TutorialReadList(ref string error)
        {
            dynamic output = null;
                using (var db = new DataContext()) {
                try
                {
                    var data = db.tutorial_header.ToList().Select(x => new
                    {
                        x.tutorial_id,
                        x.tutorial_text,
                        detail = db.tutorial_detail.Where(a => a.tutorial_id == x.tutorial_id).ToList().OrderBy(a => a.detail_id).ToList()
                    }).ToList().OrderBy(x=>x.tutorial_id);
                    output = new
                    {
                        header = data
                    };
                }catch(Exception ex)
                {
                    error = ex.Message;
                }
                
            }
            return output;
        }

        public dynamic EmailList(ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try {
                    var data = (from a in db.email_sending
                                join b1 in db.student on new { a.student_id } equals new { b1.student_id } into b2
                                from b in b2.DefaultIfEmpty()
                                join c1 in db.course on new { a.course_id } equals new { course_id = c1.id } into c2
                                from c in c2.DefaultIfEmpty()
                                select new
                                {
                                    a.email_id,
                                    a.email_type,
                                    a.student_id,
                                    a.course_id,
                                    a.send_dt,
                                    b.firstname,
                                    b.lastname,
                                    b.email,
                                    c.name,
                                    a.subject
                                }).ToList().Select(x => new
                                {
                                    x.email_id,
                                    x.email_type,
                                    x.student_id,
                                    x.course_id,
                                    send_dt = x.send_dt.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                    x.firstname,
                                    x.lastname,
                                    x.email,
                                    course_name = x.name,
                                    x.subject
                                }).ToList();
                    output = new
                    {
                        data
                    };
                } catch(Exception ex) { errorMsg = ex.Message; }
                return output;
            }
        }
        //public void TutorialManage(tutorial_header h,List<tutorial_detail> d,Authentication.Authentication auth,ref string errorMsg)
        //{
        //    using (var db = new DataContext())
        //    {
        //        try
        //        {
        //            var data = db.tutorial_header.Where(x => x.tutorial_id == h.tutorial_id).FirstOrDefault();
        //            if (data != null)
        //            {
        //                data.tutorial_text = h.tutorial_text;
        //                data.update_by = auth.user_id;
        //                data.update_dt = DateTime.Now;
        //                db.SaveChanges();
        //            }
        //            var itemno = db.tutorial_header.Select(x => x.tutorial_id).Max() ?? 0;
        //            h.tutorial_id = ++itemno;
        //            h.create_by = auth.user_id;
        //            h.create_dt = DateTime.Now;
        //            db.tutorial_header.Add(h);
        //            db.SaveChanges();
        //            foreach (var a in d)
        //            {
        //                var d2 = db.tutorial_detail.Where(x => x.tutorial_id == h.tutorial_id && x.detail_id == a.detail_id).FirstOrDefault();
        //                if (d2 != null)
        //                {
        //                    d2.title = a.title;
        //                    d2.img_path = a.img_path;
        //                    db.SaveChanges();
        //                }
        //                var detail = db.tutorial_detail.Where(x => x.tutorial_id == a.tutorial_id).Select(x => x.detail_id).Max() ?? 0;
        //                a.tutorial_id = h.tutorial_id;
        //                a.detail_id = ++detail;
        //                db.tutorial_detail.Add(a);
        //                db.SaveChanges();

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            errorMsg = ex.Message;
        //        }
        //    }
        //}
        public void TutorialDelete(int tutorial_id,ref string errorMsg)
        {
            using (var db = new DataContext())
            {
                try
                {
                    db.tutorial_header.Remove(db.tutorial_header.Where(a => a.tutorial_id == tutorial_id).FirstOrDefault());
                    db.tutorial_detail.RemoveRange(db.tutorial_detail.Where(a => a.tutorial_id == tutorial_id).ToList());
                    db.SaveChanges();
                }catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
                }
        }
    }
}