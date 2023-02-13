using DpimProject.Models.Data;
using DpimProject.Models.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpimProject.Models.Banner
{
    public class Banner
    {
        TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        private readonly DateTime now;

        public Banner()
        {
            now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
        }

        public dynamic get_all_banner(ref string error)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try
                {
                    var data = db.banner.ToList().Select(x => new
                         {
                             x.id,
                             x.order,
                             x.image_pc,
                             x.image_mobile,
                             x.link,
                             x.is_deleted,
                             x.created_by,
                             x.created_at,
                             x.update_by,
                             x.update_at
                         }).ToList();

                output = new { data };
            }
                catch (Exception ex)
            {
                error = ex.Message;
            }
        }
            return output;
        }

        public dynamic get_banner(string id, ref string error)
        {
            dynamic output = null;

            using (var db = new DataContext())
            {
                var id_int = int.Parse(id);

                try
                {
                    var data = db.banner.Where(x => x.id == id_int).FirstOrDefault();
                    if (data != null)
                    {
                        output = new { data };
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return output;
        }

        public dynamic insert_banner (string order, string image_pc, string image_mobile, string link, ref string error, Authentication.Authentication auth)
        {
            dynamic output = null;

            using (var db = new DataContext()) {
                try
                {
                    //var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
                    banner banner_object = new banner()
                    {
                        order = int.Parse(order),
                        image_pc = image_pc,
                        image_mobile = image_mobile,
                        link = link,
                        is_deleted = 0,
                        created_by = auth.user_id,
                        created_at = now,
                        update_by = auth.user_id,
                        update_at = now
                    };

                    db.banner.Add(banner_object);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return output;
        }

        public dynamic update_banner (string id, string order, string image_pc, string image_mobile, string link, ref string error, Authentication.Authentication auth)
        {
            dynamic output = null;

            using (var db = new DataContext())
            {
                try
                {
                    banner banner_object = new banner()
                    {
                        id = int.Parse(id),
                        order = int.Parse(order),
                        image_pc = image_pc,
                        image_mobile = image_mobile,
                        link = link,
                        update_by = auth.user_id,
                        update_at = now
                    };

                    var data = db.banner.Where(x => x.id == banner_object.id).FirstOrDefault();
                    if (data != null)
                    {
                        data.id = banner_object.id;
                        data.order = banner_object.order;
                        data.image_pc = banner_object.image_pc;
                        data.image_mobile = banner_object.image_mobile;
                        data.link = banner_object.link;
                        data.update_by = banner_object.update_by;
                        data.update_at = banner_object.update_at;
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return output;
        }
    }
}