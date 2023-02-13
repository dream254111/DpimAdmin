using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DpimProject.Models.Data.DataModels
{
    [Table("certificate")]
    public class certificate //Create 10/23/2020 4:45:50 PM
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(20)]
        public string certificate_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [MaxLength(255)]
        public string user_id { get; set; }

        public int? print_time { get; set; }

        public DateTime? print_dt { get; set; }

        public int? download_time { get; set; }

        public DateTime? download_dt { get; set; }

    }
}