using DevIO.Business.Models;
using DioIO.Business.Interface;
using DioIO.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace DioIO.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }
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
            _notifier.Handle(new Notification(message));
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
