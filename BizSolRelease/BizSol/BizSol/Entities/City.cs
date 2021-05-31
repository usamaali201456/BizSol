namespace BizSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("city")]
    public class City
    {
        public City()
        {
            AdPosts = new HashSet<AdPost>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityID { get; set; }

        public string CityName { get; set; }

        public string CityImage { get; set; }

        public virtual ICollection<AdPost> AdPosts { get; set; }
    }
}
