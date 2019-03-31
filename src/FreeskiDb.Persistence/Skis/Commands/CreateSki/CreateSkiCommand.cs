using System;
using FreeskiDb.Persistence.Entities;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Commands.CreateSki
{
    public class CreateSkiCommand : IRequest<Guid>
    {
        public Ski Ski { get; set; }
    }
}