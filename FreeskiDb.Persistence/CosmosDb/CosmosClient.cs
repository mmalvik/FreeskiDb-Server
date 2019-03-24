using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace FreeskiDb.Persistence.CosmosDb
{
    public class CosmosClient : ICosmosClient
    {
        private readonly DocumentClient _documentClient;

        public CosmosClient(string cosmosDbUri, string cosmosDbPrimaryKey)
        {
            _documentClient = new DocumentClient(new Uri(cosmosDbUri), cosmosDbPrimaryKey);
        }

        public async Task CreateDatabaseIfNotExistsAsync(string databaseId)
        {
            await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId });
        }

        public async Task DeleteDatabaseAsync(string databaseId)
        {
            await _documentClient.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
        }

        public async Task CreateCollectionIfNotExistsAsync(string databaseId, string collectionId)
        {
            await _documentClient.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(databaseId),
                new DocumentCollection { Id = collectionId },
                new RequestOptions { OfferThroughput = 400 });
        }

        public async Task DeleteCollectionAsync(string databaseId, string collectionId)
        {
            await _documentClient.DeleteDocumentCollectionAsync(
                UriFactory.CreateDocumentCollectionUri(databaseId, collectionId));
        }

        public async Task CreateDocument(Uri documentCollectionUri, object document)
        {
            await _documentClient.CreateDocumentAsync(documentCollectionUri, document);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(Uri documentCollectionUri, string query)
        {
            var queryResult = new List<T>();
            var querySetup = _documentClient.CreateDocumentQuery<T>(documentCollectionUri, query).AsDocumentQuery();

            while (querySetup.HasMoreResults)
            {
                queryResult.AddRange(await querySetup.ExecuteNextAsync<T>());
            }

            return queryResult.AsEnumerable();
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(Uri documentCollectionUri, SqlQuerySpec querySpec)
        {
            var queryResult = new List<T>();
            var querySetup = _documentClient.CreateDocumentQuery<T>(documentCollectionUri, querySpec).AsDocumentQuery();

            while (querySetup.HasMoreResults)
            {
                queryResult.AddRange(await querySetup.ExecuteNextAsync<T>());
            }

            return queryResult.AsEnumerable();
        }
    }
}