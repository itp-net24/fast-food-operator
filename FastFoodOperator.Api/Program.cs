using FastFoodOperator.Api.Data.Models;
using FastFoodOperator.Api.Data.Migrations;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["DefaultConnection:ConnectionString"]);
            });



            // Configure middlewares
            var app = builder.Build();

            // Development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.Run();
        }
    }
}