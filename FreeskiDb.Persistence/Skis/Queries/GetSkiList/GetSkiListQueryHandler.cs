using System;
using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using MediatR;
using Microsoft.Azure.Documents.Client;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiList
{
    public class GetSkiListQueryHandler : IRequestHandler<GetSkiListQuery, SkiListModel>
    {
        private readonly ICosmosClient _cosmosClient;
        private readonly Uri _docCollectionUri = UriFactory.CreateDocumentCollectionUri("FreeskiDb", "SkiCollection");

        public GetSkiListQueryHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<SkiListModel> Handle(GetSkiListQuery request, CancellationToken cancellationToken)
        {
            var result = await _cosmosClient.ExecuteQuery<Ski>(_docCollectionUri, "SELECT * FROM SkiCollection");
            return new SkiListModel {Skis = result};
        }
    }
}