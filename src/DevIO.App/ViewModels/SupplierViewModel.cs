using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Nome")]
        public string Name { get; set; }


        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 11)]
        [Display(Name = "Documento")]
        public string Document { get; set; }

       
        [Display(Name = "Tipo do Fornecedor")]
        public int SupplierType { get; set; }


        [Display(Name = "Endereço")]
        public AddressViewModel Address { get; set; }


        [DisplayName("Ativo?")]
        public bool Active { get; set; }


        public IEnumerable<AddressViewModel> Products { get; set; }

    }
}
