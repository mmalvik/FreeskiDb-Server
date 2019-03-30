$resourceGroupName="Freeskidb-Cosmos-Group"

az group show  --name $resourceGroupName
az group delete --name $resourceGroupName --no-wait