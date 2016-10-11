using Amazon;
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
            var result = CredStashConfig.CreateConfig(RegionEndpoint.USEast1);

            // Assert
            Assert.AreEqual("credential-store", result.TableName);
            Assert.AreEqual("name", result.KeyName);
            Assert.AreEqual(RegionEndpoint.USEast1, result.RegionEndpoint);
        }
    }
}