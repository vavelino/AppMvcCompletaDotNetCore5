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
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository,
                                   IMapper mapper)
        {
            _supplierRepository = supplierRepository;
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
            // Não excluir o que já existe

            await _supplierRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<SupplierViewModel> GetSupplierAddress(Guid id)
        {
            return _mapper
                .Map<SupplierViewModel>
                (await _supplierRepository.GetSupplierAddressByID(id));
        }
        private async Task<SupplierViewModel> GetSupplierProductAddress(Guid id)
        {
            return _mapper
                .Map<SupplierViewModel>
                (await _supplierRepository.GetSupplierProductAddressByID(id));
        }
    }
}
