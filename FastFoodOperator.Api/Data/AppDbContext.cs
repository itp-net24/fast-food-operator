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
			modelBuilder.Entity<Combo>(combo =>
			{
				combo.HasKey(c => c.Id);

				combo.Property(c => c.Name)
					.HasMaxLength(100)
					.IsRequired();

				combo.Property(c => c.BasePrice)
					.HasColumnType("decimal(18,2)");

				combo.HasMany(c => c.ComboGroups)
					.WithMany(cg => cg.Combos)
					.UsingEntity(c => c.ToTable("ComboGroupCombo"));

				combo.HasMany(c => c.ComboProducts)
					.WithOne(cp => cp.Combo)
					.HasForeignKey(cp => cp.ComboId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			// ComboGroup
			modelBuilder.Entity<ComboGroup>(group =>
			{
				group.HasKey(g => g.Id);

				group.Property(g => g.Name)
					.IsRequired()
					.HasMaxLength(100);

				group.HasOne(g => g.DefaultComboProduct)
					.WithMany()
					.HasForeignKey(g => g.DefaultComboProductId)
					.OnDelete(DeleteBehavior.Restrict)
					.IsRequired(false);
			});

			// Combo Product
			modelBuilder.Entity<ComboProduct>(comboProduct =>
			{
				comboProduct.HasKey(cp => cp.Id);

				comboProduct.HasOne(cp => cp.Product)
					.WithMany(p => p.ComboProducts)
					.HasForeignKey(cp => cp.ProductId)
					.OnDelete(DeleteBehavior.Restrict);

				comboProduct.HasOne(cp => cp.DefaultProductVariant)
					.WithMany()
					.HasForeignKey(cp => cp.DefaultVariantId)
					.OnDelete(DeleteBehavior.Restrict)
					.IsRequired(false);

				comboProduct.HasOne(cp => cp.ComboGroup)
					.WithMany(cg => cg.ComboProducts)
					.HasForeignKey(cp => cp.ComboGroupId)
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired(false);

				comboProduct.HasOne(cp => cp.Combo)
					.WithMany(c => c.ComboProducts)
					.HasForeignKey(cp => cp.ComboId)
					.OnDelete(DeleteBehavior.Restrict)
					.IsRequired(false);
			});


			// Ingredient
			modelBuilder.Entity<Ingredient>()
				.Property(i => i.Name)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<Ingredient>()
				 .Property(i => i.PriceModifier)
				 .HasColumnType("decimal(10, 2)");


            //Order Combo
			modelBuilder.Entity<OrderCombo>()
                .HasKey(oc => oc.Id);

            modelBuilder.Entity<OrderCombo>()
                .HasOne(oc => oc.Order)
                .WithMany(o => o.OrderCombos)
                .HasForeignKey(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<OrderCombo>()
				.Property(oc => oc.FinalPrice)
				.HasColumnType("decimal(10, 2)");

			//Order Product
			modelBuilder.Entity<OrderProduct>()
                .HasKey(op => op.Id);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<OrderProduct>()
	            .Property(op => op.FinalPrice)
	            .HasColumnType("decimal(10, 2)");

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
				.Property(p => p.ImageUrl)
				.HasMaxLength(2048)
				.IsUnicode(false);

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
				.HasForeignKey(pi => pi.IngredientId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ProductIngredient>()
				.HasOne(pi => pi.Product)
				.WithMany(p => p.ProductIngredients)
				.HasForeignKey(pi => pi.ProductId)
				.OnDelete(DeleteBehavior.Cascade);


			// ProductVariant
			modelBuilder.Entity<ProductVariant>()
				.HasOne(pv => pv.Product)
				.WithMany(p => p.Variants)
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
