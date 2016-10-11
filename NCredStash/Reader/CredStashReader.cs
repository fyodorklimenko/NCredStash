using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.KeyManagementService;
using NCredStash.Crypto;
using NCredStash.Storages.Credential;
using NCredStash.Storages.MasterKey;

namespace NCredStash.Reader
{
    public class CredStashReader : CredStashBase, ICredStashReader
    {
        private readonly IHmacVerifier _hmacVerifier;

        public CredStashReader(ICredentialStorage credentialStorage, IMasterKeyStorage masterKeyStorage, IHmacVerifier hmacVerifier, AesCryptoBase crypto)
            : base(credentialStorage, masterKeyStorage, crypto)
        {
            if (hmacVerifier == null)
                throw new ArgumentNullException("hmacVerifier");

            _hmacVerifier = hmacVerifier;
        }

        public string GetSecret(string key, Dictionary<string, string> context)
        {
            var item = CredentialStorage.GetItem(key);

            var plainTextBytes = MasterKeyStorage.Decrypt(GetBytes(item["key"]), context);

            var kmsHmac = new byte[32];
            Buffer.BlockCopy(plainTextBytes, 32, kmsHmac, 0, 32);
            _hmacVerifier.Verify(kmsHmac, GetBytes(item["contents"]), item["hmac"]);

            var kmsKeyBytes = new byte[32];
            Buffer.BlockCopy(plainTextBytes, 0, kmsKeyBytes, 0, 32);
            var decryptedBytes = Crypto.Decrypt(kmsKeyBytes, GetBytes(item["contents"]));

            var secret = Encoding.UTF8.GetString(decryptedBytes);

            return secret;
        }

        public static CredStashReader CreateReader(CredStashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            var credentialStorage = new CredentialStorage(config, new AmazonDynamoDBClient(config.RegionEndpoint));
            var masterKeyStorage = new MasterKeyStorage(new AmazonKeyManagementServiceClient(config.RegionEndpoint));
            var hmacVerifier = new HmacSha256Verifier();
            var crypto = new AesCrypto();

            return new CredStashReader(credentialStorage, masterKeyStorage, hmacVerifier, crypto);
        }

        private static byte[] GetBytes(string value)
        {
            return !string.IsNullOrEmpty(value) ? Convert.FromBase64String(value) : null;
        }
    }
}