using eCommerce.Data;
using eCommerce.Models.Entities.AnnulledEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {

        private readonly SqlContext _context;

        public DeleteController(SqlContext context)
        {
            _context = context;
        }

        [HttpDelete("Customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerEntity = await _context.Customers.FindAsync(id);
            if (customerEntity == null)
            {
                return NotFound();
            }

            customerEntity.FirstName = "Deleted";
            customerEntity.LastName = "Deleted";
            customerEntity.Email = "Deleted";


            try
            {
                _context.Entry(customerEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("Admin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var adminEntity = await _context.Admins.FindAsync(id);
            if (adminEntity == null)
            {
                return NotFound();
            }

            try
            {
                _context.Admins.Remove(adminEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    


        [HttpDelete("Product/{articleNumber}")]
        //[UseAdminKey]
        public async Task<IActionResult> DeleteProduct(string articleNumber)
        {
            var productEntity = await _context.Products.FindAsync(articleNumber);
            if (productEntity == null)
            {
                return NotFound();
            }

            var _annulledProduct = new AnnulledProductEntity(productEntity.ArticleNumber, productEntity.Name, productEntity.Description, productEntity.Price);
            _context.AnnulledProducts.Add(_annulledProduct);

            try
            {
                _context.Products.Remove(productEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(articleNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        [HttpDelete("Order/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            var _annulledOrder = new AnnulledOrderEntity(orderEntity.Id, orderEntity.CustomerId, orderEntity.CreatedDate, DateTime.Now, orderEntity.OrderTotal);
            
            _context.AnnulledOrders.Add(_annulledOrder);
            await _context.SaveChangesAsync();

            

            try
            {
                _context.Orders.Remove(orderEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool EntityExists(int id)
        {
            throw new NotImplementedException();
        }
        private bool ProductEntityExists(string an)
        {
            throw new NotImplementedException();
        }
    }
}
