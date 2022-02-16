using eCommerce.Data;
using eCommerce.Models.Entities;
using eCommerce.Models.SignInUpModels;
using eCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SqlContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(SqlContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }




        [HttpPost("SignUpCustomer")]
        public async Task<ActionResult> SignUpCustomer(CustomerSignUpModel model)
        {
            if (await _context.Customers.AnyAsync(x => x.Email == model.Email))
                return BadRequest();

            var customerEntity = new CustomerEntity(model.FirstName, model.LastName, model.Email, new ContactEntity(model.Phone, model.PhoneWork, model.Organization));
            customerEntity.CreateSecurePassword(model.Password);

            var address = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == model.StreetName && x.PostalCode == model.PostalCode);
            if (address != null)
                customerEntity.AddressId = address.Id;
            else
                customerEntity.Address = new AddressEntity(model.StreetName, model.PostalCode, model.City, model.Country);

            

            _context.Customers.Add(customerEntity);
            await _context.SaveChangesAsync();


            return Ok();


            //return CreatedAtAction("GetCustomer", new { id = customerEntity.Id },
            //    new CustomerOutputModel(
            //    customerEntity.Id, customerEntity.FirstName, customerEntity.LastName, customerEntity.Email,
            //    new AddressModel(customerEntity.Address.StreetName, customerEntity.Address.PostalCode, customerEntity.Address.City, customerEntity.Address.Country)));
        }

        [HttpPost("SignInCustomer")]
        public async Task<ActionResult> SignInCustomer(SignInModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Email and password can not be empty");

            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (customer == null || !customer.CompareSecurePassword(model.Password))
                return BadRequest("Email or password is not correct");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.Id.ToString()),
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim("valid", _configuration.GetValue<string>("ApiCustomerKey")),
                    new Claim("valid", _configuration.GetValue<string>("ApiUserKey"))
                }),
               
                
                Expires = DateTime.Now.AddDays(1),

                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecretKey"))),
                        SecurityAlgorithms.HmacSha512Signature
                        )
            };

            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return Ok(accessToken);

        }

        [HttpPost("SignUpAdmin")]
        public async Task<ActionResult> SignUpAdmin(AdminSignUpModel model)
        {
            if (await _context.Admins.AnyAsync(x => x.Email == model.Email))
                return BadRequest();

            var adminEntity = new AdminEntity(model.FirstName, model.LastName, model.Email);
            adminEntity.CreateSecurePassword(model.Password);


            _context.Admins.Add(adminEntity);
            await _context.SaveChangesAsync();


            return Ok();
        }

        [HttpPost("SignInAdmin")]
        public async Task<ActionResult> SignInAdmin(SignInModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Email and password can not be empty");

            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (admin == null || !admin.CompareSecurePassword(model.Password))
                return BadRequest("Email or password is not correct");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, admin.Id.ToString()),
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim("valid", _configuration.GetValue<string>("ApiAdminKey")),
                    new Claim("valid", _configuration.GetValue<string>("ApiUserKey"))
                }),

                Expires = DateTime.Now.AddDays(1),

                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecretKey"))),
                        SecurityAlgorithms.HmacSha512Signature
                        )
            };

            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return Ok(accessToken);

        }
    }
}
