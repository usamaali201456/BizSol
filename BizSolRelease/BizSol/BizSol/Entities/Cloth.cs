namespace BizSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cloth")]
    public class Cloth
    {
        public Cloth()
        {
            AdPosts = new HashSet<AdPost>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClothId { get; set; }

        public string ClothType { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        [ForeignKey("Category")]
        public int? FkCatId { get; set; }

        public virtual ICollection<AdPost> AdPosts { get; set; }

        public virtual Category Category { get; set; }
    }
}
