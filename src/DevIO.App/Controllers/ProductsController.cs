﻿using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Models;
using DioIO.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,
                                  ISupplierRepository supplierRepository,
                                  IMapper mapper
                                  )
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
        //GetProductById
        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(
                _mapper
                .Map<IEnumerable<ProductViewModel>>
                (await _productRepository.GetProductsSuppliers())
                );
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var productViewModel = await GetProductById(id);

            if (productViewModel == null) return NotFound();


            return View(productViewModel);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ProductViewModel productViewModel = await FillSuppliers(new ProductViewModel());
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {

            productViewModel = await FillSuppliers(productViewModel);

            if (!ModelState.IsValid) return View(productViewModel);

            await _productRepository.Add(_mapper.Map<Product>(productViewModel));

            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProductById(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(productViewModel);
            await _productRepository.Update(
                _mapper
                .Map<Product>
                (productViewModel)
                );
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProductById(id);
            if (product == null) return NotFound();

            await _productRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProductViewModel> GetProductById(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await
                _productRepository.GetProductSupplier(id));
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>
                (await _supplierRepository.GetAll());
            return product;
        }
        private async Task<ProductViewModel> FillSuppliers(ProductViewModel product)
        {
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>
                (await _supplierRepository.GetAll());
            return product;
        }
    }
}