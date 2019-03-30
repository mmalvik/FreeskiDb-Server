# Set variables for the new SQL API account and container
$resourceGroupName="Freeskidb-Cosmos-Group"
$location="northeurope"

#Account namme needs to be lower case, will be URI for Cosmos: <accountName>.documents.azure.com
$accountName="eric-cartman" #needs to be lower case, will be URI for
$id = "eric-cartman-northeurope"

# Create a resource group
az group create --name $resourceGroupName --location northeurope

# Create a SQL API Cosmos DB account with session consistency and multi-master enabled
az cosmosdb create --resource-group $resourceGroupName --name $accountName --kind GlobalDocumentDB --default-consistency-level "Session" --enable-multiple-write-locations false

# List necessary keys for connection
az cosmosdb list-connection-strings --name $accountName --resource-group $resourceGroupName
