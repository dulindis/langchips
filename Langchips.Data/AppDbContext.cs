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

            // If Language is an enum, you don't need to define a relationship here, 
            // you can just let it be stored as a simple integer in the database.
            modelBuilder.Entity<Set>()
                .Property(s => s.TermLanguage) // Store as an integer (enum value)
                .IsRequired();

            modelBuilder.Entity<Set>()
                .Property(s => s.TranslationLanguage) // Store as an integer (enum value)
                .IsRequired();
        }
        //public string GetDataDirectory()
        //{
        //    using var connection = new NpgsqlConnection(_connectionString); 
        //    connection.Open();
        //    using var command = new NpgsqlCommand("SHOW data_directory;", connection);
        //    var result = command.ExecuteScalar();
        //    return result?.ToString() ?? "Unknown";
        //}

        //public override int SaveChanges()
        //{
        //    // Create an audit log for any changes before saving to the database
        //    AddAuditLogs();

        //    // Call the base method to perform the actual database save
        //    return base.SaveChanges();
        //}

        //private void AddAuditLogs()
        //{
        //    var entries = ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
        //        .ToList();

        //    foreach (var entry in entries)
        //    {
        //        var auditLog = new AuditLog
        //        {
        //            UserId = GetCurrentUserId(),  // You can set this from the current authenticated user context.
        //            Action = $"{entry.State} {entry.Entity.GetType().Name}",
        //            ActionDate = DateTime.UtcNow,
        //            Details = GetEntityDetails(entry)
        //        };

        //        AuditLogs.Add(auditLog);
        //    }
        //}
        //private string GetEntityDetails(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        //{
        //    // Generate some details to log, such as the entity's state and its properties
        //    var details = "";
        //    if (entry.State == EntityState.Added)
        //    {
        //        details = "Entity added: " + string.Join(", ", entry.CurrentValues.Properties.Select(p => $"{p.Name}: {entry.CurrentValues[p]}"));
        //    }
        //    else if (entry.State == EntityState.Modified)
        //    {
        //        details = "Entity modified: " + string.Join(", ", entry.OriginalValues.Properties.Select(p => $"{p.Name}: {entry.OriginalValues[p]} -> {entry.CurrentValues[p]}"));
        //    }
        //    else if (entry.State == EntityState.Deleted)
        //    {
        //        details = "Entity deleted: " + string.Join(", ", entry.OriginalValues.Properties.Select(p => $"{p.Name}: {entry.OriginalValues[p]}"));
        //    }

        //    return details;
        //}
        //private Guid GetCurrentUserId()
        //{
        //    // Ideally, you would get the current user ID from your authentication system (e.g., from a token or session)
        //    // For now, we can use a placeholder for demonstration.
        //    return Guid.NewGuid();  // Replace with actual logic to get the authenticated user ID
        //}
    }
}
