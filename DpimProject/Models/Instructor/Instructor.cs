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
using System.Net;
using System.Net.Mail;
namespace DpimProject.Models.Instructor
{
    public class Instructor
    {
        public dynamic InstructorReadlist(string search_text, int take, int skip, ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var data = db.instructor.ToList().Select(x => new
                    {
                        x.id,
                        x.firstname,
                        x.lastname,
                        name_th = $"{x.firstname} {x.lastname}",
                        name_en = $"{x.firstname_en} {x.lastname_en}",
                        x.position,
                        x.phone,
                        x.email,

                    });

                    if (!string.IsNullOrEmpty(search_text))
                    {
                        data = data.Where(x => x.firstname == search_text || x.lastname == search_text || x.email == search_text || x.position == search_text || x.phone == search_text || x.email == search_text);

                    }
                    data = data.ToList().Skip(skip).Take(take);
                    output = new
                    {
                        data
                    };
                } catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
                return output;
            }

        }
        public dynamic InstructorRead(int? instructor_id, ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try {
                    var data = db.instructor.Where(x => x.id == instructor_id).FirstOrDefault();
                    if (data != null)
                    {
                        output = new { data };
                    }
                } catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
            return output;
        }
        public dynamic InstructorManage(instructor ins,Authentication.Authentication auth, ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var data = db.instructor.Where(x => x.id == ins.id).FirstOrDefault();
                    if (data != null)
                    {
                        data.id = ins.id;
                        data.firstname = ins.firstname;
                        data.lastname = ins.lastname;
                        data.profile_image = ins.profile_image;
                        data.position = ins.position;
                        data.work = ins.work;
                        data.email = ins.email;
                        data.title_name = ins.title_name;
                        data.phone = ins.phone;
                        data.facebook = ins.facebook;
                        data.twitter = ins.twitter;
                        data.instagram = ins.instagram;
                        data.description = ins.description;
                        data.is_deleted = ins.is_deleted;
                      
                        data.update_by = auth.user_id;
                        data.update_at =DateTime.Now;
                        data.firstname_en = ins.firstname_en;
                        data.lastname_en = ins.lastname_en;
                        db.SaveChanges();
                    }
                    ins.is_deleted = 0;
                    ins.created_at = DateTime.Now;
                    ins.created_by = auth.user_id;
                    db.instructor.Add(ins);
                    db.SaveChanges();
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
        public dynamic CategoryReadList(string search_text,int skip, int take, ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var data = db.course_category.ToList().Select(x =>
                    new
                    {
                        x.id,
                        x.name
                    });
                    if (!string.IsNullOrEmpty(search_text))
                    {
                        data = data.Where(x => x.name == search_text);
                    }
                    data = data.Skip(skip).Take(take).ToList();
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
           public dynamic CategoryRead(int? category_id, ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var data = db.course_category.Where(x => x.id == category_id).FirstOrDefault();
                    if (data != null)
                    {
                        output = new
                        {
                            data
                        };
                    }
                }catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
            return output;
        }
        public dynamic CategoryManage(course_category cat, Authentication.Authentication auth, ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var data = db.course_category.Where(x => x.id == cat.id).FirstOrDefault();
                    if (data != null)
                    {
                        data.id = cat.id;
                        data.name = cat.name;
                        data.color = cat.color;

                        db.SaveChanges();
                    }

                    
                    db.course_category.Add(cat);
                    db.SaveChanges();
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
        public void SendingEmail(int? instructor_id,ref string errorMsg)
        {
            using (var db = new DataContext()) { 
                try
                {
                    var ins = db.instructor.Where(x => x.id == instructor_id).FirstOrDefault();
                    var fromAddress = new MailAddress("from@gmail.com", "From Name");
                    var toAddress = new MailAddress("to@example.com", "To Name");
                    const string fromPassword = "fromPassword";
                    const string subject = "Subject";
                    const string body = "Body";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
                }
        }
    }
}