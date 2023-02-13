using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("video_on_demand")]
    public class video_on_demand //Create 12/1/2020 1:41:11 PM
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id { get; set; }

        public int? course_category_id { get; set; }

        [MaxLength(510)]
        public string name { get; set; }

        [MaxLength(1000)]
        public string video { get; set; }

        public int? count_view { get; set; }

        [MaxLength(500)]
        public string description { get; set; }

        [MaxLength(510)]
        public string producer_name { get; set; }

        [MaxLength(510)]
        public string phone { get; set; }

        [MaxLength(510)]
        public string email { get; set; }

        [MaxLength(1000)]
        public string attachment { get; set; }

        public int? is_deleted { get; set; }

        public int? created_by { get; set; }

        public DateTime? created_at { get; set; }

        public int? update_by { get; set; }

        public DateTime? update_at { get; set; }

        public string cover_thumbnail { get; set; }

    }
}