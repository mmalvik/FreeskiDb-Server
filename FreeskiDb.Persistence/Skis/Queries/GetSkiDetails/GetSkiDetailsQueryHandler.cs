using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using MediatR;
using Microsoft.Azure.Documents;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiDetails
{
    public class GetSkiDetailsQueryHandler : IRequestHandler<GetSkiDetailsQuery, SkiDocument>
    {
        private readonly ICosmosClient _cosmosClient;

        public GetSkiDetailsQueryHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<SkiDocument> Handle(GetSkiDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await _cosmosClient.ReadDocument(request.Id);
            var skiDocument = (SkiDocument)(dynamic)result.Resource;
            return skiDocument;
        }
    }
}