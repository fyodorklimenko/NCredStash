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
        protected readonly AesCryptoBase Crypto;

        protected CredStashBase(ICredentialStorage credentialStorage, IMasterKeyStorage masterKeyStorage, AesCryptoBase crypto)
        {
            if (credentialStorage == null)
                throw new ArgumentNullException("credentialStorage");
            if (masterKeyStorage == null)
                throw new ArgumentNullException("masterKeyStorage");
            if (crypto == null)
                throw new ArgumentNullException("crypto");

            CredentialStorage = credentialStorage;
            MasterKeyStorage = masterKeyStorage;
            Crypto = crypto;
        }
    }
}