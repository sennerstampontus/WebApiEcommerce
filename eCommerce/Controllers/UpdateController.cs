using eCommerce.Data;
using eCommerce.Filters;
using eCommerce.Models.Entities;
using eCommerce.Models.UpdateModels;
using eCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UpdateController : ControllerBase
    {

        private readonly SqlContext _context;

        public UpdateController(SqlContext context)
        {
            _context = context;
        }

        [HttpPut("Customer/{id}")]
        [UseUserKey]
        public async Task<IActionResult> UpdateCustomer(int id, UserUpdateModel model)
        {

            var customerEntity = await _context.Customers.FindAsync(id);
            if (id != customerEntity.Id)
            {
                return BadRequest("Customer not found");
            }

            if (!customerEntity.CompareSecurePassword(model.Password))
                return BadRequest("Password is incorrect");
            else
                customerEntity.CreateSecurePassword(model.Password);

            var customer = new CustomerEntity(model.FirstName, model.LastName, model.Email);

            customerEntity.FirstName = model.FirstName;
            customerEntity.LastName = model.LastName;
            customerEntity.Email = model.Email;

            customer.CreateSecurePassword(model.Password);

            _context.Entry(customerEntity).State = EntityState.Modified;

            try
            {
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

            return Ok($"Updates:\n{customerEntity.FirstName}\n {customerEntity.LastName}\n {customerEntity.Email}\n Updated at: {DateTime.Now}");
        }

        [HttpPut("Customer/Contact/{id}")]
        [UseUserKey]
        public async Task<IActionResult> UpdateCustomerContact(int id, ContactUpdateModel model)
        {

            var customerEntity = await _context.Customers.Where(x => x.Id == id).Include(x => x.Contact).FirstOrDefaultAsync();


            if (customerEntity == null)
                return NotFound();

            if (model.Phone == "")
                customerEntity.Contact.Phone = customerEntity.Contact.Phone;
            else
                customerEntity.Contact.Phone = model.Phone;

            if (model.PhoneWork == "")
                customerEntity.Contact.PhoneWork = customerEntity.Contact.PhoneWork;
            else
                customerEntity.Contact.PhoneWork = model.PhoneWork;

            if (model.Organization == "")
                customerEntity.Contact.Organization = customerEntity.Contact.Organization;
            else
                customerEntity.Contact.Organization = model.Organization;

            var contact = new ContactEntity(customerEntity.Contact.Phone, customerEntity.Contact.PhoneWork, customerEntity.Contact.Organization);

            _context.Entry(customerEntity.Contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Ok($"Updates:\nPhone: {contact.Phone}\nPhone Work: {contact.PhoneWork}\nOrganization: {contact.Organization}");
        }


        [HttpPut("Admin/{id}")]
        [UseAdminKey]
        public async Task<IActionResult> UpdateAdmin(int id, UserUpdateModel admin)
        {
            var adminEntity = await _context.Admins.FindAsync(id);
            if (id != adminEntity.Id)
            {
                return BadRequest();
            }
            if (!adminEntity.CompareSecurePassword(admin.Password))
                return BadRequest("Password is incorrect");
            else
                adminEntity.CreateSecurePassword(admin.Password);

            adminEntity.FirstName = admin.FirstName;
            adminEntity.LastName = admin.LastName;
            adminEntity.Email = admin.Email;



            _context.Entry(adminEntity).State = EntityState.Modified;

            try
            {
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

            return Ok($"Updates:\n{adminEntity.FirstName}\n {adminEntity.LastName}\n {adminEntity.Email}\n Updated at: {DateTime.Now}");
        }

        [HttpPut("Product/{articleNumber}")]
        [UseAdminKey]
        public async Task<IActionResult> UpdateProduct(string articleNumber, ProductUpdateModel model)
        {
            var productEntity = await _context.Products.FindAsync(articleNumber);
            if(productEntity == null)
            {
                return NotFound();
            }
            else if (articleNumber != productEntity.ArticleNumber)
            {
                return BadRequest();
            }
            
            if(string.IsNullOrEmpty(model.Name))
                productEntity.Name = productEntity.Name;

            else
                productEntity.Name = model.Name;

            if (string.IsNullOrEmpty(model.Description))
                productEntity.Description = productEntity.Description;

            else
                productEntity.Description = model.Description;

            if (model.Price == 0)
                productEntity.Price = productEntity.Price;

            else
                productEntity.Price = model.Price;

            productEntity.UpdatedDate = DateTime.Now;

            _context.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(articleNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Updates:\n{productEntity.Name}\n {productEntity.Description}\n {productEntity.Price}\n Updated at: {productEntity.UpdatedDate}");
        }

        [HttpPut("Order/{id}")]
        [UseAdminKey]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateModel model)
        {
            var _status = await _context.Statuses.FindAsync(model.StatusId);

            if (_status == null)
            {
                switch (model.StatusId)
                {
                    case 2:
                        _status = new StatusEntity("Processing");
                        _context.Statuses.Add(_status);
                        _context.SaveChangesAsync();
                        break;

                    case 3:
                        _status = new StatusEntity("Delivering");
                        _context.Statuses.Add(_status);
                        _context.SaveChangesAsync();
                        break;

                    case 4:
                        _status = new StatusEntity("Delivered");
                        _context.Statuses.Add(_status);
                        _context.SaveChangesAsync();
                        break;

                }
            }


            var orderEntity = await _context.Orders.Where(x => x.Id == id).Include(x => x.Status).FirstOrDefaultAsync();
           
            
            if (id != orderEntity.Id)
            {
                return BadRequest();
            }

            orderEntity.UpdatedDate = DateTime.Now;
            orderEntity.StatusId = model.StatusId;
            
            if (model.OrderTotal == 0)
                orderEntity.OrderTotal = orderEntity.OrderTotal;

            else
                orderEntity.OrderTotal = model.OrderTotal;

            _context.Entry(orderEntity).State = EntityState.Modified;

            try
            {
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

            return Ok($"Updates:\n{orderEntity.StatusId}\n Updated at: {orderEntity.UpdatedDate}");
        }

        private bool EntityExists(int id)
        {
            throw new NotImplementedException();
        }
        private bool EntityExists(string an)
        {
            throw new NotImplementedException();
        }

    }
}
