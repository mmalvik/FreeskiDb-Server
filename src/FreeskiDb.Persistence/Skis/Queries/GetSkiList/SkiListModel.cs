using System.Collections.Generic;
using FreeskiDb.Persistence.Entities;

namespace FreeskiDb.Persistence.Skis.Queries.GetSkiList
{
    public class SkiListModel
    {
        public IEnumerable<Ski> Skis { get; set; }
    }
}