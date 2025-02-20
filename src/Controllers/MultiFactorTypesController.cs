using System.Threading.Tasks;
using IdentityProvider.Core;
using IdentityProvider.Data.Repository.MultiFactorType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "token")]
    public class MultiFactorTypesController : ApiControllerBase
    {
        private readonly IMultiFactorTypePersistence _multiFactorTypePersistence;

        public MultiFactorTypesController(IMultiFactorTypePersistence multiFactorTypePersistence)
        {
            _multiFactorTypePersistence = multiFactorTypePersistence;
        }

        [HttpGet]
        public async Task<IActionResult> Users(string code)
        {
            return await GetActionResult(async () =>
            {
                var users = await _multiFactorTypePersistence.GetAllAsync();

                return Ok(users);
            });
        }
    }
}