namespace Mvc3Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Image { get; set; }

        public decimal MakePrice { get; set; }

        public decimal NewPrice { get; set; }

        public DateTime GetDate { get; set; }

        public bool Enable { get; set; }

        public Guid ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}
