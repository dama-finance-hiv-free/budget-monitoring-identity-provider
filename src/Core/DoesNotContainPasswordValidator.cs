﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Core
{
    public class DoesNotContainPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var username = await manager.GetUserNameAsync(user);

            if (username == password)
                return IdentityResult.Failed(new IdentityError { Description = "Password cannot contain username" });
            if (password.Contains("password"))
                return IdentityResult.Failed(new IdentityError { Description = "Password cannot conain password" });

            return IdentityResult.Success;
        }
    }
}
