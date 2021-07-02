using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Models
{
    public class Address : Entity
    {
        public Guid SupplierId { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        //[StringLength(200, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name="Rua")]
        public string Street { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
       // [StringLength(200, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Número")]
        public string Number { get; set; }
        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        //[StringLength(8, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 8)]
        [Display(Name = "Código Postal")]
        public string ZipCode { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        //[StringLength(100, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        //[StringLength(100, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
       // [StringLength(50, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Estado")]
        public string State { get; set; }

        /* EF Relation */
        public Supplier Supplier { get; set; }
    }
}
