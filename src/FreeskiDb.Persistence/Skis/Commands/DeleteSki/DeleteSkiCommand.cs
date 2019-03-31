using System;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Commands.DeleteSki
{
    public class DeleteSkiCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}