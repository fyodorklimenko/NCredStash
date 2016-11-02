using System;
using NCredStash.Crypto;
using NCredStash.Storages.Credential;
using NCredStash.Storages.MasterKey;

namespace NCredStash
{
    public abstract class CredStashBase
    {
        protected readonly ICredentialStorage CredentialStorage;
        protected readonly IMasterKeyStorage MasterKeyStorage;
        protected readonly AesCryptoBase AesCrypto;

        protected CredStashBase(ICredentialStorage credentialStorage, IMasterKeyStorage masterKeyStorage, AesCryptoBase aesCrypto)
        {
            if (credentialStorage == null)
                throw new ArgumentNullException("credentialStorage");
            if (masterKeyStorage == null)
                throw new ArgumentNullException("masterKeyStorage");
            if (aesCrypto == null)
                throw new ArgumentNullException("aesCrypto");

            CredentialStorage = credentialStorage;
            MasterKeyStorage = masterKeyStorage;
            AesCrypto = aesCrypto;
        }
    }
}