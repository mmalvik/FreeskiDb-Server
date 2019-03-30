using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Commands.DeleteSki
{
    public class DeleteSkiCommandHandler : IRequestHandler<DeleteSkiCommand>
    {
        private readonly ICosmosClient _cosmosClient;

        public DeleteSkiCommandHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<Unit> Handle(DeleteSkiCommand request, CancellationToken cancellationToken)
        {
            await _cosmosClient.DeleteDocument(request.Id);
            return Unit.Value;
        }
    }
}