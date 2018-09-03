using System.Threading.Tasks;

namespace FreeskiDb.WebApi.DocumentDb
{
    public interface IDbClient
    {
        Task CreateDocument(object document);

    }
}