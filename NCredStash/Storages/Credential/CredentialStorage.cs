using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace NCredStash.Storages.Credential
{
    public class CredentialStorage : ICredentialStorage
    {
        private readonly CredStashConfig _config;
        private readonly IAmazonDynamoDB _ddbClient;

        public CredentialStorage(CredStashConfig config, IAmazonDynamoDB ddbClient)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (ddbClient == null)
                throw new ArgumentNullException("ddbClient");

            _config = config;
            _ddbClient = ddbClient;
        }

        public Dictionary<string, string> GetItem(string key)
        {
            var request = new QueryRequest
            {
                TableName = _config.TableName,
                Limit = 1,
                ScanIndexForward = false,
                ConsistentRead = true,
                KeyConditions = new Dictionary<string, Condition>
                {
                    {
                        _config.KeyName, new Condition
                        {
                            ComparisonOperator = ComparisonOperator.EQ,
                            AttributeValueList = new List<AttributeValue> {new AttributeValue(key)}
                        }
                    }
                }
            };
            var response = _ddbClient.Query(request);
            if (response.HttpStatusCode != HttpStatusCode.OK || !response.Items.Any())
                throw new Exception(string.Format("Secret Name: {0} not found.", key));

            var item = response.Items.First();

            return item.ToDictionary(attribute => attribute.Key, attribute => attribute.Value.S);
        }
    }
}