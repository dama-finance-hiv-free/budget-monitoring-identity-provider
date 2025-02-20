using System.Linq;
using System.Threading.Tasks;
using IdentityProvider.Core;
using IdentityProvider.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "token")]
    public class UsersController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Users(string code)
        {
            return await GetActionResult(async () =>
            {
                var users = await _context.Users.Select(x => new
                {
                    Tenant = x.Organization,
                    Code = x.UserCode,
                    UsrName = x.FullName,
                    x.Locale,
                    PhotoUrl = x.ImageUrl,
                    x.Email
                }).ToArrayAsync();

                return Ok(users);
            });
        }
    }
}