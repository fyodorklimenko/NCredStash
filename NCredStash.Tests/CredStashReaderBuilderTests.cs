using Amazon.DynamoDBv2;
using Amazon.KeyManagementService;
using NCredStash.Reader;
using NSubstitute;
using NUnit.Framework;

namespace NCredStash.Tests
{
    [TestFixture]
    public class CredStashReaderBuilderTests
    {
        [Test]
        public void Build_ReturnsExpectedResult()
        {
            // Arrange
            var ddbClient = Substitute.For<IAmazonDynamoDB>();
            var kmsClient = Substitute.For<IAmazonKeyManagementService>();

            // Act
            var reader = CredStashReaderBuilder.New()
                .WithDynamoDbClient(ddbClient)
                .WithKeyManagementServiceClient(kmsClient)
                .Build();

            // Assert
            Assert.NotNull(reader);
        }
    }
}