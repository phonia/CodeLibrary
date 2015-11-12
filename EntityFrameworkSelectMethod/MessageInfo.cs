namespace EntityFrameworkSelectMethod
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageInfo")]
    public partial class MessageInfo
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Message { get; set; }

        public Guid ProductId { get; set; }

        public virtual ProductInfo ProductInfo { get; set; }
    }
}
