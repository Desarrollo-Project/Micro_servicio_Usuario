using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.DataBase;

public class MongoInitializer
{
    private readonly IMongoClient _mongoClient;

    public MongoInitializer(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public void Initialize()
    {
        var database = _mongoClient.GetDatabase("usuarios_db");

        // Configurar colección de usuarios
        InitializeUsuarios(database);

    }

    private void InitializeUsuarios(IMongoDatabase database)
    {
        var collection = database.GetCollection<UsuarioMongo>("usuarios");

        // Índice único para username
        var usuarioIndexKeys = Builders<UsuarioMongo>.IndexKeys
            .Ascending(u => u.Username);

        collection.Indexes.CreateOne(
            new CreateIndexModel<UsuarioMongo>(usuarioIndexKeys,
                new CreateIndexOptions { Unique = true })
        );
    }

}