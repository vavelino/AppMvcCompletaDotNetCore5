using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Models
{
    public class Product : Entity
    {
        public Guid SupplierId { get; set; }

      //  [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
       // [StringLength(200, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        //[StringLength(1000, ErrorMessage = "O campo {0} Precisa ter entre {2} e {1} Caracteres", MinimumLength = 2)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Display(Name = "Imagem")]
        public string Image { get; set; }

        //[Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [Column(TypeName = "decimal(10,4)")]
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Data de Registro")]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Ativo")]
        public bool Active { get; set; }

        /* EF Relations */
        public Supplier Supplier { get; set; }
    }
}
