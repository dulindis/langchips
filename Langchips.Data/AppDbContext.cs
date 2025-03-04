using Langchips.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Data
{
    public class AppDbContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=language_app";

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(connectionString);
        }

        public string GetDataDirectory()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SHOW data_directory;", connection))
                {
                    var result = command.ExecuteScalar();
                    return result.ToString();
                }
            }
        }
    }
}
