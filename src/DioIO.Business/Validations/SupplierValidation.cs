using DevIO.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DioIO.Business.Validations.Docs.DocsValidation;

namespace DioIO.Business.Validations
{
    class SupplierValidation: AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres}");

            When(s => s.SupplierType == SupplierType.Individual, () =>
            {
                RuleFor(s => s.Document.Length)
                .Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(s => CpfValidacao.Validar(s.Document)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

            When(s => s.SupplierType == SupplierType.LegalEntity, () =>
            {
                RuleFor(s => s.Document.Length).Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(s => CnpjValidacao.Validar(s.Document)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

        }
    }
}
