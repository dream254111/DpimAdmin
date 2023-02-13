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
namespace DpimProject.Models.Report
{
    public class ReportStudent
    {
        private string[] pic_mapping = new string[] { "jpg", "jpeg", "png", "gif", "bmp" };
        private string[] doc_mapping = new string[] { "doc", "docx", "xls", "xlsx", "pdf", "csv", "pptx", "ppt" };
        private string[] video_mapping = new string[] { "mp4", "mpeg", "3gp" };
        private string virtual_dir = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UploadPath"]) ? @"C:\fileuploads\" : System.Configuration.ConfigurationManager.AppSettings["UploadPath"]+ "\\";

        //รายงานรายชื่อนักเรียน
        public dynamic ReportStudentCourse(string search_text, int take,int skip,ref string error_message)
        {
            dynamic output = null;
            using (var db = new DataContext()){
                try
                {
                    System.Globalization.CultureInfo th_format = new System.Globalization.CultureInfo("th-Th");
                    var data = (from a in db.student
                                join b1 in db.student_account_type on new { a.student_account_type_id } equals new { student_account_type_id = b1.id } into b2
                                from b in b2.DefaultIfEmpty()

                                select new
                                {
                                    a.course_id,
                                    a.firstname,
                                    a.firstname_en,
                                    a.lastname,
                                    a.lastname_en,
                                   
                                    a.email,
                                    a.student_account_type_id,
                                    student_type_name = b.name,
                                    course_row_total = db.student_course_info.Where(x => x.student_id == a.id).ToList().Count(),
                                    a.phone,
                                    a.position,
                                    add_date = a.update_at,
                                    a.province_id,
                                }).ToList().Select(x => new
                                {
                                    x.course_id,
                                    x.firstname,
                                    x.firstname_en,
                                    x.lastname,
                                    x.lastname_en,
                                    name_th = $"{x.firstname} {x.lastname}",
                                    name_en = $"{x.firstname_en} {x.lastname_en}",
                                    x.email,
                                    x.student_account_type_id,
                                    x.student_type_name,
                                    x.course_row_total,
                                    x.phone,
                                    x.position,
                                    add_date = x.add_date?.ToString("dd MMMM YYYY", th_format),
                                    x.province_id,
                                });
                    if (search_text != null)
                    {
                        data = data.Where(x => x.name_th == search_text||x.name_en==search_text).ToList();
                    }
                    data = data.ToList().Skip(skip).Take(take);
                    output = new
                    {
                        data,
                        page_total = data.Count(),
                        
                    };
                }
                catch (Exception ex)
                {
                    error_message= ex.Message;
                }
            }
            return output;
        }
        //รายงานผลแบบประเมิน

        public dynamic EvaluationReport(string search_text,int take,int skip,ref string error_message)
        {
            dynamic output = null;
            using (var db = new DataContext()){
                try
                {
                    var i = 1;
                    System.Globalization.CultureInfo th_format = new System.Globalization.CultureInfo("th-Th");
                    var data = (from a in db.course_evaluation
                                join b1 in db.course_evaluation_result on new { a.id } equals new { id = b1.course_evaluation_id } into b2
                                from b in b2.DefaultIfEmpty()
                                  join f1 in db.course_evaluation_choices on new { a.id } equals new { id = f1.course_evaluation_id } into f2
                                from f in f2.DefaultIfEmpty()
                                join c1 in db.course on new { a.course_id } equals new { course_id = c1.id } into c2 from c in c2.DefaultIfEmpty()

                              
                                select new
                                {
                                    a.question,
                                    a.course_id,
                                    b.course_evaluation_id,
                                   course_evaluation_choices_id= f.id,
                                    c.name,
                                    f.score,
                                   

                                }).ToList()
                                .Select(x => {

                                    decimal? total_score = db.course_evaluation_choices.Where(a => a.course_evaluation_id == x.course_evaluation_id&&a.id==x.course_evaluation_choices_id).Sum(a => a.score);
                                    decimal? total_eval = db.course_evaluation_result.Where(a => a.course_evaluation_id == x.course_evaluation_id && a.course_id == x.course_id).ToList().Count();
                                    decimal? total_question = db.course_evaluation.Where(a => a.course_id == x.course_id).ToList().Count();
                                    return new
                                    {
                                        x.question,
                                        x.course_id,
                                        x.course_evaluation_id,
                                        course_name = x.name,
                                       result=(x.score*total_score) / total_score

                                    };
                                });

                    if (search_text != null)
                    {
                        data = data.Where(x => x.course_name == search_text).ToList();
                    }
                    data = data.ToList().Skip(skip).Take(take);

                    output = new
                    {
                        data,
                        page_total = data.Count(),
                        
                    };
                }
                catch (Exception ex)
                {
                    error_message= ex.Message;
                }
            }
            return output;
        }    public dynamic StudentReport(string search_text, string course_id,int take,int skip,ref string error_message)
        {
            dynamic output = null;
            using (var db = new DataContext()){
                try
                {
                    var i = 1;
                    System.Globalization.CultureInfo th_format = new System.Globalization.CultureInfo("th-Th");
                    var data = (from a in db.student
                                join b1 in db.student_course_info on new { a.id } equals new { id = b1.student_id } into b2
                                from b in b2.DefaultIfEmpty()
                                join c1 in db.course on new { b.course_id } equals new { course_id = c1.id } into c2
                                from c in c2.DefaultIfEmpty()
                                join e1 in db.provinces on new { a.province_id } equals new { province_id = e1.id } into e2
                                from e in e2.DefaultIfEmpty()
                                join d1 in db.course_category on new { id=c.course_category_id } equals new { d1.id } into d2 from d in d2.DefaultIfEmpty()
                                where b.course_id==course_id
                                select new
                                {
                                    a.province_id,
                                    e.province_name,
                                    e.province_code,
                                    e.province_name_eng,
                                    a.id,
                                    b.course_id,
                                    c.name,
                                    c.course_category_id,
                                    couse_category_name=  d.name
                                }).GroupBy(g => g.province_id, (k, v) => new
                                {
                                    provice_id = v.Select(x => x.province_id).FirstOrDefault(),
                                    course_id = v.Select(x => x.course_id).FirstOrDefault(),
                                    course_name = v.Select(x => x.course_id).FirstOrDefault(),
                                    province_code = v.Select(x => x.province_code).FirstOrDefault(),
                                    province_name = v.Select(x => x.province_name).FirstOrDefault(),
                                    province_name_eng = v.Select(x => x.province_name_eng).FirstOrDefault(),
                                    couse_category_id = v.Select(x => x.course_category_id).FirstOrDefault(),
                                    couse_category_name = v.Select(x => x.couse_category_name).FirstOrDefault(),
                                    count_provice = v.Count()

                                }).ToList().Select(x =>
                                {
                                    decimal? province_result = db.student.Where(a => a.province_id == x.provice_id).ToList().Count();
                                    decimal? total = db.student.Count();
                                    return new
                                    {
                                        x.provice_id,
                                        x.province_code,
                                        x.province_name,
                                        x.province_name_eng,
                                        x.course_id,
                                        x.course_name,
                                        x.count_provice,
                                        x.couse_category_id,
                                        x.couse_category_name,
                                        province_percent = province_result - x.count_provice / total
                                    };





                                });


                    if (search_text != null)
                    {
                        data = data.Where(x => x.couse_category_name == search_text).ToList();
                    }

                    data = data.ToList().Skip(skip).Take(take);

                    output = new
                    {
                        data,
                        page_total = data.Count(),
                        
                    };
                }
                catch (Exception ex)
                {
                    error_message= ex.Message;
                }
            }
            return output;
        } public dynamic StudentExamPressReport(int take,int skip,ref string error_message)
        {
            dynamic output = null;
            using (var db = new DataContext()){
                try
                {
                    var i = 1;
                    System.Globalization.CultureInfo th_format = new System.Globalization.CultureInfo("th-Th");
                    var exam_press = (from a in db.student

                                join b1 in db.student_course_info on new { a.id } equals new { id = b1.student_id } into b2
                                from b in b2.DefaultIfEmpty()
                                join c1 in db.course on new { b.course_id } equals new { course_id = c1.id } into c2
                                from c in c2.DefaultIfEmpty()
                                join e1 in db.course_exam on new { b.course_id } equals new { e1.course_id } into e2
                                from e in e2.DefaultIfEmpty()
                                join f1 in db.course_exam_answer on new { course_exam_id = e.id } equals new { f1.course_exam_id } into f2
                                from f in f2.DefaultIfEmpty()
                                join z1 in db.course_exam_logging on new { b.course_id, b.student_id } equals new { z1.course_id, z1.student_id } into z2
                                from z in z2.DefaultIfEmpty()
                                select new
                                {
                                    b.student_id,
                                    b.course_id,
                                    course_name = c.name,
                                    course_exam_id = e.id,
                                    e.question,
                                    z.score,
                                    course_exam_answer_id = f.id,
                                    f.correct
                                }).ToList().Select(x =>
                                {
                                    int? correct_exam = db.course_exam_answer.Where(a => a.course_exam_id == x.course_exam_id && a.correct == 1).Select(a => a.id).FirstOrDefault();
                                    var exam_score_press = db.course_exam_logging.Where(a =>a.student_id==x.student_id&& a.course_id == x.course_id && (a.course_exam_answer_id == correct_exam && a.course_exam_id == x.course_exam_id)).Sum(a=>a.score);
                                    var exam_total_score = db.course_exam_logging.Where(a => a.course_id == x.course_id /*&& a.course_exam_answer_id == correct_exam*/).Sum(a => a.score);
                                    return new
                                    {

                                        x.student_id,
                                        x.course_id,
                                        x.course_name,
                                        x.course_exam_id,
                                        x.question,
                                        exam_score_press_result = exam_score_press * 100 / exam_total_score
                                    };
                                }).ToList();
                    var course_row = db.course.ToList().Count();
                    var data = (from a in db.course



                                   select new
                                   {
                                       course_id = a.id,
                                       course_name=a.name
                                   }).ToList().Select(x => {
                                    
                                       var total_press = exam_press.Where(a => a.exam_score_press_result > 80 && a.course_id == x.course_id).ToList().Count();
                                        
                                       return new
                                       {

                                           x.course_id,
                                           x.course_name,
                                          exam_percent =(course_row*total_press)/total_press
                                       };
                                   });
                    data = data.ToList().Skip(skip).Take(take);

                    output = new
                    {
                        data,
                        page_total = data.Count(),
                        
                    };
                }
                catch (Exception ex)
                {
                    error_message= ex.Message;
                }
            }
            return output;
        }
        public dynamic CertificateCategoryPrintReport(string search_text,int take,int skip,ref string error_message)
        {
            dynamic output = null;
            using (var db = new DataContext()){
                try
                {
                    var i = 1;
                    System.Globalization.CultureInfo th_format = new System.Globalization.CultureInfo("th-Th");
                    var data = (from a in db.certificate_print
                                join b1 in db.certificate_type on new { certificate_type_id = a.certificate_type } equals new { b1.certificate_type_id } into b2
                                from b in b2.DefaultIfEmpty()
                                join f1 in db.course on new {  a.course_id } equals new { course_id = f1.id } into f2 from f in f2.DefaultIfEmpty()
                                
                                join c1 in db.course_category on new {  f.course_category_id } equals new { course_category_id = c1.id } into c2
                                from c in c2.DefaultIfEmpty()
                                join d1 in db.student on new { a.student_id } equals new { student_id = d1.id } into d2
                                from d in d2.DefaultIfEmpty()
                                select new
                                {
                                    a.certificate_id,
                                    certificate_type_name = b.certificate_type_desc,
                                    catagory_name = c.name,
                                    a.student_id,

                                }).ToList().Select(x => new
                                {
                                    x.certificate_id,
                                    x.certificate_type_name,
                                    x.catagory_name,
                                    x.student_id,
                                    print_total = db.certificate_print.Where(a=>a.certificate_id==x.certificate_id&&a.student_id==x.student_id).ToList().Count()
                                });
                                if (search_text != null)
                    {
                        data = data.Where(a => a.catagory_name == search_text).ToList();
                    }
                    data = data.ToList().Skip(skip).Take(take);

                    output = new
                    {
                        data,
                        page_total = data.Count(),
                        
                    };
                }
                catch (Exception ex)
                {
                    error_message= ex.Message;
                }
            }
            return output;
        }    public dynamic CertificateCoursePrintReport(string search_text,int take,int skip,ref string error_message)
        {
            dynamic output = null;
            using (var db = new DataContext()){
                try
                {
                    var i = 1;
                    System.Globalization.CultureInfo th_format = new System.Globalization.CultureInfo("th-Th");
                    var data = (from a in db.certificate_print
                                join b1 in db.certificate_type on new { certificate_type_id = a.certificate_type } equals new { b1.certificate_type_id } into b2
                                from b in b2.DefaultIfEmpty()
                                join c1 in db.course on new { course_id = a.course_id } equals new { course_id = c1.id } into c2
                                from c in c2.DefaultIfEmpty()
                                join d1 in db.student on new { a.student_id } equals new { student_id = d1.id } into d2
                                from d in d2.DefaultIfEmpty()
                                select new
                                {
                                    a.certificate_id,
                                    certificate_type_name = b.certificate_type_desc,
                                    course_name = c.name,
                                    a.student_id,

                                }).ToList().Select(x => new
                                {
                                    x.certificate_id,
                                    x.certificate_type_name,
                                    x.course_name,
                                    x.student_id,
                                    print_total = db.certificate_print.Where(a=>a.certificate_id==x.certificate_id&&a.student_id==x.student_id).ToList().Count()
                                });
                                if (search_text != null)
                    {
                        data = data.Where(a => a.course_name == search_text).ToList();
                    }
                    data = data.ToList().Skip(skip).Take(take);

                    output = new
                    {
                        data,
                        page_total = data.Count(),
                        
                    };
                }
                catch (Exception ex)
                {
                    error_message= ex.Message;
                }
            }
            return output;
        }
        public dynamic DriveSpecReport(ref string errorMsg)
        {
            double freeSpec = 0;
            double useSpec = 0;
            double totalSpec = 0;
            double freeSpec_per = 0;
            double totalSpec_per = 0;
            double useSpec_per = 0;
            double cal_mb= 1048576;
            double cal_kb= 1024;
            double cal_gb = 1073741824;
            dynamic output = null;
            var totalSpec_txt ="";
            var freeSpec_txt = "";
            var useSpec_txt = "";
            var total_per_txt ="";
            var free_per_txt = "";
            var use_per_txt = "";
            try
            {
                dynamic driveSpec = null;
                DriveInfo driveInfo = new DriveInfo("D");
                if (driveInfo.IsReady)
                {
                    freeSpec_per = (driveInfo.AvailableFreeSpace / (double)driveInfo.TotalSize) * 100;
                    totalSpec_per = ((float)driveInfo.TotalSize / 100)*100;
                    useSpec_per = (((float)driveInfo.TotalSize - driveInfo.AvailableFreeSpace)/(float)driveInfo.TotalSize)* 100;
                    totalSpec = (driveInfo.TotalSize/cal_gb);
                    freeSpec = driveInfo.AvailableFreeSpace / cal_gb ;
                    free_per_txt = $"{freeSpec_per.ToString("0.##")} %";
                     use_per_txt = $"{useSpec_per.ToString("0.##")} %";
                    totalSpec_per = freeSpec_per + useSpec_per;
                    total_per_txt = $"{totalSpec_per.ToString("0.##")} %";
                    totalSpec_txt = $"{totalSpec.ToString("0.##")} GB";
                     freeSpec_txt = $"{freeSpec.ToString("0.##")} GB";
                    if (Math.Round(totalSpec) < 0)
                    {
                        totalSpec = driveInfo.TotalSize / cal_mb;
                         totalSpec_txt = $"{totalSpec.ToString("0.##")} MB";

                    }
                    if (Math.Round(freeSpec) < 0)
                    {
                        freeSpec = driveInfo.AvailableFreeSpace / cal_mb;
                        freeSpec_txt = $"{freeSpec.ToString("0.##")} MB";

                    }
                    useSpec = (driveInfo.TotalSize - driveInfo.AvailableFreeSpace)/cal_gb;
                    useSpec_txt = $"{useSpec.ToString("0.##")} GB";

                    if (Math.Round(useSpec) < 0)
                    {
                        useSpec = (driveInfo.TotalSize - driveInfo.AvailableFreeSpace) / cal_mb;
                        useSpec_txt = $"{useSpec.ToString("0.##")} MB";

                    }
                }


                var driveData = new
                {
                    freeSpec,
                    useSpec,
                    totalSpec,
                    totalSpec_txt,
                    freeSpec_txt,
                    useSpec_txt,
                    free_per_txt,
                 use_per_txt,
                    total_per_txt
                };
                var pic_count = 0;
                 var doc_count = 0;
                 var video_count = 0;
                double img_size = 0;
                double doc_size = 0;
                double video_size = 0;
                double total_size = 0;

                dynamic test = null;
                foreach (var x in pic_mapping)
                {
                  var  file_path = Directory.GetFiles(virtual_dir, $"*.{x}");
                    for(var i = 0; i < file_path.Length; i++)
                    {
                        FileInfo fileInfo = new FileInfo(file_path[i]);
                        img_size += fileInfo.Length;
                        total_size += fileInfo.Length;

                    }
                    DirectoryInfo info = new DirectoryInfo(virtual_dir);
                    pic_count += Directory.GetFiles(virtual_dir, $"*.{x}").Length;
                } foreach (var x in doc_mapping)
                {
                    var file_path = Directory.GetFiles(virtual_dir, $"*.{x}");
                    for (var i = 0; i < file_path.Length; i++)
                    {
                        FileInfo fileInfo = new FileInfo(file_path[i]);
                        doc_size += fileInfo.Length;
                        total_size += fileInfo.Length;

                    }
                    doc_count += Directory.GetFiles(virtual_dir, $"*.{x}").Length;
                } foreach (var x in video_mapping)
                {
                    var file_path = Directory.GetFiles(virtual_dir, $"*.{x}");
                    for (var i = 0; i < file_path.Length; i++)
                    {
                        FileInfo fileInfo = new FileInfo(file_path[i]);
                        video_size += fileInfo.Length ;
                        total_size += fileInfo.Length;
                    }
                    video_count += Directory.GetFiles(virtual_dir, $"*.{x}").Length;
                }
                var result_count = pic_count + doc_count + video_count;
                var total_count = Directory.GetFiles(virtual_dir).Length;
                var other_count = total_count - result_count;
                var result_size = img_size + doc_size + video_size;
                var total_path = Directory.GetFiles(virtual_dir);
                for (var xx = 0; xx < total_path.Length; xx++) {
                   
                        FileInfo fileInfo = new FileInfo(total_path[xx]);
                        total_size += fileInfo.Length ;

                    
                }
                var img_size_txt = "";
                var doc_size_txt = "";
                var video_size_txt = "";
                var other_size_txt = "";
                var total_size_txt = "";
                var other_size = total_size - result_size;
                var img_gb = img_size / cal_gb;
                var img_mb= img_size / cal_mb;
                var img_kb= img_size / cal_kb;
                var doc_gb = doc_size / cal_gb;
                var doc_mb = doc_size / cal_mb;
                var doc_kb = doc_size / cal_kb;
                double pic_per = 0;
                var pic_per_txt="";
                double doc_per = 0;
                var doc_per_txt="";
                double video_per = 0;
                var video_per_txt = "";
                    double other_per = 0;
                var other_per_txt = "";

                if (Math.Round(img_gb) > 0) { img_size_txt = $"{(img_size/cal_gb).ToString("0.##")} GB"; }
               else if (Math.Round(img_mb)>0) { img_size_txt = $"{(img_size/cal_mb).ToString("0.##")} MB"; }
                else { img_size_txt = $"{(img_size/cal_kb).ToString("0.##")} KB"; }
                if (Math.Round(doc_gb)>0) { doc_size_txt = $"{(doc_size/cal_gb).ToString("0.##")} GB"; }
                else if (Math.Round(doc_mb) >0) { doc_size_txt = $"{(doc_size/cal_mb).ToString("0.##")} MB"; }
                else  { doc_size_txt = $"{(doc_size/cal_kb).ToString("0.##")} KB"; }
                if (Math.Round(video_size/cal_gb)>0) { video_size_txt = $"{(video_size / cal_gb).ToString("0.##")} GB"; }
                else if (Math.Round(video_size / cal_mb)>0) { video_size_txt = $"{(video_size / cal_mb).ToString("0.##")} MB"; }
                else { video_size_txt = $"{(video_size / cal_kb).ToString("0.##")} KB"; }
                if (Math.Round(other_size/cal_gb)>0) {
                    other_size_txt = $"{(other_size/cal_gb).ToString("0.##")} GB"; }
                else if (Math.Round(other_size / cal_mb)>0) {
                    other_size_txt = $"{(other_size/cal_mb).ToString("0.##")} MB"; }
                else  {
                    other_size_txt = $"{(other_size/cal_kb).ToString("0.##")} KB"; }
                   if (Math.Round(total_size/cal_gb)>0) {
                    total_size_txt = $"{(total_size/cal_gb).ToString("0.##")} GB"; }
                else if (Math.Round(total_size / cal_mb) >0) {
                    total_size_txt = $"{(total_size/cal_mb).ToString("0.##")} MB"; }
                else  {
                    total_size_txt = $"{(total_size/cal_kb).ToString("0.##")} KB"; }
                pic_per = (img_size / total_size) * 100;
                pic_per_txt = $"{pic_per.ToString("0.##")} %";
                doc_per = (doc_size / total_size) * 100;
                doc_per_txt = $"{doc_per.ToString("0.##")} %";
                video_per = (video_size / total_size) * 100;
                video_per_txt = $"{video_per.ToString("0.##")} %";
                other_per = (other_size / total_size) * 100;
                other_per_txt = $"{other_per.ToString("0.##")} %";
            
                var fileCount = new
                {
                    pic_count,
                    doc_count,
                    video_count,
                    other_count,
                    total_count,
                    fileSize = new
                    {
                        pic_size = img_size_txt,
                        doc_size=doc_size_txt,
                        video_size = video_size_txt,
                        other_size = other_size_txt,
                        total_size =total_size_txt,
                        pic_per_txt,
                        doc_per_txt,
                        video_per_txt,
                        other_per_txt
            }

                };
                output = new { driveData ,fileCount };
            }catch(Exception ex)
            {
                errorMsg = ex.Message;
            }
            return output;
        }
        public dynamic ViewCountVideoReport(string search_text,int take,int skip,ref string errorMsg)
        {
            dynamic output = null;
            using (var db = new DataContext())
            {
                try {
                    var data = (from a in db.video_on_demand
                                join b1 in db.course_category on new { a.course_category_id } equals new { course_category_id = b1.id } into b2 from b in b2.DefaultIfEmpty()
                                select new
                                {
                                    a.count_view,
                                    a.name,
                                    a.course_category_id,
                                    course_category_name = b.name
                                }).ToList().Select(x => new
                                {
                                    x.course_category_id,
                                    x.course_category_name,
                                    x.count_view,
                                    video_name = x.name
                                });
                    if (search_text != null)
                    {
                        data = data.Where(x => x.course_category_name == search_text);
                    }
                    data = data.ToList().OrderByDescending(x => x.count_view).Skip(skip).Take(take);
                    output = new
                    {
                        data
                    };
                } catch(Exception ex)
                {
                    errorMsg = ex.Message;
                }
                return output;
            }
        }
    }
}