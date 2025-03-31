using FastFoodOperator.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCombo> OrderCombos { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Product> Products { get; set; }    // TEMPORARY
        public DbSet<ProductVariant> ProductVariants { get; set; }

        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderCombo>()
                .HasKey(oc => new { oc.OrderId, oc.ComboId });

            modelBuilder.Entity<OrderCombo>()
                .HasOne(oc => oc.Order)
                .WithMany(o => o.OrderCombos)
                .HasForeignKey(oc => oc.OrderId);



            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ItemId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);



            modelBuilder.Entity<ComboProduct>()
                .HasKey(ci => new { ci.ComboId, ci.ProductId });

            modelBuilder.Entity<ComboProduct>()
                .HasOne(ci => ci.Combo)
                .WithMany(c => c.ComboProducts)
                .HasForeignKey(ci => ci.ComboId);

            modelBuilder.Entity<ComboProduct>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.ComboProducts)
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<ComboProduct>()
                .HasOne(ci => ci.ProductVariant)
                .WithMany()
                .HasForeignKey(ci => ci.ProductVariantId);



            modelBuilder.Entity<Product>()
                .HasMany(p => p.Combos)
                .WithMany(c => c.Products)
                .UsingEntity<ComboProduct>(
                    j => j
                        .HasOne(ci => ci.Combo)
                        .WithMany(c => c.ComboProducts)
                        .HasForeignKey(ci => ci.ComboId),
                    j => j
                        .HasOne(ci => ci.Product)
                        .WithMany()
                        .HasForeignKey(ci => ci.ProductId)
                );
        }

        // TEMPORARY
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public ICollection<ComboProduct> ComboProducts { get; set; } = [];
            public ICollection<Combo> Combos { get; set; } = [];
        }
	}   
}
