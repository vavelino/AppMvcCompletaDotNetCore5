using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Models
{
    public enum SupplierType
    {
        [Display(Name = "Pessoa Física")]
        Individual = 1,
        [Display(Name = "Pessoa Juridica")]
        LegalEntity = 2
    }
}
