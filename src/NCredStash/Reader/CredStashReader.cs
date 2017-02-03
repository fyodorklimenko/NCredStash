using System;
using System.Collections.Generic;
using System.Text;
using NCredStash.Crypto;
using NCredStash.Storages.Credential;
using NCredStash.Storages.MasterKey;

namespace NCredStash.Reader
{
    public sealed class CredStashReader : CredStashBase, ICredStashReader
    {
        private readonly IHmacVerifier _hmacVerifier;

        public CredStashReader(ICredentialStorage credentialStorage, IMasterKeyStorage masterKeyStorage, IHmacVerifier hmacVerifier, AesCryptoBase aesCrypto)
            : base(credentialStorage, masterKeyStorage, aesCrypto)
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
            var decryptedBytes = AesCrypto.Decrypt(kmsKeyBytes, GetBytes(item["contents"]));

            var secret = Encoding.UTF8.GetString(decryptedBytes);

            return secret;
        }

        private static byte[] GetBytes(string value)
        {
            return !string.IsNullOrEmpty(value) ? Convert.FromBase64String(value) : null;
        }
    }
}