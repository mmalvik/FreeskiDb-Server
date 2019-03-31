using System;
using FreeskiDb.Persistence.Entities;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Commands.UpdateSki
{
    public class UpdateSkiCommand : IRequest
    {
        public Guid Id { get; set; }

        public Ski Ski { get; set; }
        
    }
}