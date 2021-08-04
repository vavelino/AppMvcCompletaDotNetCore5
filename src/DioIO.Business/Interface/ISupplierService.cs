using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioIO.Business.Interface
{
    interface ISupplierService
    {
        Task Add(Supplier supplier);
        Task Update(Supplier supplier);
        Task Remove(Guid id);
        Task UpdateAdress(Address address);
    }
}