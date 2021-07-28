using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Models;
using DioIO.Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DevIO.App.Controllers
{
    [Route("produto")]
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
        [Route("lista")]
        public async Task<IActionResult> Index()
        {
            return View(
               _mapper
             .Map<IEnumerable<ProductViewModel>>
            (await _productRepository.GetProductsSuppliers())
             );
        }

        // GET: Products/Details/5
        [Route("detalhes/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var productViewModel = await GetProductById(id);

            if (productViewModel == null) return NotFound();


            return View(productViewModel);
        }

        // GET: Products/Create
        [Route("novo/{id:guid}")]
        public async Task<IActionResult> Create()
        {
            ProductViewModel productViewModel = await FillSuppliers(new ProductViewModel());
            return View(productViewModel);
        }

        // POST: Products/Create
        [Route("novo/{id:guid}")]
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel.Image = "";

            productViewModel = await FillSuppliers(productViewModel);

            if (!ModelState.IsValid) return View(productViewModel);

            var imgPrefix = Guid.NewGuid() + "_";// Garantir que a imagem nunca vai repetir

            if (!await UploadFile(productViewModel.ImageUpload, imgPrefix))
            {
                return View(productViewModel);
            }
            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;

            await _productRepository.Add(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProductById(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [Route("editar/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {

            if (id != productViewModel.Id) return NotFound();

            var productViewModelUpdate = await GetProductById(id);

            productViewModel.Supplier = productViewModelUpdate.Supplier;

            productViewModel.Image = productViewModelUpdate.Image;

            if (!ModelState.IsValid) return View(productViewModel);


            if (productViewModel.ImageUpload != null)
            {
                var imgPrefix = Guid.NewGuid() + "_";// Garantir que a imagem nunca vai repetir
                if (!await UploadFile(productViewModel.ImageUpload, imgPrefix))
                {
                    return View(productViewModel);
                }
                productViewModelUpdate.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            }

            // Tecnica segura de Edição, Evita que o Usuário Edite Campos 
            // que você não queira

            productViewModelUpdate.Name = productViewModel.Name;
            productViewModelUpdate.Description = productViewModel.Description;
            productViewModelUpdate.Value = productViewModel.Value;
            productViewModelUpdate.Active = productViewModel.Active;

            productViewModelUpdate.Supplier = null; // Correção Erro Gravar ID que já existe

            var productUpdate = _mapper
                .Map<Product>
                (productViewModelUpdate);

            await _productRepository.Update(productUpdate);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Delete/5
        [Route("excluir/{id:guid}")]
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
        private async Task<bool> UploadFile(IFormFile file, string imgPrefix)
        {
            if (file.Length <= 0) return false;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", imgPrefix + file.FileName);
            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome!");
                return false;
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return true;
        }
    }
}
