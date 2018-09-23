using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FreeskiDb.WebApi.CosmosDb;
using FreeskiDb.WebApi.Documents;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace FreeskiDb.WebApi.Repository
{
    public class SkiRepository : ISkiRepository
    {
        private readonly ICosmosClient _cosmosClient;
        private readonly Uri _docCollectionUri;

        public SkiRepository(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _docCollectionUri = UriFactory.CreateDocumentCollectionUri("FreeskiDb", "TestCollection");
        }

        public async Task<Ski> GetById(string id)
        {
            var parameters = new SqlParameterCollection
            {
                new SqlParameter("@id", id)
            };
            var query = new SqlQuerySpec("SELECT * FROM c WHERE c.id = @id", parameters);
            var skis = await _cosmosClient.ExecuteQuery<Ski>(_docCollectionUri, query);

            return skis.FirstOrDefault();
        }

        public async Task<IEnumerable<Ski>> List()
        {
            return await _cosmosClient.ExecuteQuery<Ski>(_docCollectionUri, "SELECT * FROM TestCollection");
        }

        public IEnumerable<Ski> List(Expression<Func<Ski, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Ski entity)
        {
            await _cosmosClient.CreateDocument(_docCollectionUri, entity);
        }

        public void Delete(Ski entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Ski entity)
        {
            throw new NotImplementedException();
        }
    }
}