using System;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiDetails
{
    public class GetSkiDetailsQuery : IRequest<SkiDetails>
    {
        public GetSkiDetailsQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}