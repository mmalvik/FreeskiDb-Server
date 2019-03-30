using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace FreeskiDb.Persistence.CosmosDb
{
    public interface ICosmosClient
    {
        Task CreateDatabaseIfNotExistsAsync();

        Task DeleteDatabaseAsync();

        Task CreateCollectionIfNotExistsAsync();

        Task DeleteCollectionAsync();

        Task<ResourceResponse<Document>> CreateDocument(object document);

        /// <summary>
        /// See sample here:
        /// https://github.com/Azure/azure-cosmos-dotnet-v2/blob/f374cc601f4cf08d11c88f0c3fa7dcefaf7ecfe8/samples/code-samples/DocumentManagement/Program.cs#L211
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        Task<ResourceResponse<Document>> ReadDocument(Guid documentId);

        Task DeleteDocument(Guid documentId);

        Task<IEnumerable<T>> ExecuteQuery<T>(string query);

        Task<IEnumerable<T>> ExecuteQuery<T>(SqlQuerySpec query);
    }
}