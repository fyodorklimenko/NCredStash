using System.Collections.Generic;

namespace NCredStash.Storages.Credential
{
    public interface ICredentialStorage
    {
        Dictionary<string, string> GetItem(string key);
    }
}