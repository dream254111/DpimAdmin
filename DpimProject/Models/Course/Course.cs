using DpimProject.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpimProject.Models.Course
{
    public class Course
    {
        private readonly DataContext db;
        TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        private readonly DateTime now;

        public Course()
        {
            db = new DataContext();
            now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
        }

        public dynamic get_all_course(ref string error_message)
        {
            dynamic output = null;
            try
            {
                var get_lesson_course = db.course_lesson.Where(w => w.is_deleted == 0);
                output = (from c in db.course
                          where c.is_deleted == 0
                          select new
                          {
                              id = c.id,
                              name = c.name,
                              cover_img = c.cover_pic,
                              count_lesson = get_lesson_course.Where(w => w.course_id == c.id).Count(),
                              lesson_time = get_lesson_course.Where(w => w.course_id == c.id).Sum(s => s.lesson_time)
                          }).ToList();
            }
            catch (Exception ex)
            {
                error_message = ex.Message;
            }
            return output;
        }
        
        public dynamic remove_course(string id, Authentication.Authentication auth, ref string error_message)
        {
            dynamic output = null;
            try
            {
                var data = db.course.Where(w => w.id == id && w.is_deleted == 0).FirstOrDefault();
                if(data != null)
                {
                    data.is_deleted = 1;
                    data.update_at = now;
                    data.update_by = auth.user_id;
                    db.SaveChanges();

                    output = new
                    {
                        data
                    };
                }
                else
                {
                    output = null;
                }
            }
            catch (Exception ex)
            {
                error_message = ex.Message;
            }
            return output;
        }
    }
}