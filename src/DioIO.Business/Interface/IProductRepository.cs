using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioIO.Business.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProducstBySupplier(Guid supplierId);
        Task<IEnumerable<Product>> GetProductsSuppliers();
        Task<Product> GetProductSupplier(Guid id); // Retornando um produto e o fornecedor dele
    }
}

