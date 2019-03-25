using MediatR;

namespace FreeskiDb.Persistence.Skis.Commands.CreateSki
{
    public class CreateSkiCommand : IRequest
    {
        public string Brand { get; set; }

        public string Model { get; set; }

    }
}