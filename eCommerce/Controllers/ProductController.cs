#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data;
using eCommerce.Models.Entities;
using eCommerce.Filters;
using eCommerce.Models.ProductModels;
using eCommerce.Models.CategoryModels;
using eCommerce.Models.Entities.AnnulledEntities;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet("Products")]       
        [UseUserKey]

        public async Task<ActionResult<IEnumerable<ProductOutputModel>>> GetProducts()
        {
            var _products = new List<ProductOutputModel>();
            foreach (var product in await _context.Products.Include(x => x.Category).ToListAsync())
            {
                _products.Add(new ProductOutputModel(product.ArticleNumber, product.Name, product.Description, product.Price, new CategoryModel(product.Category.Name)));
            }
            return _products;
        }


        [HttpGet("{articleNumber}")]
        [UseUserKey]
        public async Task<ActionResult<ProductOutputModel>> GetProduct(string articleNumber)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ArticleNumber == articleNumber);

            if (productEntity == null)
            {
                return NotFound();
            }

            return 
                new ProductOutputModel(productEntity.ArticleNumber, productEntity.Name, productEntity.Description, productEntity.Price, 
                new CategoryModel(productEntity.Category.Name));
        }


        [HttpPost]
        [UseAdminKey]
        public async Task<ActionResult<ProductOutputModel>> CreateProduct(CreateProductModel model)
        {
            if(await _context.Products.AnyAsync(x => x.Name == model.ProductName))
                return BadRequest("Product with that name already exists");



            var productEntity = new ProductEntity(model.ProductName, model.ProductDescription, model.Price);

            var categoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.Name == model.CategoryName);

            if (categoryEntity != null)
                productEntity.CategoryId = categoryEntity.Id;
            else
                productEntity.Category = new CategoryEntity(model.CategoryName);


            
           _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return new ProductOutputModel(productEntity.ArticleNumber, productEntity.Name, productEntity.Description, productEntity.Price, new CategoryModel(productEntity.Category.Name));
        }

 
        

        private bool ProductEntityExists(string id)
        {
            return _context.Products.Any(e => e.ArticleNumber == id);
        }
    }
}
