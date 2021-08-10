using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Models;
using DioIO.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.App.Controllers
{
    [Route("fornecedore")]
    public class SuppliersController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;
        //private readonly IAddressRepository _addressRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository,
                                   ISupplierService supplierService,
                                   IMapper mapper,
                                   INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
           // _addressRepository = addressRepository;
            _mapper = mapper;
        }

        // GET: Suppliers
        [Route("lista")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper
                .Map<IEnumerable<SupplierViewModel>>
                (await _supplierRepository.GetAll())
                );
        }

        // GET: Suppliers/Details/5
        [Route("detalhes/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }
            return View(supplierViewModel);
        }

        // GET: Suppliers/Create
        [Route("novo")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [Route("novo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper
                 .Map<Supplier>(supplierViewModel);
            //await _supplierRepository.Add(supplier); 
            await _supplierService.Add(supplier);

            return RedirectToAction("Index");
            //  return RedirectToAction(nameof(Index)); // Não precisa escrever Strings
        }

        // GET: Suppliers/Edit/5
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var supplierViewModel = await GetSupplierProductAddress(id);
            if (supplierViewModel == null)
            {
                return NotFound();
            }
            return View(supplierViewModel);
        }

        // POST: Suppliers/Edit/5
        [Route("editar/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper
                 .Map<Supplier>(supplierViewModel);

            //await _supplierRepository.Update(supplier);

            await _supplierService.Update(supplier);            

            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Delete/5
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);


            if (supplierViewModel == null) return NotFound();


            return View(supplierViewModel);
        }

        // POST: Suppliers/Delete/5
        [Route("excluir/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);

            if (supplierViewModel == null) return NotFound();
            // Não excluir o que já não existe

            // await _supplierRepository.Remove(id);
            await _supplierService.Remove(id);            

            return RedirectToAction(nameof(Index));
        }
        [Route("obter-endereco/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null) return NotFound();
            return PartialView("_DetailsAddress", supplier);
        }
        [Route("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null) return NotFound();

            return PartialView("_UpdateAddress",
                new SupplierViewModel { Address = supplier.Address }
                );
        }
        [Route("atualizar-endereco/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplierViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");
            if (!ModelState.IsValid) return PartialView("_UpdateAddress",
                supplierViewModel);

           // await _addressRepository.Update(
            //    _mapper.Map<Address>(supplierViewModel.Address));


            await _supplierService.UpdateAdress(
                        _mapper.Map<Address>(supplierViewModel.Address));
            

            var url = Url.Action("GetAddress", "Suppliers",
                new { id = supplierViewModel.Address.SupplierId });

            return Json(new { success = true, url });
        }    

        private async Task<SupplierViewModel> GetSupplierAddress(Guid id)
        {
            return _mapper
                .Map<SupplierViewModel>
                (await _supplierRepository.GetSupplierAddressByID(id));
        }      
        private async Task<SupplierViewModel> GetSupplierProductAddress(Guid id)
        {        
            var SupplierProductAddress = _mapper
                .Map<SupplierViewModel>
                (await _supplierRepository.GetSupplierProductAddressByID(id));
            return SupplierProductAddress;
        }
    }
}
