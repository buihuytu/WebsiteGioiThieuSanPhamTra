﻿using DoGiaDung.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebBanTra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly WebbantraContext _context;
        public ProductsController(WebbantraContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                int countTrash = _context.TblProducts.Where(m => m.IsDelete == 1).Count();
                var list = from p in _context.TblProducts
                           join c in _context.TblCategories
                           on p.CateId equals c.Id
                           where p.IsDelete == 0
                           where p.CateId == c.Id
                           orderby p.CreatedDate descending
                           select new ProductCategory()
                           {
                               ProductId = p.Id,
                               ProductName = p.Name,
                               Slug = p.Slug,
                               ProductImg = p.Image,
                               CategoryName = c.Name,
                               ProductPrice = p.Price,
                               IsActive = p.IsActive
                           };
                await list.ToListAsync();
                return Ok(new { countTrash, list });
            }
            catch (HttpRequestException e)
                when (e.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                // Handle Bad Request
                return BadRequest();
            }
            catch (HttpRequestException e)  
                when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Handle Bad Not Found
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetProductPage/{page}")]
        public async Task<IActionResult> GetAllProductsByPage(int page)
        {
            int countTrash = _context.TblProducts.Where(m => m.IsDelete == 1).Count();
            var list = from p in _context.TblProducts
                       join c in _context.TblCategories
                       on p.CateId equals c.Id
                       where p.IsDelete == 0
                       where p.CateId == c.Id
                       orderby p.CreatedDate descending
                       select new ProductCategory()
                       {
                           ProductId = p.Id,
                           ProductName = p.Name,
                           Slug = p.Slug,
                           ProductImg = p.Image,
                           CategoryName = c.Name,
                           ProductPrice = p.Price,
                           IsActive = p.IsActive
                       };
            int countProduct = list.Count();
            list = list.Skip((page - 1) * 10).Take(10);
            await list.ToListAsync();
            return Ok(new { countTrash, countProduct, list });
        }

        [HttpGet]
        [Route("Trash")]
        public async Task<IActionResult> GetTrash()
        {
            var list = from p in _context.TblProducts
                       join c in _context.TblCategories
                       on p.CateId equals c.Id
                       where p.IsDelete == 1
                       where p.CateId == c.Id
                       orderby p.CreatedDate descending
                       select new ProductCategory()
                       {
                           ProductId = p.Id,
                           ProductName = p.Name,
                           Slug = p.Slug,
                           ProductImg = p.Image,
                           CategoryName = c.Name,
                           ProductPrice = p.Price,
                           IsActive = p.IsActive
                       };
            await list.ToListAsync();
            return Ok(list);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.TblProducts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return BadRequest($"Không tồn tại Sản phẩm có Id = {id}");
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductByName/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var listProduct = from p in _context.TblProducts
                              join c in _context.TblCategories
                              on p.CateId equals c.Id
                              where p.IsDelete == 0
                              orderby p.CreatedDate descending
                              select new ProductCategory()
                              {
                                  ProductId = p.Id,
                                  ProductName = p.Name,
                                  Slug = p.Slug,
                                  ProductImg = p.Image,
                                  CategoryName = c.Name,
                                  ProductPrice = p.Price,
                                  IsActive = p.IsActive
                              };
            await listProduct.ToListAsync();
            var list = await listProduct.Where(p => p.ProductName.Contains(name)).ToListAsync();
            return Ok(list);
        }

        [HttpGet]
        [Route("GetProductByPriceRange/{page}/{min}/{max}")]
        public async Task<IActionResult> GetProductByPriceRange(double min, double max, int page)
        {
            var listProduct = from p in _context.TblProducts
                              join c in _context.TblCategories
                              on p.CateId equals c.Id
                              where p.IsDelete == 0
                              where p.CateId == c.Id && p.Price >= min && p.Price <= max
                              orderby p.CreatedDate descending
                              select new ProductCategory()
                              {
                                  ProductId = p.Id,
                                  ProductName = p.Name,
                                  Slug = p.Slug,
                                  ProductImg = p.Image,
                                  CategoryName = c.Name,
                                  ProductPrice = p.Price,
                                  IsActive = p.IsActive
                              };
            int countProduct = listProduct.Count();
            listProduct = listProduct.Skip((page - 1) * 9).Take(9);
            await listProduct.ToListAsync();
            return Ok(new { countProduct, listProduct });
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.TblProducts.FindAsync(id);
            if (product == null)
            {
                return BadRequest();
            }
            string fileName = product.Image;
            if (fileName != null)
            {
                System.IO.File.Delete("D:\\Ki_2-Nam_3\\ThucTapChuyenNganh\\WebBanTra\\WebBanTra\\Public\\Admin\\Pictures\\products\\" + fileName);
            }
            var listImage = _context.TblProductImages.Where(pi => pi.IdProduct == id).ToList();
            foreach (var img in listImage)
            {
                System.IO.File.Delete("D:\\Ki_2-Nam_3\\ThucTapChuyenNganh\\WebBanTra\\WebBanTra\\Public\\Admin\\Pictures\\product-images\\" + img.Name);
            }
            _context.TblProducts.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProduct([FromForm] ProductImage p)
        {
            if(ModelState.IsValid)
            {
                String strSlug = XString.ToAscii(p.Name);
                var product = new TblProduct()
                {
                    Name = p.Name,
                    Slug = strSlug,
                    CateId = p.CateId,
                    Mass = p.Mass,
                    Price = p.Price,
                    ProPrice = p.ProPrice,
                    Description = p.Description,
                    Detail = p.Detail,
                    MetaTitle = p.MetaTitle,
                    MetaKey = p.MetaKey,
                    MetaDesc = p.MetaDesc,
                    CreatedDate = DateTime.Now,
                    CreatedBy = p.CreatedBy,
                    UpdatedDate = null,
                    UpdatedBy = null,
                    IsDelete = 0,
                    IsActive = p.IsActive
                };
                if(p.FileImage != null)
                {
                    String fileName = strSlug + p.FileImage.FileName.Substring(p.FileImage.FileName.LastIndexOf('.'));
                    var path = Path.Combine("D:\\Ki_2-Nam_3\\ThucTapChuyenNganh\\WebBanTra\\WebBanTra\\Public\\Admin\\Pictures\\products", fileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await p.FileImage.CopyToAsync(stream);
                    }
                    product.Image = fileName;
                }
                else
                {
                    product.Image = null;
                }
                _context.TblProducts.Add(product);
                await _context.SaveChangesAsync();
                return Ok(product.Id);
            }
            return BadRequest();
        }

        [HttpPut("id")]
        public async Task<IActionResult> EditProduct(int ID, [FromForm] ProductImage p)
        {
            if (ModelState.IsValid)
            {
                String strSlug = XString.ToAscii(p.Name);
                var product = new TblProduct()
                {
                    Id = ID,
                    Name = p.Name,
                    Slug = strSlug,
                    CateId = p.CateId,
                    Mass = p.Mass,
                    Price = p.Price,
                    ProPrice = p.ProPrice,
                    Description = p.Description,
                    Detail = p.Detail,
                    MetaTitle = p.MetaTitle,
                    MetaKey = p.MetaKey,
                    MetaDesc = p.MetaDesc,
                    CreatedDate = (from c in _context.TblProducts where c.Id == ID select c.CreatedDate).FirstOrDefault(),
                    CreatedBy = (from c in _context.TblProducts where c.Id == ID select c.CreatedBy).FirstOrDefault(),
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = p.UpdatedBy,
                    IsDelete = 0,
                    IsActive = p.IsActive
                };
                if (p.FileImage != null)
                {
                    String fileName = strSlug + p.FileImage.FileName.Substring(p.FileImage.FileName.LastIndexOf('.'));
                    var path = Path.Combine("D:\\Ki_2-Nam_3\\ThucTapChuyenNganh\\WebBanTra\\WebBanTra\\Public\\Admin\\Pictures\\products", fileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await p.FileImage.CopyToAsync(stream);
                    }
                    product.Image = fileName;
                }
                else
                {
                    product.Image = (from c in _context.TblProducts where c.Id == ID select c.Image).FirstOrDefault();
                }
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Edit Successfully");
            }
            return BadRequest();
        }
    }
}
