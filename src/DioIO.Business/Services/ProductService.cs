using DevIO.Business.Models;
using DioIO.Business.Interface;
using DioIO.Business.Validations;
using System;
using System.Threading.Tasks;

namespace DioIO.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;


        public async Task Add(Product product)
        {
            if (!ExeculteValidation(new ProductValidation(), product)) return;
        }

        public async Task Update(Product product)
        {
            if (!ExeculteValidation(new ProductValidation(), product)) return;
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}