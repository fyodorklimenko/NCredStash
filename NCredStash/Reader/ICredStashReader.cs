using System.Collections.Generic;

namespace NCredStash.Reader
{
    public interface ICredStashReader
    {
        string GetSecret(string key, Dictionary<string, string> context);
    }
}