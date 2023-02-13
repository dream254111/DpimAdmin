using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("course_evaluation_result")]
    public class course_evaluation_result //Create 11/21/2020 3:50:14 AM
    {
        [Key]
        [Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? id { get; set; }

        public int? course_evaluation_id { get; set; }

        public int? student_id { get; set; }

        public string course_id { get; set; }

        public int? course_evaluation_choices_id { get; set; }
        
        public string answer { get; set; }

        public int? is_deleted { get; set; }

        public int? created_by { get; set; }

        public DateTime? created_at { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_at { get; set; }

    }

    public class model_evaluation_result
    {
        public int course_evaluation_id { get; set; }
        public int course_evaluation_choices_id { get; set; }
        public string answer { get; set; }
    }
}