
using Langchips.Data;
using Langchips.Helpers;
using Langchips.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Xml.Linq;

namespace Langchips.ConsoleApplication
{
    internal class Program
    {
        //TODO: keep user logged in 
        private static User CurrentUser;

        static void Main(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<AppDbContext>(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                return new AppDbContext(optionsBuilder.Options, configuration);
            });

            var serviceProvider = services.BuildServiceProvider();

            var db = serviceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();

            try
            {
                if (db.Database.CanConnect())
                {
                    Console.WriteLine("✅ Database connection successful!");
                }
                else
                {
                    Console.WriteLine("❌ Database connection failed!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }
        static void Login(AppDbContext db)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = ReadPassword();

            var user = db.Users.SingleOrDefault(u => u.Username == username);

            if (user != null && user.VerifyPassword(password))
            {
                CurrentUser = user;
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }
        static string ReadPassword()
        {
            string password = string.Empty;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                password += key.KeyChar;
            }
            Console.WriteLine();
            return password;
        }  
    }
}
