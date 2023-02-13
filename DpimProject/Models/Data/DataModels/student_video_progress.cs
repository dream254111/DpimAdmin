using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DpimProject.Models.Data.DataModels
{
    [Table("student_video_progress")]
    public class student_video_progress //Create 12/2/2020 10:21:02 AM
    {
        [Key]
        [Column(Order = 0)]
        public int? student_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int? course_id { get; set; }

        public int? course_lesson_id { get; set; }

        public string video_path { get; set; }

        public decimal? video_position { get; set; }

        public decimal? video_progress { get; set; }

        public int? create_by { get; set; }

        public DateTime? create_at { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_dt { get; set; }

    }
}