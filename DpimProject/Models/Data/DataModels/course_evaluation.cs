using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("course_evaluation")]
    public class course_evaluation //Create 11/21/2020 3:41:10 AM
    {
        [Key]
        [Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? id { get; set; }

        public string course_id { get; set; }

        public int? order { get; set; }
        
        public string question { get; set; }

        public int? is_free_fill_text { get; set; }

        public string free_fill_text { get; set; }

        public int? is_deleted { get; set; }

        public int? created_by { get; set; }

        public DateTime? created_at { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_at { get; set; }

    }
}