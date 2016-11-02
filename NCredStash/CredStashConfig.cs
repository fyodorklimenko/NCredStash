using System;

namespace NCredStash
{
    public sealed class CredStashConfig
    {
        public CredStashConfig(string tableName, string keyName)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (string.IsNullOrEmpty(keyName))
                throw new ArgumentNullException("keyName");

            TableName = tableName;
            KeyName = keyName;
        }

        public string TableName { get; set; }
        public string KeyName { get; set; }

        public static CredStashConfig Default()
        {
            return new CredStashConfig("credential-store", "name");
        }
    }
}