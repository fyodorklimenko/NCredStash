using System.Collections.Generic;

namespace NCredStash.Storages.MasterKey
{
    public interface IMasterKeyStorage
    {
        byte[] Decrypt(byte[] keyBytes, Dictionary<string, string> context);
    }
}