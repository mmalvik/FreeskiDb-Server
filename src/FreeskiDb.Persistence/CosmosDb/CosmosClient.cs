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
        private readonly CosmosConfiguration _configuration;
        private readonly Uri _databaseUri;
        private readonly Uri _documentCollectionUri;

        public CosmosClient(CosmosConfiguration configuration)
        {
            _configuration = configuration;
            _documentClient = new DocumentClient(new Uri(configuration.CosmosUri), configuration.CosmosKey);
            _databaseUri = UriFactory.CreateDatabaseUri(configuration.DatabaseId);
            _documentCollectionUri =
                UriFactory.CreateDocumentCollectionUri(configuration.DatabaseId, configuration.CollectionId);
        }

        public async Task CreateDatabaseIfNotExistsAsync()
        {
            await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = _configuration.DatabaseId });
        }

        public async Task DeleteDatabaseAsync()
        {
            await _documentClient.DeleteDatabaseAsync(_databaseUri);
        }

        public async Task CreateCollectionIfNotExistsAsync()
        {
            await _documentClient.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(_configuration.DatabaseId),
                new DocumentCollection { Id = _configuration.CollectionId },
                new RequestOptions { OfferThroughput = 400 });
        }

        public async Task DeleteCollectionAsync()
        {
            await _documentClient.DeleteDocumentCollectionAsync(_documentCollectionUri);
        }

        public async Task<ResourceResponse<Document>> CreateDocument(object document)
        {
            return await _documentClient.CreateDocumentAsync(_documentCollectionUri, document);
        }

        public async Task<ResourceResponse<Document>> ReadDocument(Guid documentId)
        {
            var documentUri = UriFactory.CreateDocumentUri(_configuration.DatabaseId, _configuration.CollectionId,
                documentId.ToString());
            return await _documentClient.ReadDocumentAsync(documentUri);
        }

        public async Task<ResourceResponse<Document>> UpsertDocument(object document)
        {
            return await _documentClient.UpsertDocumentAsync(_documentCollectionUri, document);
        }

        public async Task DeleteDocument(Guid documentId)
        {
            var documentUri = UriFactory.CreateDocumentUri(_configuration.DatabaseId, _configuration.CollectionId,
                documentId.ToString());
            await _documentClient.DeleteDocumentAsync(documentUri);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(string query)
        {
            var queryResult = new List<T>();
            var querySetup = _documentClient.CreateDocumentQuery<T>(_documentCollectionUri, query).AsDocumentQuery();

            while (querySetup.HasMoreResults)
            {
                queryResult.AddRange(await querySetup.ExecuteNextAsync<T>());
            }

            return queryResult.AsEnumerable();
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(SqlQuerySpec querySpec)
        {
            var queryResult = new List<T>();
            var querySetup = _documentClient.CreateDocumentQuery<T>(_documentCollectionUri, querySpec).AsDocumentQuery();

            while (querySetup.HasMoreResults)
            {
                queryResult.AddRange(await querySetup.ExecuteNextAsync<T>());
            }

            return queryResult.AsEnumerable();
        }
    }
}