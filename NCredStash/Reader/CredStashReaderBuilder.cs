using System;
using Amazon.DynamoDBv2;
using Amazon.KeyManagementService;
using NCredStash.Crypto;
using NCredStash.Storages.Credential;
using NCredStash.Storages.MasterKey;

namespace NCredStash.Reader
{
    public sealed class CredStashReaderBuilder
    {
        private IAmazonDynamoDB _dynamoDbClient;
        private IAmazonKeyManagementService _keyManagementServiceClient;

        public CredStashReaderBuilder WithDynamoDbClient(IAmazonDynamoDB ddbClient)
        {
            if (ddbClient == null)
                throw new ArgumentNullException("ddbClient");

            _dynamoDbClient = ddbClient;

            return this;
        }

        public CredStashReaderBuilder WithKeyManagementServiceClient(IAmazonKeyManagementService kmsClient)
        {
            if (kmsClient == null)
                throw new ArgumentNullException("kmsClient");

            _keyManagementServiceClient = kmsClient;

            return this;
        }

        public ICredStashReader Build()
        {
            if (_dynamoDbClient == null)
                _dynamoDbClient = new AmazonDynamoDBClient();
            if (_keyManagementServiceClient == null)
                _keyManagementServiceClient = new AmazonKeyManagementServiceClient();

            var credentialStorage = new CredentialStorage(CredStashConfig.Default(), _dynamoDbClient);
            var masterKeyStorage = new MasterKeyStorage(_keyManagementServiceClient);

            return new CredStashReader(credentialStorage, masterKeyStorage, new HmacSha256Verifier(), new AesCrypto());
        }

        public static CredStashReaderBuilder New()
        {
            return new CredStashReaderBuilder();
        }
    }
}