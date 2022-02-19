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
using Microsoft.AspNetCore.Authorization;
using eCommerce.Filters;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet("Orders")]
        [UseAdminKey]
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
                            
                                _line.Add(new OrderLineOutputModel(line.Id, line.ProductArticleNumber, line.ProductName, line.ProductPrice, line.Amount));

                            //else
                            //{
                            //    var _annulledProduct = await _context.AnnulledProducts.Where(x => x.ArticleNumber == line.ProductArticleNumber).FirstOrDefaultAsync();
                            //    _line.Add(new OrderLineOutputModel(line.Id, _annulledProduct.ArticleNumber, _annulledProduct.Name, _annulledProduct.Price, line.Amount));
                            //}
                        }


                         _orders.Add(new OrderOutputModel(order.Id, order.CreatedDate, order.DueDate, order.UpdatedDate, new CustomerOutputModel(order.Customer.Id, order.Customer.FirstName, order.Customer.LastName, order.Customer.Email, new AddressOutputModel(order.Customer.Address.StreetName, order.Customer.Address.PostalCode, order.Customer.Address.City, order.Customer.Address.Country)), _line, order.OrderTotal, new StatusOutputModel(order.Status.Id, order.Status.StatusName)));
                    }
                }

            return _orders;
        }

        [HttpGet("{id}")]
        [UseUserKey]
        public async Task<ActionResult<OrderOutputModel>> GetOrder(int id)
        {
            var orderEntity = await _context.Orders.Where(x => x.Id == id).Include(x => x.Customer).ThenInclude(x => x.Address).Include(x => x.Lines).Include(x => x.Status).FirstOrDefaultAsync();

            List<OrderLineOutputModel> _line = new();

            foreach (var line in orderEntity.Lines)
                _line.Add(new OrderLineOutputModel(line.Id, line.ProductArticleNumber, line.ProductName, line.LinePrice, line.Amount));


            if (orderEntity == null)
            {
                return NotFound();
            }

            return new OrderOutputModel(orderEntity.Id, orderEntity.CreatedDate, orderEntity.DueDate, orderEntity.UpdatedDate, new CustomerOutputModel(orderEntity.Customer.Id, orderEntity.Customer.FirstName, orderEntity.Customer.LastName, orderEntity.Customer.Email, new AddressOutputModel(orderEntity.Customer.Address.StreetName, orderEntity.Customer.Address.PostalCode, orderEntity.Customer.Address.City, orderEntity.Customer.Address.Country)), _line, orderEntity.OrderTotal, new StatusOutputModel(orderEntity.Status.Id, orderEntity.Status.StatusName));
        }


        [HttpPost]
        [UseUserKey]
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

            //Test för att få incremeringen av OrderLines rätt.
            //var _order = await GreateOrderId(_customer, _status);
            //List<OrderLineEntity> line = await GetOrderLines(model, _order);

            decimal totalPrice = 0;

            foreach (var lineEntity in model.Line)
            {

                var _product = await _context.Products.FindAsync(lineEntity.ProductArticleNumber);

                if (_product == null)
                    return NotFound("The desired product could not be found.");


                var linePrice = _product.Price * lineEntity.Amount;

                var _line = new OrderLineEntity(_product.ArticleNumber, _product.Name, _product.Price, lineEntity.Amount, linePrice);

                line.Add(_line);
            }

            foreach ( var _line in line)
                totalPrice += _line.LinePrice;



            //= await _context.Orders.Where(x => x.Id == _order.Id).Include(x => x.Customer).Include(x => x.Lines).Include(x => x.Status).FirstOrDefaultAsync();
            //var orderCreated  = await UpdateNewOrder(orderEntity, _customer, line, totalPrice, _status);

            var orderEntity = new OrderEntity(_customer, line, totalPrice, _status);

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return Ok($"Order Created with Order Id: {orderEntity.Id}");
        }

        //private async Task<List<OrderLineEntity>> GetOrderLines(CreateOrderModel model, OrderEntity orderEntity)
        //{
        //    string notFound;
        //    List<OrderLineEntity> line = new();

        //    foreach (var lineEntity in model.Line)
        //    {


        //        var _product = await _context.Products.FindAsync(lineEntity.ProductArticleNumber);

        //        if (_product == null)
        //        {
        //            notFound = ($"Product was not found with ArticleNumber: {lineEntity.ProductArticleNumber}");
        //        }



        //        var linePrice = _product.Price * lineEntity.Amount;

        //        var _line = new OrderLineEntity(orderEntity.Id, _product.ArticleNumber, _product.Name, _product.Price, lineEntity.Amount, linePrice);

        //        line.Add(_line);

        //        _context.OrdersLine.Add(_line);
        //        _context.Entry(_line).State = EntityState.Detached;
        //        await _context.SaveChangesAsync();
        //    }

        //    return line;
        //}

        //private async Task<string> UpdateNewOrder(OrderEntity orderEntity, CustomerEntity customer, List<OrderLineEntity> line, decimal totalPrice, StatusEntity status)
        //{


        //    orderEntity.Customer = customer;
        //    orderEntity.Lines = line;
        //    orderEntity.OrderTotal = totalPrice;
        //    orderEntity.Status = status;

        //    _context.Entry(orderEntity).State = EntityState.Detached;
        //    await _context.SaveChangesAsync();

        //   return "Order Created";
        //}

        //private async Task<OrderEntity> GreateOrderId(CustomerEntity customer, StatusEntity status)
        //{
        //    var orderEntity = new OrderEntity(customer, status);
        //    _context.Orders.Add(orderEntity);
        //    await _context.SaveChangesAsync();

        //    return orderEntity;
        //}
    }
}
