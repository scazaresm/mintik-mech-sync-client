using MechanicalSyncApp.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp_Tests
{
    internal class SHA256ChecksumCalculator
    {
        [TestFixture]
        public class Sha256FileChecksumCalculatorTests
        {
            private Sha256FileChecksumCalculator calculator;

            [SetUp]
            public void SetUp()
            {
                calculator = new Sha256FileChecksumCalculator();
            }

            [Test]
            public void CalculateChecksum_ShouldThrowFileNotFoundException_WhenFilePathDoesNotExist()
            {
                string nonExistentFilePath = "nonexistentfile.txt";

                var ex = Assert.Throws<FileNotFoundException>(() => calculator.CalculateChecksum(nonExistentFilePath));
                Assert.That(ex.Message, Does.Contain($"Could not find"));
            }

            [Test]
            public void CalculateChecksum_ShouldReturnCorrectChecksum_ForValidFile()
            {
                string filePath = "testfile.txt";
                File.WriteAllText(filePath, "test content");

                string expectedChecksum;
                using (SHA256 sha256 = SHA256.Create())
                {
                    var hash = sha256.ComputeHash(File.ReadAllBytes(filePath));
                    expectedChecksum = BitConverter.ToString(hash).Replace("-", "").ToLower();
                }

                string actualChecksum = calculator.CalculateChecksum(filePath);

                Assert.That(actualChecksum, Is.EqualTo(expectedChecksum));

                File.Delete(filePath);
            }

            [Test]
            public Task CalculateChecksumAsync_ShouldThrowFileNotFoundException_WhenFilePathDoesNotExist()
            {
                string nonExistentFilePath = "nonexistentfile.txt";

                var ex = Assert.ThrowsAsync<FileNotFoundException>(async () => await calculator.CalculateChecksumAsync(nonExistentFilePath));
                Assert.That(ex.Message, Does.Contain($"Could not find file"));
                return Task.CompletedTask;
            }

            [Test]
            public async Task CalculateChecksumAsync_ShouldReturnCorrectChecksum_ForValidFile()
            {
                string filePath = "testfile.txt";
                File.WriteAllText(filePath, "test content");

                string expectedChecksum;
                using (SHA256 sha256 = SHA256.Create())
                {
                    var hash = sha256.ComputeHash(File.ReadAllBytes(filePath));
                    expectedChecksum = BitConverter.ToString(hash).Replace("-", "").ToLower();
                }

                string actualChecksum = await calculator.CalculateChecksumAsync(filePath);

                Assert.That(actualChecksum, Is.EqualTo(expectedChecksum));

                File.Delete(filePath);
            }
        }
    }
}
