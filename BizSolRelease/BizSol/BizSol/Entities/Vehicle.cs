namespace BizSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vehicle")]
    public class Vehicle
    {
        public Vehicle()
        {
            AdPosts = new HashSet<AdPost>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Vehicle_Id { get; set; }
        public string BrandName { get; set; }
        public string IsFileAvailable { get; set; }
        public int Vehicle_Milage { get; set; }
        [ForeignKey("Category")]
        public int? fk_cat_Id { get; set; }

        public int Vehicle_Year { get; set; }

        public virtual ICollection<AdPost> AdPosts { get; set; }

        public virtual Category Category { get; set; }
    }
}
