using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DpimProject.Models.Data.DataModels
{
    [Table("certificate_info")]
    public class certificate_info //Create 11/28/2020 7:34:50 AM
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(40)]
        public string certificate_id { get; set; }
        
        public int? student_id { get; set; }
        
        public int? course_id { get; set; }

        public DateTime? certificate_dt { get; set; }

        public int? create_by { get; set; }

        public DateTime? create_dt { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_dt { get; set; }

        public string path { get; set; }

        [MaxLength(1)]
        public string cert_status { get; set; }
    }
}