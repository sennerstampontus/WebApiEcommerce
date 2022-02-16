#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data;
using eCommerce.Models.Entities;
using eCommerce.Models.ViewModels;
using eCommerce.OrderModels;
using eCommerce.Models.ProductModels;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderOutputModel>>> GetOrders()
        {
            var _orders = new List<OrderOutputModel>();

  
                foreach (var order in await _context.Orders.Include(x => x.Lines).Include(x => x.Customer).ThenInclude(x => x.Address).Include(x => x.Status).ToListAsync())
                {
                    if(order.Customer != null)
                    {
                         var _line = new List<OrderLineOutputModel>();

                        foreach (var line in order.Lines.Where(x => x.OrderId == order.Id))
                        {

                            var _product = await _context.Products.Where(x => x.ArticleNumber == line.ProductArticleNumber).FirstOrDefaultAsync();

                            if (_product != null)
                                _line.Add(new OrderLineOutputModel(line.Id, _product.ArticleNumber, _product.Name, _product.Price, line.Amount));

                            else
                            {
                                var _annulledProduct = await _context.AnnulledProducts.Where(x => x.ArticleNumber == line.ProductArticleNumber).FirstOrDefaultAsync();
                                _line.Add(new OrderLineOutputModel(line.Id, _annulledProduct.ArticleNumber, _annulledProduct.Name, _annulledProduct.Price, line.Amount));
                            }
                        }


                         _orders.Add(new OrderOutputModel(order.Id, order.CreatedDate, order.DueDate, order.UpdatedDate, new CustomerOutputModel(order.Customer.Id, order.Customer.FirstName, order.Customer.LastName, order.Customer.Email, new AddressOutputModel(order.Customer.Address.StreetName, order.Customer.Address.PostalCode, order.Customer.Address.City, order.Customer.Address.Country)), _line, order.OrderTotal, new StatusOutputModel(order.Status.Id, order.Status.StatusName)));
                    }
                }

            return _orders;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderEntity>> GetOrder(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);

            if (orderEntity == null)
            {
                return NotFound();
            }

            return orderEntity;
        }



        [HttpPost]
        public async Task<ActionResult<OrderOutputModel>> CreateOrder(CreateOrderModel model)
        {
            List<OrderLineEntity> line = new();

            var _customer = await _context.Customers.Where(x => x.Id == model.CustomerId).Include(x => x.Address).FirstOrDefaultAsync();
            var _status = await _context.Statuses.FindAsync(model.StatusId);
            if (_status == null)
            {
                _status = new StatusEntity("Waiting");
                _context.Statuses.Add(_status);
            }
            
                    
         
            decimal totalPrice = 0;
 
            foreach(var lineEntity in model.Line)
            {
                
                var _product = await _context.Products.FindAsync(lineEntity.ProductArticleNumber);
                var linePrice = _product.Price * lineEntity.Amount;


                line.Add(new OrderLineEntity(_product.ArticleNumber, _product.Name, _product.Price, lineEntity.Amount, linePrice));
            }

            foreach( var _line in line)
            {
                totalPrice += _line.LinePrice;
            }
         
            

            var orderEntity = new OrderEntity(_customer, line, totalPrice, _status);
    
            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return Ok("Order Created!");
        }
        private bool OrderEntityExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
