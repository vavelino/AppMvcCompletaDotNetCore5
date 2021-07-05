using DevIO.Business.Models;
using DevIO.Data.Context;
using DioIO.Business.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(MyDbContext db) : base(db) { }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await Db.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SupplierId == supplierId);
        }
    }
}
