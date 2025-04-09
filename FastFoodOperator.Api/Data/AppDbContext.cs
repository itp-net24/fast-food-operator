using FastFoodOperator.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOperator.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCombo> OrderCombos { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<ProductVariant> ProductVariants { get; set; }

        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            // Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            
           // Combo 
            modelBuilder.Entity<Combo>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Combo>()
                .Property(c => c.BasePrice)
                .HasColumnType("decimal(10, 2)");
            
            
            // Combo Product
            modelBuilder.Entity<ComboProduct>()
                .HasKey(cp => new { cp.ComboId, cp.ProductId });
           
            modelBuilder.Entity<ComboProduct>()
                .HasOne(cp => cp.Combo)
                .WithMany(c => c.ComboProducts)
                .HasForeignKey(cp => cp.ComboId);
           
            modelBuilder.Entity<ComboProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.ComboProducts)
                .HasForeignKey(cp => cp.ProductId);
           
            modelBuilder.Entity<ComboProduct>()
                .HasOne(cp => cp.ProductVariant)
                .WithMany()
                .HasForeignKey(cp => cp.ProductVariantId);
            
            
            // Ingredient
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();
            
           modelBuilder.Entity<Ingredient>()
                .Property(i => i.PriceModifier)
                .HasColumnType("decimal(10, 2)");
           
           
           // Order Combo
            modelBuilder.Entity<OrderCombo>()
                .HasKey(oc => new { oc.OrderId, oc.ComboId });

            modelBuilder.Entity<OrderCombo>()
                .HasOne(oc => oc.Order)
                .WithMany(o => o.OrderCombos)
                .HasForeignKey(oc => oc.OrderId);

            modelBuilder.Entity<OrderCombo>()
                .HasOne(oc => oc.Combo)
                .WithMany()
                .HasForeignKey(oc => oc.ComboId);

			      modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId);            

            
            // Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(1000);
            
            modelBuilder.Entity<Product>()
                .Property(p => p.BasePrice)
                .HasColumnType("decimal(10, 2)");
            
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            // Product Ingredient
            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);
            
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);
           
            
            // ProductVariant
            modelBuilder.Entity<ProductVariant>()
                .HasOne(pv => pv.Product)
                .WithMany()
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<ProductVariant>()
                .Property(pv => pv.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            modelBuilder.Entity<ProductVariant>()
                .Property(pv => pv.Description)
                .HasMaxLength(1000);
            
            modelBuilder.Entity<ProductVariant>()
                .Property(pv => pv.PriceModifier)
                .HasColumnType("decimal(10, 2)");
            
            DatabaseSeeder.Seed(modelBuilder);
        }
	}   
}
