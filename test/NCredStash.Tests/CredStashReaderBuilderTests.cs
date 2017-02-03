using Amazon.DynamoDBv2;
using Amazon.KeyManagementService;
using NCredStash.Reader;
using NSubstitute;
using Xunit;

namespace NCredStash.Tests
{
    public class CredStashReaderBuilderTests
    {
        [Fact]
        public void Build_ReturnsExpectedResult()
        {
            // Arrange
            var ddbClient = Substitute.For<IAmazonDynamoDB>();
            var kmsClient = Substitute.For<IAmazonKeyManagementService>();
            var credStashConfig = new CredStashConfig("tableName", "keyName");

            // Act
            var reader = CredStashReaderBuilder.New()
                .WithDynamoDbClient(ddbClient)
                .WithKeyManagementServiceClient(kmsClient)
                .WithCredStashConfig(credStashConfig)
                .Build();

            // Assert
            Assert.NotNull(reader);
        }
    }
}
