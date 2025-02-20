using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityProvider.Data.Entity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            // Add custom claims in token here based on user properties or any other source
            claims.Add(new Claim("user_telephone", user.PhoneNumber ?? string.Empty));
            claims.Add(new Claim("user_email", user.Email ?? string.Empty));
            claims.Add(new Claim("user_locale", user.Locale ?? string.Empty));
            claims.Add(new Claim("user_fullname", user.FullName ?? string.Empty));
            claims.Add(new Claim("user_organization", user.Organization ?? string.Empty));
            claims.Add(new Claim("user_map", user.UserCode ?? string.Empty));
            claims.Add(new Claim("user_avatar", user.ImageUrl ?? string.Empty));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
