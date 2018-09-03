using System.Threading.Tasks;


namespace FreeskiDb.WebApi.DocumentDb
{
    public class DbClient : IDbClient
    {
        // TODO doc client
        private string _documentClient;

        public DbClient()
        {
        }

        public Task CreateDocument(object document)
        {
            return Task.CompletedTask;
        }
    }
}