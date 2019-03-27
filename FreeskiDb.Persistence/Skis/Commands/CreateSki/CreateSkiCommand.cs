using FreeskiDb.Persistence.Entities;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Commands.CreateSki
{
    public class CreateSkiCommand : IRequest<Ski>
    {
        public string Brand { get; set; }

        public string Model { get; set; }

    }
}