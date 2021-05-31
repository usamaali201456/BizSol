namespace BizSol.Entities
{
    using BizSol.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ad_post")]
    public class AdPost
    {
        public AdPost()
        {
            AdsImages = new HashSet<AdsImages>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

       
        public string Condition { get; set; }

       
        public string Detail { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        [ForeignKey("Category")]
        public int? cat_Id { get; set; }

        [ForeignKey("Vehicle")]
        public int? fk_vehicle_Id { get; set; }

        [ForeignKey("Bike")]
        public int? fkBikeid { get; set; }
        [ForeignKey("Mobile")]
        public int? fkMobileId { get; set; }
        [ForeignKey("Electronic")]
        public int? fkElectronicId { get; set; }
        [ForeignKey("Cloth")]
        public int? fkClothid { get; set; }
        [ForeignKey("Property")]
        public int? fkPropertyId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime WhenAdded { get; set; }
        [ForeignKey("City")]
        public int? FkCityId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Bike Bike { get; set; }

        public virtual Mobile Mobile { get; set; }

        public virtual Electronic Electronic { get; set; }

        public virtual Cloth Cloth { get; set; }

        public virtual Property Property { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<AdsImages> AdsImages { get; set; }

    }
}
