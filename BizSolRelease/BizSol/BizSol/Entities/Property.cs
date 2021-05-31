namespace BizSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("property")]
    public class Property
    {
        public Property()
        {
            AdPosts = new HashSet<AdPost>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropertyId { get; set; }
        public string AreaType { get; set; }
        public int PropertyArea { get; set; }
        public int WashRoom { get; set; }
        public int BedRoom { get; set; }
        public string IsCarParking { get; set; }
        [ForeignKey("Category")]
        public int? FKCatId { get; set; }

        public virtual ICollection<AdPost> AdPosts { get; set; }

        public virtual Category Category { get; set; }
    }
}
