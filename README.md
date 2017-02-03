# NCredStash
[![Build status](https://ci.appveyor.com/api/projects/status/sf3s3r9m3xvf8aao?svg=true)](https://ci.appveyor.com/project/kokhans/ncredstash)

NCredStash provides an easy way to securely store and retrieve credentials in AWS environment.

## Getting Started
### Supported Frameworks
* netcoreapp1.0
* net461

### Basic Use
To instantiate CredStashReader we can call method **New()** from **CredStashBuilder** class and then call **Build()**
```
var reader = CredStashReaderBuilder.New().Build();
```
Also we can configure our **DynamoDbClient**, **KeyManagementServiceClient** and **CredStashConfig** before **CredStashReader** instantiation calling methods **WithDynamoDbClient()**, **WithKeyManagementServiceClient()** and **WithCredStashConfig()** from **CredStashBuilder** class and then call **Build()**
```
var ddbClient = new AmazonDynamoDBClient();
var kmsClient = new AmazonKeyManagementServiceClient();
var credStashConfig = new CredStashConfig("tableName", "keyName");
var reader = CredStashReaderBuilder.New()
                .WithDynamoDbClient(ddbClient)
                .WithKeyManagementServiceClient(kmsClient)
                .WithCredStashConfig(credStashConfig)
                .Build();
```
To fetch credential we can use method **GetSecret(string key, Dictionary<string, string> context)** from CredStashReader instance
```
var secret = reader.GetSecret("keyName", new Dictionary<string, string>());
```

## Licence
NCredStash is licensed under the [MIT License](https://github.com/kokhans/NCredStash/blob/master/LICENSE).

## Dependencies
* Microsoft.NETCore.App
* AWSSDK.Core
* AWSSDK.DynamoDBv2
* AWSSDK.KeyManagementService
* BounyCastle
* BounyCastle.CoreClr
* xUnit
* NSubstitute

## Notifications
To be notified for any new pull requests and issues that are created [watch](https://github.com/kokhans/NCredStash/subscription) NCredStash.

## Support
Do you have questions, feature requests or would you like to report a bug? Please post them on the [issue list](https://github.com/kokhans/NCredStash/issues).
