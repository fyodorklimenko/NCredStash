using System;
using Amazon;

namespace NCredStash
{
    public class CredStashConfig
    {
        public CredStashConfig(string tableName, string keyName, RegionEndpoint regionEndpoint)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (string.IsNullOrEmpty(keyName))
                throw new ArgumentNullException("keyName");
            if (regionEndpoint == null)
                throw new ArgumentNullException("regionEndpoint");

            TableName = tableName;
            KeyName = keyName;
            RegionEndpoint = regionEndpoint;
        }

        public string TableName { get; set; }
        public string KeyName { get; set; }
        public RegionEndpoint RegionEndpoint { get; set; }

        public static CredStashConfig CreateConfig(RegionEndpoint regionEndpoint)
        {
            return new CredStashConfig("credential-store", "name", regionEndpoint);
        }
    }
}