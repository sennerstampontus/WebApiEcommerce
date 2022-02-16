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
using Microsoft.AspNetCore.Authorization;
using eCommerce.Models.ViewModels;
using eCommerce.Filters;
using eCommerce.Models.UpdateModels;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly SqlContext _context;

        public AdminController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[UseAdminKey]
        public async Task<ActionResult<IEnumerable<AdminOutputModel>>> GetAdmins()
        {
            var _admins = new List<AdminOutputModel>();

            foreach(var admin in await _context.Admins.ToListAsync())
            {
                _admins.Add(new AdminOutputModel(admin.Id, admin.FirstName, admin.LastName, admin.Email));
            }
            return _admins;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminOutputModel>> GetAdmin(int id)
        {
            var adminEntity = await _context.Admins.FindAsync(id);

            if (adminEntity == null)
            {
                return NotFound();
            }

            return new AdminOutputModel(adminEntity.Id, adminEntity.FirstName, adminEntity.LastName, adminEntity.Email);
        }   
    }
}
