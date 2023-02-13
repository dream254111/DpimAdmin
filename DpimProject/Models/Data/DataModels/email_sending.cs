using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DpimProject.Models.Data.DataModels
{
    [Table("email_sending")]
    public class email_sending //Create 11/24/2020 4:14:36 PM
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? email_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? student_id { get; set; }

        [MaxLength(255)]
        public string from_email { get; set; }

        [MaxLength(255)]
        public string subject { get; set; }

        public string body_email { get; set; }

        public DateTime? send_dt { get; set; }

        public int? send_by { get; set; }

        [MaxLength(10)]
        public string email_type { get; set; }
        public string course_id { get; set; }
        public string token { get; set; }

    }
}