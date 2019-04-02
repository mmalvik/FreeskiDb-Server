using System;
using System.Threading;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Commands.UpdateSki
{
    public class UpdateSkiCommandHandler : IRequestHandler<UpdateSkiCommand>
    {
        private readonly ICosmosClient _cosmosClient;

        public UpdateSkiCommandHandler(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<Unit> Handle(UpdateSkiCommand request, CancellationToken cancellationToken)
        {
            var skiDocument = new SkiDocument
            {
                Brand = request.Ski.Brand,
                ModelName = request.Ski.ModelName,
                TipWidth = request.Ski.TipWidth,
                WaistWidth = request.Ski.WaistWidth,
                TailWidth = request.Ski.TailWidth,
                Id = request.Id,
                TimeStamp = DateTime.UtcNow
            };

            await _cosmosClient.UpsertDocument(skiDocument);
            return Unit.Value;
        }
    }
}