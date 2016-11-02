using System;
using System.Collections.Generic;
using System.IO;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace NCredStash.Storages.MasterKey
{
    public sealed class MasterKeyStorage : IMasterKeyStorage
    {
        private readonly IAmazonKeyManagementService _kmsClient;

        public MasterKeyStorage(IAmazonKeyManagementService kmsClient)
        {
            if (kmsClient == null)
                throw new ArgumentNullException("kmsClient");

            _kmsClient = kmsClient;
        }

        public byte[] Decrypt(byte[] keyBytes, Dictionary<string, string> context)
        {
            var cipherStream = new MemoryStream(keyBytes) { Position = 0 };
            var request = new DecryptRequest
            {
                CiphertextBlob = cipherStream,
                EncryptionContext = context
            };
            var response = _kmsClient.Decrypt(request);
            var result = new byte[response.Plaintext.Length];
            response.Plaintext.Read(result, 0, (int)response.Plaintext.Length);

            return result;
        }
    }
}