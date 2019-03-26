using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using MediatR;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiDetails
{
    public class GetSkiDetailsQueryHandler : IRequestHandler<GetSkiDetailsQuery, SkiDetails>
    {
        private readonly ICosmosClient _cosmosClient;
        private readonly Uri _docCollectionUri = UriFactory.CreateDocumentCollectionUri("FreeskiDb", "SkiCollection");

        public GetSkiDetailsQueryHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<SkiDetails> Handle(GetSkiDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = new SqlQuerySpec("SELECT * FROM skis WHERE skis.id = @id",
                new SqlParameterCollection {new SqlParameter("@id", request.Id)});

            var result = await _cosmosClient.ExecuteQuery<Ski>(_docCollectionUri, query);
            return result.Any() ? new SkiDetails {Ski = result.First() } : new SkiDetails();
        }
    }
}