using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using WebApplication1.Services;

namespace WebApplication1
{
    public class CustomClaimTransformer() : IClaimsTransformation
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            try
            {
                var identity = (WindowsIdentity?)principal.Identity;
                if (identity == null)
                {
                    return principal;
                }
                RoleService.AddNewUserRole(identity.Name);
                var role = RoleService.GetRolesOf(identity.Name);
                if (role != null)
                {
                    var claimIdentity = new ClaimsIdentity();
                     claimIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                   
                    principal.AddIdentity(claimIdentity);
                }
            }
            catch (Exception ex)
            {
                return principal;
            }
            return principal;
        }

    }
}
