using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace FreeskiDb.WebApi.CosmosDb
{
    public class CosmosClient : ICosmosClient
    {
        private readonly DocumentClient _documentClient;

        public CosmosClient(string cosmosDbUri, string cosmosDbPrimaryKey)
        {
            _documentClient = new DocumentClient(new Uri(cosmosDbUri), cosmosDbPrimaryKey);
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