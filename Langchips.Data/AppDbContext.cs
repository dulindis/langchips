using Langchips.Models;
using Langchips.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Langchips.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<Translation> Translations { get; set; }

        private readonly string _connectionString;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration? configuration = null)
        : base(options)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Database connection string is missing.");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured && !string.IsNullOrEmpty(_connectionString))
            {
                options.UseNpgsql(_connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - Set relationship (One-to-Many)
            modelBuilder.Entity<Set>()
                .HasOne(s => s.User) // Set has one User (creator)
                .WithMany(u => u.CreatedSets) // User can have many created Sets
                .HasForeignKey(s => s.UserId) // Foreign key on Set entity
                .OnDelete(DeleteBehavior.Cascade); // When User is deleted, delete the Sets

            // Set - Term relationship (One-to-Many)
            modelBuilder.Entity<Term>()
                .HasOne(t => t.Set) // Term belongs to one Set
                .WithMany(s => s.Terms) // Set can have many Terms
                .HasForeignKey(t => t.SetId) // Foreign key on Term entity
                .OnDelete(DeleteBehavior.Cascade); // When Set is deleted, delete the Terms

            // Term - Translation relationship (One-to-Many)
            modelBuilder.Entity<Translation>()
                .HasOne(t => t.Term) // Translation belongs to one Term
                .WithMany(te => te.Translations) // Term can have many Translations
                .HasForeignKey(t => t.TermId) // Foreign key on Translation entity
                .OnDelete(DeleteBehavior.Cascade); // When Term is deleted, delete the Translations


            modelBuilder.Entity<Set>()
                .Property(s => s.TermLanguage)
                .IsRequired();

            modelBuilder.Entity<Set>()
                .Property(s => s.TranslationLanguage)
                .IsRequired();
        }        
    }
}
