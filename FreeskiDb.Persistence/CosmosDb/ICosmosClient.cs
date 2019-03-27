using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace FreeskiDb.Persistence.CosmosDb
{
    public interface ICosmosClient
    {
        Task CreateDatabaseIfNotExistsAsync();

        Task DeleteDatabaseAsync();

        Task CreateCollectionIfNotExistsAsync();

        Task DeleteCollectionAsync();

        Task CreateDocument(object document);

        Task<IEnumerable<T>> ExecuteQuery<T>(string query);

        Task<IEnumerable<T>> ExecuteQuery<T>(SqlQuerySpec query);
    }
}