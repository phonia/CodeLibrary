namespace Mvc3Demo.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<TestTree> TestTree { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MakePrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.NewPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestTree>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<TestTree>()
                .HasMany(e => e.TestTree1)
                .WithOptional(e => e.TestTree2)
                .HasForeignKey(e => e.Parent);
        }
    }
}
