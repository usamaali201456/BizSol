namespace BizSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mobile
    {
        public Mobile()
        {
            AdPosts = new HashSet<AdPost>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MobilesId { get; set; }

        public string BrandName { get; set; }
        [ForeignKey("Category")]
        public int? fkCatId { get; set; }

        public virtual ICollection<AdPost> AdPosts { get; set; }

        public virtual Category Category { get; set; }
    }
}
