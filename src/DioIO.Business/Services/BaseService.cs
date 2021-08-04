using DevIO.Business.Models;
using FluentValidation;
using FluentValidation.Results;

namespace DioIO.Business.Services
{
    public abstract class BaseService
    {
        protected void Notify(ValidationResult validationResult) //Coleção de erros
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }
        protected void Notify(string message)
        {
            //Propagar esse erro até a camada de apresentação
        }
        // TV validação , TE entidade generica
        protected bool ExeculteValidation<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE>
            where TE : Entity // Filtrar
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return (true);

            Notify(validator);

            return (false);
        }
    }
}
