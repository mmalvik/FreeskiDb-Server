using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FreeskiDb.WebApi.Documents;

namespace FreeskiDb.WebApi.Repository
{
    public interface ISkiRepository
    {
        Task<Ski> GetById(int id);
        Task<IEnumerable<Ski>> List();
        IEnumerable<Ski> List(Expression<Func<Ski, bool>> predicate);
        Task Add(Ski entity);
        void Delete(Ski entity);
        void Edit(Ski entity);
    }
}