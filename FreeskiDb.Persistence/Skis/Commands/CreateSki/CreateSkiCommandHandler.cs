using System;
using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Commands.CreateSki
{
    public class CreateSkiCommandHandler : IRequestHandler<CreateSkiCommand, Guid>
    {
        private readonly ICosmosClient _cosmosClient;

        public CreateSkiCommandHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<Guid> Handle(CreateSkiCommand request, CancellationToken cancellationToken)
        {
            var result = await _cosmosClient.CreateDocument(request.Ski);

            return Guid.Parse(result.Resource.Id);
        }
    }
}