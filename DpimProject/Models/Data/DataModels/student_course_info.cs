using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("student_course_info")]
    public class student_course_info //Create 11/21/2020 3:48:15 AM
    {
        [Key]
        [Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? id { get; set; }

        public int? student_id { get; set; }

        public string course_id { get; set; }

        public int? course_lesson_id { get; set; }

        public int? cert_print_count { get; set; }

        public int? is_extend_study_time { get; set; }

        public DateTime? learning_startdate { get; set; }

        public DateTime? learning_enddate { get; set; }

        public int? is_deleted { get; set; }

        public int? created_by { get; set; }

        public DateTime? created_at { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_at { get; set; }

    }
}