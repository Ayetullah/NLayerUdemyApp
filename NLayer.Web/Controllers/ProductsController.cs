﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.Dtos.Categories;
using NLayer.Core.Dtos.Products;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var customResponse = await _productService.GetProductsWithCategory();
            return View(customResponse.Data);
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(_mapper.Map<Product>(product));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);
            return View(_mapper.Map<ProductUpdateDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(_mapper.Map<Product>(product));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}