using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FreeskiDb.Persistence.Entities;

namespace FreeskiDb.WebApi.Repository
{
    public interface ISkiRepository
    {
        Task<Ski> GetById(string id);
        Task<IEnumerable<Ski>> List();
        IEnumerable<Ski> List(Expression<Func<Ski, bool>> predicate);
        Task Add(Ski entity);
        void Delete(Ski entity);
        void Edit(Ski entity);
    }
}