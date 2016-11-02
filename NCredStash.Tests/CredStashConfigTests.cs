using NUnit.Framework;

namespace NCredStash.Tests
{
    [TestFixture]
    public class CredStashConfigTests
    {
        [Test]
        public void CreateConfig_ReturnsExpectedResult()
        {
            // Arrange

            // Act
            var result = CredStashConfig.Default();

            // Assert
            Assert.AreEqual("credential-store", result.TableName);
            Assert.AreEqual("name", result.KeyName);
        }
    }
}