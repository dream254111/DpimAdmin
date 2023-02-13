using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DpimProject.Models.Data.DataModels
{
    [Table("course_category")]
    public class course_category
    {
            [Key]
            [Column(Order = 0)]
            //[DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int? id { get; set; }

            [MaxLength(255)]
            public string name { get; set; }

            [MaxLength(255)]
            public string color { get; set; }
    
        
    }
}