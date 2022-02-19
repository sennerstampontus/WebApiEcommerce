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
using eCommerce.Models.SignInUpModels;
using Microsoft.AspNetCore.Authorization;
using eCommerce.Filters;
using eCommerce.Models.UpdateModels;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly SqlContext _context;

        public CustomerController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        [UseAdminKey]
        public async Task<ActionResult<IEnumerable<CustomerOutputModel>>> GetCustomers()
        {
            var _customers = new List<CustomerOutputModel>();
            foreach(var cust in await _context.Customers.Include(x => x.Address).ToListAsync())
            {
                _customers.Add(
                    new CustomerOutputModel(cust.Id, cust.FirstName, cust.LastName, cust.Email, 
                    new AddressOutputModel(cust.Address.StreetName, cust.Address.PostalCode, cust.Address.City, cust.Address.Country)));
            }
            return _customers;
        }

        [HttpGet("{id}")]
        [UseUserKey]
        public async Task<ActionResult<CustomerOutputModel>> GetCustomer(int id)
        {
            var customerEntity = await _context.Customers.Include(x => x.Address).Include(x => x.Contact).FirstOrDefaultAsync(x => x.Id == id);
           

            if (customerEntity == null)
            {
                return NotFound();
            }
            
            if(customerEntity.Contact == null)
                   return 
                    new CustomerOutputModel(customerEntity.Id, customerEntity.FirstName, customerEntity.LastName, customerEntity.Email, 
                    new AddressOutputModel(customerEntity.Address.StreetName, customerEntity.Address.PostalCode, customerEntity.Address.City, customerEntity.Address.Country));
            else
                return
                    new CustomerOutputModel(customerEntity.Id, customerEntity.FirstName, customerEntity.LastName, customerEntity.Email, new ContactOutputModel(customerEntity.Contact.Phone, customerEntity.Contact.PhoneWork, customerEntity.Contact.Organization),
                    new AddressOutputModel(customerEntity.Address.StreetName, customerEntity.Address.PostalCode, customerEntity.Address.City, customerEntity.Address.Country));
        }
    }
}
