namespace BizSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bike")]
    public class Bike
    {
        public Bike()
        {
            AdPosts = new HashSet<AdPost>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BikeID { get; set; }

        public int BikeMileage { get; set; }
        public string BrandName { get; set; }
        public string IsFileAvailable { get; set; }

        public int BikeYear { get; set; }
        [ForeignKey("Category")]
        public int? fk_Cat_Id { get; set; }

        public virtual ICollection<AdPost> AdPosts { get; set; }

        public virtual Category Category { get; set; }
    }
}
