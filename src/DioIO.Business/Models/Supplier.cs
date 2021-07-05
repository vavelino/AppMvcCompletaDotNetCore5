using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Supplier : Entity
    {
       // [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
       // [StringLength(100, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

       // [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
       // [StringLength(14, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 11)]
        [Display(Name = "Documento")]
        public string Document { get; set; }
        [Display(Name = "Tipo do Fornecedor")]
        public SupplierType SupplierType { get; set; }
        [Display(Name = "Endereço")]
        public Address Address { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        /* EF Relations */
        public IEnumerable<Product> Products { get; set; }

    }
}
