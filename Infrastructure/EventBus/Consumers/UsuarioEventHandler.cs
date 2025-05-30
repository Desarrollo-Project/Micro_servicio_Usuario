using System.Threading.Tasks;
using Domain.Entities;
using Domain.Events;

using Infrastructure.EventBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.EventBus.Consumers;

public class UsuarioEventHandler : IUsuarioEventHandler
{
    private readonly IMongoClient _mongoClient;

    public UsuarioEventHandler(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    private IMongoCollection<UsuarioMongo> Usuarios =>
        _mongoClient.GetDatabase("usuarios_db").GetCollection<UsuarioMongo>("usuarios");

 
    public async Task HandleUsuarioRegistradoAsync(UsuarioCreadoEvent evento)
    {

        Console.WriteLine("👤 Procesando creación de usuario en MongoDB...");

        var usuario = new UsuarioMongo
        {
            Id = evento.Id,
            Nombre = evento.Nombre,
            Apellido = evento.Apellido,
            Username = evento.Username,
            Password = evento.Password,
            Telefono = evento.Telefono,
            Correo = evento.Correo,
            Direccion = evento.Direccion,
        };
        Console.WriteLine($"➡️ Insertando usuario: {usuario.Id}, {usuario.Nombre}, {usuario.Correo}");
        await Usuarios.InsertOneAsync(usuario);
    }

    public async Task HandleUsuarioConfirmadoAsync(UsuarioConfirmadoEvent evento)
    {
        var filter = Builders<UsuarioMongo>.Filter.Eq(u => u.Id, evento.UsuarioId);
        var update = Builders<UsuarioMongo>.Update.Set(u => u.Verificado, evento.Confirmado);
        await Usuarios.UpdateOneAsync(filter, update);
    }

    
}
