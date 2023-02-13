using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("student_course_lesson_progress")]
    public class student_course_lesson_progress
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public int course_id { get; set; }
        public int course_lesson_id { get; set; }
        public int is_done_lesson { get; set; }
        public int is_done_exercise { get; set; }
    }
}