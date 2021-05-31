namespace BizSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public class Category
    {
        public Category()
        {
            AdPosts = new HashSet<AdPost>();
            Bikes = new HashSet<Bike>();
            Clothes = new HashSet<Cloth>();
            Electronics = new HashSet<Electronic>();
            Mobiles = new HashSet<Mobile>();
            Properties = new HashSet<Property>();
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cat_Id { get; set; }

        public string cat_name { get; set; }


        public int? cat_Status { get; set; }


        public virtual ICollection<AdPost> AdPosts { get; set; }

        public virtual ICollection<Bike> Bikes { get; set; }

        public virtual ICollection<Cloth> Clothes { get; set; }

        public virtual ICollection<Electronic> Electronics { get; set; }


        public virtual ICollection<Mobile> Mobiles { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
