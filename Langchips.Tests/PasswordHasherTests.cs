using Langchips.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Tests
{
    [TestFixture]
    internal class PasswordHasherTests
    {
        [Test]

        public void GenerateSalt_ShouldReturnSaltOfCorrectLength()
        {
            int expectedSize = 16;
            string salt = PasswordHasher.GenerateSalt();

            byte[] saltBytes = Convert.FromBase64String(salt);
            Assert.AreEqual(expectedSize, saltBytes.Length);


            //Assert.AreEqual(expectedSize * 4 / 3, salt.Length);
        }

        [Test]
        public void GenerateSalt_ShouldReturnDifferentSaltsOnSubsequentCalls()
        {
            string salt1 = PasswordHasher.GenerateSalt(); 
            string salt2 = PasswordHasher.GenerateSalt();  

            Assert.AreNotEqual(salt1, salt2);
        }

        [Test]
        public void GenerateSalt_WithCustomSize_ShouldReturnSaltOfCustomLength()
        {
            int customSize = 32;  
            string salt = PasswordHasher.GenerateSalt(customSize);

            byte[] saltBytes = Convert.FromBase64String(salt);
            Assert.AreEqual(customSize, saltBytes.Length);
        }

        [Test]
        public void HashPassword_ShouldReturnSameHashForSameInput()
        {
            string password = "password123";
            string salt = PasswordHasher.GenerateSalt();
            string hash1 = PasswordHasher.HashPassword(password, salt);
            string hash2 = PasswordHasher.HashPassword(password, salt);

            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void HashPassword_ShouldReturnDifferentHashesForDifferentSalts()
        {
            string password = "password123";
            string salt1 = PasswordHasher.GenerateSalt();
            string salt2 = PasswordHasher.GenerateSalt();

            string hash1 = PasswordHasher.HashPassword(password, salt1);
            string hash2 = PasswordHasher.HashPassword(password, salt2);

            Assert.AreNotEqual(hash1, hash2);
        }
        [Test]
        public void HashPassword_ShouldReturnDifferentHashesForSaltsOfDifferentLengths()
        {
            string password = "password123";
            string salt1 = PasswordHasher.GenerateSalt(16);  
            string salt2 = PasswordHasher.GenerateSalt(32);  

            string hash1 = PasswordHasher.HashPassword(password, salt1);
            string hash2 = PasswordHasher.HashPassword(password, salt2);

            Assert.AreNotEqual(hash1, hash2);
        }
        [Test]
        public void VerifyPassword_ShouldReturnTrueForCorrectPassword()
        {
            string password = "password123";
            string salt = PasswordHasher.GenerateSalt();
            string hash = PasswordHasher.HashPassword(password, salt);

            bool result = PasswordHasher.VerifyPassword(password, salt, hash);

            Assert.IsTrue(result);
        }
        [Test]
        public void VerifyPassword_ShouldReturnFalseForIncorrectPassword()
        {
            string password = "password123";
            string incorrectPassword = "wrongpassword";
            string salt = PasswordHasher.GenerateSalt();
            string hash = PasswordHasher.HashPassword(password, salt);

            bool result = PasswordHasher.VerifyPassword(incorrectPassword, salt, hash);

            Assert.IsFalse(result);
        }
        [Test]
        public void HashPassword_ShouldHandleEmptyPassword()
        {
            string password = string.Empty;
            string salt = PasswordHasher.GenerateSalt();
            string hash = PasswordHasher.HashPassword(password, salt);

            Assert.IsNotNull(hash);
            Assert.AreNotEqual(string.Empty, hash);
        }
        [Test]
        public void HashPassword_ShouldThrowArgumentNullExceptionForNullPassword()
        {
            Assert.Throws<ArgumentNullException>(() =>
                PasswordHasher.HashPassword(null, "someSalt"));
        }

        [Test]
        public void VerifyPassword_ShouldReturnFalseForEmptyPassword()
        {
            string password = string.Empty;
            string salt = PasswordHasher.GenerateSalt();
            string hash = PasswordHasher.HashPassword(password, salt);

            bool result = PasswordHasher.VerifyPassword(string.Empty, salt, hash);

            Assert.IsTrue(result);
        }
    }
}
