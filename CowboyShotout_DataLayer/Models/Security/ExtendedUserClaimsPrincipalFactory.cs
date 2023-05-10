using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CowboyShotout_DataLayer.Models.Security
{
    public class ExtendedUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ExtendedUserModel>
    {
        public ExtendedUserClaimsPrincipalFactory(UserManager<ExtendedUserModel> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ExtendedUserModel user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Location", user.Location));
            // identity.AddClaim(new Claim("SomeValue", user.PasswordHash));
            return identity;
        }
    }
}