using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    public class CurrencyAttribute : ValidationAttribute // Validação Apenas no Servidor
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var Currency = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult("Moeda em formato inválido");
            }
            return ValidationResult.Success;
        }
    }

    public class CurrencyAttributeAdapter : AttributeAdapterBase<CurrencyAttribute> // Comportamento do cliente
    {
        public CurrencyAttributeAdapter(CurrencyAttribute currencyAttribute, IStringLocalizer stringLocalizer) : base(currencyAttribute, stringLocalizer) { }
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-currency",
                GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-number",
                GetErrorMessage(context));
            // Possibilita a Criação de uma validação para um caso especifico
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "Moeda em formato inválido";
        }
    }
    public class CurrencyAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if(attribute is CurrencyAttribute currencyAttribute)
            {
                return new CurrencyAttributeAdapter(currencyAttribute, stringLocalizer);
            }
            return _baseProvider.GetAttributeAdapter(attribute,stringLocalizer);
        }
    }
    //Na document~ção do ASP NET Core esta mais detalhado esse processo
    // Opção mais elegante para validar do lado do cliente 
}
