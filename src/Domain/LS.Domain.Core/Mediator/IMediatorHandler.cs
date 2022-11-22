using FluentValidation.Results;
using LS.Domain.Core.Messages;
using System.Threading.Tasks;

namespace LS.Domain.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
