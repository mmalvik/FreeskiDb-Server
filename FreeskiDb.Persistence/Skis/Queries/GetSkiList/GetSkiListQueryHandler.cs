using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiList
{
    public class GetSkiListQueryHandler : IRequestHandler<GetSkiListQuery, SkiListModel>
    {
        private readonly ICosmosClient _cosmosClient;

        public GetSkiListQueryHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<SkiListModel> Handle(GetSkiListQuery request, CancellationToken cancellationToken)
        {
            var result = await _cosmosClient.ExecuteQuery<Ski>("SELECT * FROM SkiCollection");
            return new SkiListModel {Skis = result};
        }
    }
}