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
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(MyDbContext db) : base(db) { }

        public async Task<Supplier> GetSupplierAddressByID(Guid id)
        {
            return await Db.Suppliers
                .AsNoTracking()
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> GetSupplierProductAddressByID(Guid id)
        {
            /*
            var teste = await Db.Suppliers
                .AsNoTracking()
                .Include(s => s.Products)
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == id);
            */
            return await Db.Suppliers
                .AsNoTracking()
                .Include(s=>s.Products)
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}


 