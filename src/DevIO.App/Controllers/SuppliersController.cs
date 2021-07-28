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

    public class SuppliersController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository,
                                   IAddressRepository addressRepository,
                                   IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }


        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            return View(_mapper
                .Map<IEnumerable<SupplierViewModel>>
                (await _supplierRepository.GetAll())
                );
        }

        // GET: Suppliers/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper
                 .Map<Supplier>(supplierViewModel);
            await _supplierRepository.Add(supplier);

            return RedirectToAction("Index");
            //  return RedirectToAction(nameof(Index)); // Não precisa escrever Strings
        }

        // GET: Suppliers/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper
                 .Map<Supplier>(supplierViewModel);

            await _supplierRepository.Update(supplier);

            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);


            if (supplierViewModel == null) return NotFound();


            return View(supplierViewModel);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);

            if (supplierViewModel == null) return NotFound();
            // Não excluir o que já não existe

            await _supplierRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }
         
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null) return NotFound();
            return PartialView("_DetailsAddress", supplier);
        }

        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null) return NotFound();

            return PartialView("_UpdateAddress",
                new SupplierViewModel { Address = supplier.Address }
                );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplierViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");
            if (!ModelState.IsValid) return PartialView("_UpdateAddress",
                supplierViewModel);

            await _addressRepository.Update(
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

        /*
        IEnumerable<SupplierViewModel> GenerateIEnumerable(IEnumerable<Supplier> suplliers)
        {

            foreach (var item in suplliers)

            {
                SupplierViewModel supplierViewModel = new SupplierViewModel();

                supplierViewModel.Id = item.Id;
                supplierViewModel.Name = item.Name;
                supplierViewModel.Document = item.Document;
                supplierViewModel.SupplierType = 1;
                supplierViewModel.Active = item.Active;
                //supplierViewModel.Products



                yield return supplierViewModel;
            }
        }
        */
        private async Task<SupplierViewModel> GetSupplierProductAddress(Guid id)
        {
            //var teste = await _supplierRepository.GetSupplierProductAddressByID(id);
            // SupplierViewModel ale = new SupplierViewModel();




            // var aaa = 3;
            var SupplierProductAddress = _mapper
                .Map<SupplierViewModel>
                (await _supplierRepository.GetSupplierProductAddressByID(id));
            return SupplierProductAddress;
        }
    }
}
