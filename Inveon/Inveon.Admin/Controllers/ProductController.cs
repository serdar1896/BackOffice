using AutoMapper;
using Inveon.Admin.Filters;
using Inveon.Core.Interfaces.Services;
using Inveon.Core.Models;
using Inveon.Core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inveon.Core.Models.Entities;

namespace Inveon.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;
        [Obsolete]
        private readonly IHostingEnvironment webHostEnvironment;

        [Obsolete]
        public ProductController(IProductService productService, IMapper mapper, IHostingEnvironment webHostEnvironment)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var productList = await productService.GetAllAsync();
            var productDtoList = mapper.Map<IEnumerable<ProductDto>>(productList);
            return View(productDtoList.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            await productService.AddAsync(product);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Edit(string id)
        {
            var product = await productService.GetByIdAsync(id);
            var productDto = mapper.Map<ProductDto>(product);
            return View(productDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Edit(string id, ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            await productService.UpdateAsync(id, product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var product = await productService.GetByIdAsync(id);
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> DeleteConfirm(string id)
        {          
                await productService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
           
        }

        [AllowAnonymous]
        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Details(string id)
        {            
            var product = await productService.GetByIdAsync(id);
            var productDto = mapper.Map<ProductDto>(product);            
            return View(productDto);
        }
    }
}