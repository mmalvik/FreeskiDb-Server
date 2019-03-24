using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace FreeskiDb.Persistence.CosmosDb
{
    public interface ICosmosClient
    {
        Task CreateDatabaseIfNotExistsAsync(string databaseId);

        bool DoesDatabaseExist(string databaseId);

        Task CreateCollectionIfNotExistsAsync(string databaseId, string collectionId);

        Task CreateDocument(Uri documentCollectionUri, object document);

        Task<IEnumerable<T>> ExecuteQuery<T>(Uri documentCollectionUri, string query);

        Task<IEnumerable<T>> ExecuteQuery<T>(Uri documentCollectionUri, SqlQuerySpec query);
    }
}