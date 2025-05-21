using Microsoft.EntityFrameworkCore;
using FastFoodOperator.Api.Data;
using FastFoodOperator.Api.Hubs;
using FastFoodOperator.Api.Services;
using FastFoodOperator.Api.Interfaces;

namespace FastFoodOperator.Api;

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
				options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
			});

		// Cors service
		builder.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				policy.WithOrigins("https://localhost:5173") // Port for vue app
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
			});
		});

		builder.Services.AddSignalR();

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		});

		builder.Services.AddScoped<IOrderService, OrderService>();
		builder.Services.AddScoped<ProductService>();

		// Configure middlewares
		var app = builder.Build();

		// Development
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
			
		app.UseCors();

		app.UseHttpsRedirection();
            
		// Activating cors service
		app.UseCors("AllowVue");

		app.MapControllers();

		app.MapHub<OrderHub>("/test");

		app.Run();
	}
}