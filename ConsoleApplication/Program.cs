
using Langchips.Helpers;
using Langchips.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Xml.Linq;

namespace Langchips.ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (var db = new Langchips.Data.AppDbContext())
            {
                db.Database.Migrate();

                //string name = "John";
                //string surname = "Doe";
                //string email = "john.doe@example.com";
                //string pass = "securePass";
                //string username = "johnnyd";

                //var user = new User(name,surname,email,pass,username);

                //db.Users.Add(user);
                //db.SaveChanges();

                //Console.WriteLine("User added successfully!");

                var dataDirectory = db.GetDataDirectory();
                Console.WriteLine($"PostgreSQL data directory: {dataDirectory}");
            }
        }
    }
}
