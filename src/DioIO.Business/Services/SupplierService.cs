using DevIO.Business.Models;
using DioIO.Business.Interface;
using DioIO.Business.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DioIO.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository,
                                IAddressRepository addressRepository,
                                INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }
        public async Task Add(Supplier supplier)
        {
            // Validar o estado da entidade
            if (
                (!ExeculteValidation(new SupplierValidation(), supplier)) ||
                (!ExeculteValidation(new AddressValidation(), supplier.Address))
                )
            {
                return;
            }
            //Verifica se já existe o documento cadastrado
            if (
                _supplierRepository.Search(s => s.Document == supplier.Document)
                .Result.Any()
              )
            {
                Notify("Já existe um fornecedor com este documento informado.");
                return;
            }
            await _supplierRepository.Add(supplier);
        }
        public async Task Update(Supplier supplier)
        {
            if (!ExeculteValidation(new SupplierValidation(), supplier)) return;

            if (_supplierRepository.Search(s => s.Document == supplier.Document && s.Id != supplier.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento infomado.");
                return;
            }
            await _supplierRepository.Update(supplier);
        }
        public async Task UpdateAdress(Address address)
        {
            if (!ExeculteValidation(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }
        public async Task Remove(Guid id)
        {
            if (_supplierRepository.GetSupplierProductAddressByID(id).Result.Products.Any())
            {
                Notify("O fornecedor possui produtos cadastrados!");
                return;
            }
            var endereco = await _addressRepository.GetAddressBySupplier(id);

            if (endereco != null)
            {
                await _addressRepository.Remove(endereco.Id);
            }

            await _supplierRepository.Remove(id);
        }
        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
