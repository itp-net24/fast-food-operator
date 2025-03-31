using FastFoodOperator.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCombo> OrderCombos { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ProductVariant> ProductVariants { get; set; }

        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }

        public DbSet<Product> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductIngredient> ItemIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

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

            modelBuilder.Entity<ComboProduct>()
             .HasKey(ci => new { ci.ComboId, ci.ProductId });


            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(i => i.Ingredient)
                .WithMany(pi => pi.ProductIngredients)
                .HasForeignKey(i => i.IngredientId);
            
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(p => p.Product)
                .WithMany(pi => pi.ProductIngredients)
                .HasForeignKey(p => p.ProductId);

        }
	}   
}
