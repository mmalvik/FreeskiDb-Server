using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace FreeskiDb.WebApi.CosmosDb
{
    public interface ICosmosClient
    {
        Task CreateDocument(Uri documentCollectionUri, object document);

        Task<IEnumerable<T>> ExecuteQuery<T>(Uri documentCollectionUri, string query);

        Task<IEnumerable<T>> ExecuteQuery<T>(Uri documentCollectionUri, SqlQuerySpec query);
    }
}