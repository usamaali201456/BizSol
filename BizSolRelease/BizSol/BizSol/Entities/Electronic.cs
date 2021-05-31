namespace BizSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("electronic")]
    public class Electronic
    {
        public Electronic()
        {
            AdPosts = new HashSet<AdPost>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ElectronicId { get; set; }
        public int ModelYear { get; set; }
        public string BrandName { get; set; }
        [ForeignKey("Category")]
        public int? FkCatID { get; set; }

        public virtual ICollection<AdPost> AdPosts { get; set; }

        public virtual Category Category { get; set; }
    }
}
