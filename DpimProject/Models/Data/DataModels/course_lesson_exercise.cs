using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("course_lesson_exercise")]
    public class course_lesson_exercise //Create 11/21/2020 3:44:21 AM
    {
        [Key]
        [Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? id { get; set; }

        public int? course_id { get; set; }

        public int? order { get; set; }

        public int? course_lesson_id { get; set; }
        
        public string question { get; set; }
        
        public string image { get; set; }
        
        public string video { get; set; }

        public string p_480 { get; set; }

        public string p_720 { get; set; }

        public string p_1080 { get; set; }

        public string p_original { get; set; }

        public string cover_video { get; set; }

        public int? is_answer_match { get; set; }

        public int? is_answer_choice { get; set; }

        public int? is_deleted { get; set; }

        public int? created_by { get; set; }

        public DateTime? created_at { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_at { get; set; }

    }

    public class model_lesson_exercise
    {
        public int? id { get; set; }
        public int? course_id { get; set; }
        public int? order { get; set; }
        public int? course_lesson_id { get; set; }
        public string question { get; set; }
        public string image { get; set; }
        public video video { get; set; }
        public int? is_answer_match { get; set; }
        public int? is_answer_choice { get; set; }
        public List<course_lesson_exercise_answer_choices> choices { get; set; }
        public List<course_lesson_exercise_answer_match> match { get; set; }
    }
}