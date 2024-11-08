using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClientesMs.Entidades
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Id { get; set; }

        public string EncodedKey { get; set; }

        public string Apellidos { get; set; }

        public string Correo { get; set; }               

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public List<Direccion> Direcciones { get; set; }

        public bool EstaActivo { get; set; } = true;

        public Dictionary<string,string> Otros { get; set; }
    }
}