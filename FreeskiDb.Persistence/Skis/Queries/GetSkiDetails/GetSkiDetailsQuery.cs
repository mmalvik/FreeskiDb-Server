using System;
using FreeskiDb.Persistence.Entities;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiDetails
{
    public class GetSkiDetailsQuery : IRequest<SkiDocument>
    {
        public GetSkiDetailsQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}