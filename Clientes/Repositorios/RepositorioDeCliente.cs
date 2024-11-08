using ClientesMs.Entidades;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ClientesMs.Repositorios
{
    /// <summary>
    /// Repositorio de clientes
    /// </summary>
    public class RepositorioDeCliente
    {
        private readonly IMongoCollection<Cliente> _collection;

        /// <summary>
        /// Configuracion de la mongodb en el constructor
        /// </summary>
        /// <param name="configurations"></param>
        public RepositorioDeCliente(IConfiguration configurations)
        {
            var conectionString = configurations.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(conectionString);
            var nombreDeLaDb = conectionString.Split("/").Last().Split("?").First();
            var mongoDatabase = mongoClient.GetDatabase(nombreDeLaDb);
            _collection = mongoDatabase.GetCollection<Cliente>("Clientes");
        }

        internal async Task<string> AgregarAsync(Cliente item)
        {
            item.Id = (((int)await _collection.CountDocumentsAsync(_ => true)) + 1).ToString();
            await _collection.InsertOneAsync(item);

            return item.Id;
        }

        internal async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var clientes = await _collection.Find(_ => true).ToListAsync();

            return clientes;
        }

        internal async Task ActualizarAsync(Cliente item) =>
            await _collection.ReplaceOneAsync(x => x._id == item._id, item);

        internal async Task<Cliente> ObtenerPorIdAsync(string id)
        {
            return (await _collection.FindAsync(
            new BsonDocument("$or", new BsonArray
            {
                new BsonDocument("Id", id),
                new BsonDocument("EncodedKey",id)
            }))).FirstOrDefault();
        }

        internal async Task<Cliente> ObtenerPorCorreoAsync(string correo)
        {
            return (await _collection.FindAsync(x => x.Correo == correo)).FirstOrDefault();
        }
    }
}