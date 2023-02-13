using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DpimProject.Models.Data.DataModels
{
    [Table("course_lesson")]
    public class course_lesson //Create 12/1/2020 1:30:13 PM
    {
        [Key]
        [Column(Order = 0)]
        public int? id { get; set; }

        public string course_id { get; set; }

        public int? instructor_id { get; set; }

        public int? order { get; set; }
        
        public string name { get; set; }
        
        public string main_video { get; set; }

        public string main_p_480 { get; set; }

        public string main_p_720 { get; set; }

        public string main_p_1080 { get; set; }

        public string main_p_original { get; set; }

        public string main_cover_video { get; set; }

        public int? count_view { get; set; }

        public int? is_interactive { get; set; }
        
        public string interactive_time { get; set; }
        
        public string interactive_video_1 { get; set; }

        public string interactive_1_p_480 { get; set; }

        public string interactive_1_p_720 { get; set; }

        public string interactive_1_p_1080 { get; set; }

        public string interactive_1_p_original { get; set; }

        public string interactive_1_cover_video { get; set; }
        
        public string interactive_video_2 { get; set; }

        public string interactive_2_p_480 { get; set; }

        public string interactive_2_p_720 { get; set; }

        public string interactive_2_p_1080 { get; set; }

        public string interactive_2_p_original { get; set; }

        public string interactive_2_cover_video { get; set; }

        public string description { get; set; }

        public int? lesson_time { get; set; }

        public int? is_deleted { get; set; }

        public int? created_by { get; set; }

        public DateTime? created_at { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_at { get; set; }
    }

    public class model_course_lesson
    {
        public int? id { get; set; }
        public int? course_id { get; set; }
        public int? order { get; set; }
        public string name { get; set; }
        public video main_video { get; set; }
        public int? lesson_time { get; set; }
        public bool is_interactive { get; set; }
        public string interactive_time { get; set; }
        public video interactive_video_1 { get; set; }
        public video interactive_video_2 { get; set; }
        public string description { get; set; }
        public int? instructor_id { get; set; }
    }
}