using DevIO.App.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevIO.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Fornecedor")]
        public Guid SupplierId { get; set; }


        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Imagem do Produto")]
        public IFormFile ImageUpload { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Imagem")]
        public string Image { get; set; }

        [Currency] // Funciona no Serve Side
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Column(TypeName = "decimal(10,4)")]
        [Display(Name = "Valor")]
        public decimal Value { get; set; }

        [Display(Name = "Data de Registro")]
        [ScaffoldColumn(false)] // Na hora de fazer o scaffold desconsiderar essa coluna
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Ativo?")]
        public bool Active { get; set; }

        public SupplierViewModel Supplier { get; set; }

        public IEnumerable<SupplierViewModel> Suppliers { get; set; }
    }
}
