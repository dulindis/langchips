
using Langchips.Data;
using Langchips.Models.Models;
using Langchips.Models;
using Langchips.Services;
using Microsoft.EntityFrameworkCore;
using Langchips.Models.Helpers;


namespace Langchips.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddScoped<ConfigurationService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazor", policy =>
                {
                    policy.AllowAnyOrigin()  // TODO:Replace with frontend URL for security/ specify your Blazor app's origin here
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {

                var configurationService = serviceProvider.GetRequiredService<ConfigurationService>();
                var connectionString = configurationService.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Database connection string is missing.");
                }

                options.UseNpgsql(connectionString);
            });


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
                try
                {
                    if (dbContext.Database.CanConnect())
                    {
                        Console.WriteLine("✅ Database connection successful!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Failed to connect to Database.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Database connection error: {ex.Message}");
                }

                DbSeeder.SeedData(dbContext);        
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowBlazor");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
