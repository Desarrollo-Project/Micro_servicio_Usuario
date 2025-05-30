using System.Threading.Tasks;
using Domain.Events;

namespace Infrastructure.EventBus.Interfaces;

public interface IUsuarioEventHandler
{
    Task HandleUsuarioRegistradoAsync(UsuarioCreadoEvent evento);
    Task HandleUsuarioConfirmadoAsync(UsuarioConfirmadoEvent evento);

    Task HandleActividadRegistradaAsync(ActividadRegistradaEvent evento);
}