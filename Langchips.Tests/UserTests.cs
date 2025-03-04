using Langchips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Tests
{
    [TestFixture]
    internal class UserTests
    {
        [Test]
        public void Constructor_ShouldInitializeUser_WithValidParameters()
        {
            var name = "John";
            var surname = "Doe";
            var email = "john.doe@example.com";
            var password = "TestPassword123!";
            var username = "johndoe";

            var user = new User(name, surname, email, password, username);

            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(surname, user.Surname);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(username, user.Username);
            Assert.IsNotNull(user.PasswordHash);
            Assert.IsNotNull(user.Salt);
            Assert.AreNotEqual(user.PasswordHash, password); 
            //Assert.AreEqual(user.CreatedAt.Date, DateTime.UtcNow.Date); // Check if CreatedAt is set to current UTC date
        }

        [Test]
        public void SetPassword_ShouldHashPassword()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "TestPassword123!", "johndoe");

            var originalHash = user.PasswordHash;
            var originalSalt = user.Salt;

            user.SetPassword("NewPassword123!");

            Assert.AreNotEqual(originalHash, user.PasswordHash); // Ensure the hash has changed
            Assert.AreNotEqual(originalSalt, user.Salt); // Ensure the salt has changed
            Assert.AreNotEqual(user.PasswordHash, "NewPassword123!"); // Ensure it's hashed and not equal to plain password
        }

    }
}
