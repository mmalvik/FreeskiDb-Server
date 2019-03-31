using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiList
{
    public class GetSkiListQueryHandler : IRequestHandler<GetSkiListQuery, IEnumerable<SkiDocument>>
    {
        private readonly ICosmosClient _cosmosClient;

        public GetSkiListQueryHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<IEnumerable<SkiDocument>> Handle(GetSkiListQuery request, CancellationToken cancellationToken)
        {
            return await _cosmosClient.ExecuteQuery<SkiDocument>("SELECT * FROM SkiCollection");
        }
    }
}