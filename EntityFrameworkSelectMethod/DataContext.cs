namespace EntityFrameworkSelectMethod
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<MessageInfo> MessageInfoes { get; set; }
        public virtual DbSet<ProductInfo> ProductInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageInfo>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.MessageInfoes)
                .WithRequired(e => e.ProductInfo)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
}
