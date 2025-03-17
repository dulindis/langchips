using Langchips.Blazor.Components;
using Langchips.Data;
using Langchips.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Langchips.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddScoped<ConfigurationService>();

            builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {

                var configurationService = serviceProvider.GetRequiredService<ConfigurationService>();
                var connectionString = configurationService.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Database connection string is missing.");
                }

                options.UseNpgsql(connectionString);
            }
            );

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
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
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
