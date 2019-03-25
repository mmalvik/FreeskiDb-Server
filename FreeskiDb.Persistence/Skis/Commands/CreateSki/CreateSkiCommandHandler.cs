using System;
using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using MediatR;
using Microsoft.Azure.Documents.Client;

namespace FreeskiDb.Persistence.Skis.Commands.CreateSki
{
    public class CreateSkiCommandHandler : IRequestHandler<CreateSkiCommand, Unit>
    {
        private readonly ICosmosClient _cosmosClient;
        private readonly Uri _docCollectionUri = UriFactory.CreateDocumentCollectionUri("FreeskiDb", "SkiCollection");

        public CreateSkiCommandHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<Unit> Handle(CreateSkiCommand request, CancellationToken cancellationToken)
        {
            var ski = new Ski(request.Brand, request.Model);

            await _cosmosClient.CreateDocument(_docCollectionUri, ski);

            return Unit.Value;
        }
    }
}