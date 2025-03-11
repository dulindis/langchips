
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
    {//somehow keeping the contrxt cur logged in user 
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
           
            using (var db = serviceProvider.GetRequiredService<AppDbContext>())
            {
                db.Database.Migrate();

                Console.WriteLine("Welcome to Langchips. Please log in:");
                Login(db);
                if (CurrentUser != null)
                {
                    // Successfully logged in, print the user details (or do something else)
                    Console.WriteLine($"Welcome {CurrentUser.Name} {CurrentUser.Surname}!");
                    // You can now perform other actions related to the logged-in user
                }
                else
                {
                    Console.WriteLine("Login failed!");
                }

                //string name = "Ala2";
                //string surname = "Lala";
                //string email = "la.doe@example.com";
                //string pass = "securePass";
                //string username = "testuser4";

                //var user = new User(name, surname, email, pass, username);

                //db.Users.Add(user);
                //db.SaveChanges();

                //Console.WriteLine("User added successfully!");

                var dataDirectory = db.GetDataDirectory();
                Console.WriteLine($"PostgreSQL data directory: {dataDirectory}");
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
