using Langchips.Data;
using Langchips.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models.Helpers
{
    public static class DbSeeder
    {
        public static void SeedData(AppDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var users = new List<User>
                {
                    new User("John", "Doe", "john.doe@example.com", "john_doe", "Password123"),
                    new User("Jane", "Smith", "jane.smith@example.com",  "jane_smith", "Password123"),
                    new User("Alice", "Johnson", "alice.johnson@example.com", "alice_johnson", "Password123"),
                    new User("Bob", "Williams", "bob.williams@example.com", "bob_williams", "Password123"),
                    new User("Charlie", "Brown", "charlie.brown@example.com", "charlie_brown", "Password123")
                };

                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();

                foreach (var user in users)
                {
                    // Create a set for the user
                    var set = new Set(user.Id, $"{user.Name}'s Sample Set", Language.English, Language.Spanish)
                    {
                        Terms = new List<Term>
                        {
                            new Term { InputPhrase = "Hello", Translations = new List<Translation>
                                {
                                    new Translation { Content = "Hola", Language = Language.Spanish }
                                }
                            },
                            new Term { InputPhrase = "Goodbye", Translations = new List<Translation>
                                {
                                    new Translation { Content = "Adiós", Language = Language.Spanish }
                                }
                            }
                        }
                    };

                    dbContext.Sets.Add(set);
                }

                dbContext.SaveChanges();

                Console.WriteLine("Sample data has been seeded.");
            }
            else
            {
                Console.WriteLine("Database already contains users, skipping seed.");
            }
        }
    }
}
