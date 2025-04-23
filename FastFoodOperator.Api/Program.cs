using Microsoft.EntityFrameworkCore;
using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.Services;
using FastFoodOperator.Api.Interfaces;
using System.Text.Json.Serialization;


namespace FastFoodOperator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Add services to the container.
            var builder = WebApplication.CreateBuilder(args);


			// Development
			if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<ProductService>();

            builder.Services.AddScoped<IOrderService, OrderService>();


            // Configure middlewares
            var app = builder.Build();

            // Development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.MapControllers();

            app.MapControllers();

            app.MapControllers();

            app.Run();
        }
    }
}