using System.Collections.Generic;
using FreeskiDb.Persistence.Entities;
using MediatR;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiList
{
    public class GetSkiListQuery : IRequest<IEnumerable<SkiDocument>>
    {
    }
}