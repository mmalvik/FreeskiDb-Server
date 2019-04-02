## FreeskiDb-Server

[![Build Status](https://dev.azure.com/mmalvik/FreeskiDb-Server/_apis/build/status/mmalvik.FreeskiDb-Server?branchName=master)](https://dev.azure.com/mmalvik/FreeskiDb-Server/_build/latest?definitionId=2&branchName=master)

_The IMDb for freeskis?! Not yet..._

Just a tiny ASP.NET Core WebApi for hobby purposes.
Uses the [Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction) for storage.

### Prerequisites
- ASP.NET Core 2.2
- [Azure Cosmos DB Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator)

In order to run the tests you need the Azure Cosmos DB installed and running.

Currently the emulator is only available on Windows. However Microsoft is [working on](https://feedback.azure.com/forums/263030-azure-cosmos-db/suggestions/18533509-add-documentdb-emulator-support-for-mac-os-x-and) bringing the emulator to Mac and Linux.

The same can be said for the [Azure Cosmos DB Container](https://github.com/Azure/azure-cosmos-db-emulator-docker), which only runs on Windows containers 😢