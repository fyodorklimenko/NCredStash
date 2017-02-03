using Xunit;

namespace NCredStash.Tests
{
    public class CredStashConfigTests
    {
        [Fact]
        public void CreateConfig_ReturnsExpectedResult()
        {
            // Arrange

            // Act
            var result = CredStashConfig.Default();

            // Assert
            Assert.Equal("credential-store", result.TableName);
            Assert.Equal("name", result.KeyName);
        }
    }
}
