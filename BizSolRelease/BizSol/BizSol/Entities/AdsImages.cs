using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BizSol.Entities
{
    [Table("ads_images")]
    public class AdsImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("AdPost")]
        public int? AdId { get; set; }
        public virtual AdPost AdPost { get; set; }
    }
}