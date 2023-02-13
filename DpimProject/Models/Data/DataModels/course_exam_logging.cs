using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("course_exam_logging")]
    public class course_exam_logging //Create 11/21/2020 3:44:04 AM
    {
        [Key]
        [Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id { get; set; }

        public int? student_id { get; set; }

        public string course_id { get; set; }

        public int? is_pretest { get; set; }

        public int? score { get; set; }

        public int? is_deleted { get; set; }

        public int? created_by { get; set; }

        public DateTime? created_at { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_at { get; set; }

        public int? course_exam_answer_id { get; set; }

        public int? course_exam_id { get; set; }

    }

    public class model_exam_logging
    {
        public int course_exam_id { get; set; }
        public int answer { get; set; }
    }
}