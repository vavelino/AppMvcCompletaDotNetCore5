using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioIO.Business.Interface
{
    public interface ISupplierService : IDisposable
    {
        Task Add(Supplier supplier);
        Task Update(Supplier supplier);
        Task Remove(Guid id);
        Task UpdateAdress(Address address);
    }
}