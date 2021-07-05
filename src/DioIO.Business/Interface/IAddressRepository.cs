using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioIO.Business.Interface
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressBySupplier(Guid supplierId);
    }
}
