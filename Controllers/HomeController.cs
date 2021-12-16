using Microsoft.EntityFrameworkCore;
using ProductsNCategories.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Collections.Generic;
// Other using statements
namespace ProductsNCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }
     
        [HttpGet]
        [Route("")]
        public IActionResult dashboard()
        {
            
            return View();
        }
        [HttpPost("addProduct")]
        public IActionResult NewProduct(Product fromForm)
        {      
                if(ModelState.IsValid)
                {
                    _context.Add(fromForm);
                    _context.SaveChanges();
                    return RedirectToAction("dashboard", new { productId = fromForm.ProductId});

                    // return RedirectToAction("dishes", new { dishId = fromForm.DishId});        
                }
                else
                    {
                        return products();
                    }
        }
        [HttpGet]
        [Route("products")]
        public IActionResult products()
        {
            Product theProds = new Product()
            {
                listofProds= _context.Products.ToList()
            };
            return View(theProds);
        }
        [HttpGet("products/{productId}")]
        public IActionResult addCat(int productId)
        {
            // Product toRender = _context.Products;
            //I want to query for a single product
            // with the category information
            //and the categories they know

            ProductView ViewModel = new ProductView()
            {
                ToRender = _context.Products
                    .Include(p => p.CatOwned)
                    .ThenInclude( a => a.Category2)
                    .FirstOrDefault(p => p.ProductId == productId),
                ToAdd = _context.Categories
                    .Include(c => c.listOfProds)
                    .Where(a => !a.listOfProds.Any(cat => cat.ProductId == productId))
                    .ToList()

            };
            return View("addCat", ViewModel);
            
        }
        [HttpPost("addCategory")]
        public IActionResult NewCategory(Category fromForm)
        {      
                if(ModelState.IsValid)
                {
                    _context.Add(fromForm);
                    _context.SaveChanges();
                    return RedirectToAction("dashboard");
        
                }
                else
                    {
                        return categories();
                    }
        }
        [HttpGet]
        [Route("categories")]
        public IActionResult categories()
        {
            Category theCats = new Category()
            {
                listofCats= _context.Categories.ToList()
            };
            return View(theCats);
        }
        [HttpPost("product/{productId}/add")]
        public IActionResult updateProduct(int productId, ProductView viewModel)
        {
            if(ModelState.IsValid)
            {
                Association fromForm = viewModel.AddForm;
                _context.Add(fromForm);
                _context.SaveChanges();

                return RedirectToAction("products", new {productId = productId});
            }
            else
            {
                return addCat(productId);
            }
        }
        [HttpGet("categories/{categoryId}")]
        public IActionResult addProd(int categoryId)
        {
            // {
            //     ToRender = _context.Products
            //         .Include(p => p.CatOwned)
            //         .ThenInclude( a => a.Category2)
            //         .FirstOrDefault(p => p.ProductId == productId),
            //     ToAdd = _context.Categories
            //         .Include(c => c.listOfProds)
            //         .Where(a => !a.listOfProds.Any(cat => cat.ProductId == productId))
            //         .ToList()

            // };

            CategoryView ViewModel = new CategoryView()
            {
                ToRender = _context.Categories
                    .Include(p => p.listOfProds)
                    .ThenInclude( a => a.ProductWithCat)
                    .FirstOrDefault(p => p.CategoryId == categoryId),
                ToAdd = _context.Products
                    .Include(c => c.CatOwned)
                    .Where(a => !a.CatOwned.Any(cat => cat.CategoryId == categoryId))
                    .ToList()
            };
            return View("addProd", ViewModel);
            
        }
        [HttpPost("category/{categoryId}/add")]
        public IActionResult updateCategory(int categoryId, ProductView viewModel)
        {
            if(ModelState.IsValid)
            {
                Association fromForm = viewModel.AddForm;
                _context.Add(fromForm);
                _context.SaveChanges();

                return RedirectToAction("categories", new {CategoryId = categoryId});
            }
            else
            {
                return addProd(categoryId);
            }
        }
    }
}