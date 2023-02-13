using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("course_exam_answer")]
    public class course_exam_answer //Create 12/2/2020 10:26:27 PM
    {
        [Key]
        [Column(Order = 0)]
        public int? id { get; set; }

        public int? course_exam_id { get; set; }

        public int? course_id { get; set; }

        public int? correct { get; set; }

        public int? order { get; set; }
        
        public string answer { get; set; }
    }
}