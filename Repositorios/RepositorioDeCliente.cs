using ClientesMs.Entidades;
using MongoDB.Driver;

namespace ClientesMs.Repositorios
{
    public class RepositorioDeCliente
    {
        private readonly IMongoCollection<Cliente> _collection;

        public RepositorioDeCliente(IConfiguration configurations)
        {
            var mongoClient = new MongoClient(
                configurations.GetConnectionString("MongoDb")
            );
            var nombreDeLaDb = configurations.GetSection("MongoDbNombre").Value;
            var mongoDatabase = mongoClient.GetDatabase(nombreDeLaDb);
            _collection = mongoDatabase.GetCollection<Cliente>("Clientes");
        }

        internal async Task<int> AgregarAsync(Cliente item)
        {
            item.Id = ((int)await _collection.CountDocumentsAsync(_ => true)) + 1;
            await _collection.InsertOneAsync(item);

            return item.Id;
        }

        internal async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var clientes = await _collection.Find(_ => true).ToListAsync();

            return clientes;
        }

        internal async Task Actualizar(Cliente item)
        {
            await _collection.ReplaceOneAsync(item._id, item);
        }
    }
}