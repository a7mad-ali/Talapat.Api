using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talapat.Api.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<ApplicationUser?> FindUserWithAddressAsync
            (this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)

        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.NormalizedEmail ==email
            .ToUpper());
            return user;


        }
    }
}
