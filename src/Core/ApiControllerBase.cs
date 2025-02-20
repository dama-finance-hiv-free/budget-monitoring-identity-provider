using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Core
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected async Task<IActionResult> GetActionResult(Func<Task<IActionResult>> codeToExecute) => await codeToExecute.Invoke();

        protected async Task<IActionResult> GetActionResult(string requiredClaim, Func<Task<IActionResult>> codeToExecute)
        {
            IActionResult response;

            try
            {
                response = await codeToExecute.Invoke();
            }
            catch (Exception)
            {
                response = BadRequest();
            }
            return response;
        }

        protected void ReadValidationErrors(ApiResponse response)
        {
            foreach (var modelError in ModelState.Values.SelectMany(modelState => modelState.Errors))
                response.ValidationErrors.Add(modelError.ErrorMessage);
        }
    }
}