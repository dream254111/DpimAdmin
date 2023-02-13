using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DpimProject.Models.Data.DataModels
{
    [Table("user_manage_profile")]
    public class user_manage_profile //Create 3/15/2021 11:17:25 PM
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? user_id { get; set; }

        public int? is_edit_address { get; set; }

        public int? is_edit_business { get; set; }

        public int? is_edit_business_document { get; set; }

        public int? is_edit_career { get; set; }

        public int? is_edit_educational { get; set; }

        public int? is_edit_email { get; set; }

        public int? is_edit_front_back_document { get; set; }

        public int? is_edit_front_id_document { get; set; }

        public int? is_edit_know_channal { get; set; }

        public int? is_edit_personal_info { get; set; }

        public int? is_edit_phone { get; set; }

        public int? is_edit_selfie_document { get; set; }

        public int? create_by { get; set; }

        public DateTime? create_dt { get; set; }
        public int? update_by { get; set; }

        public DateTime? update_dt { get; set; }

    }
}