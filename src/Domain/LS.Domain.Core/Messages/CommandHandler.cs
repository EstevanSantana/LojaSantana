using FluentValidation.Results;
using LS.Domain.Core.Data;
using System.Threading.Tasks;

namespace LS.Domain.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit()) AdicionarErro("Erro ao tentar salvar no banco.");

            return ValidationResult;
        }
    }
}
