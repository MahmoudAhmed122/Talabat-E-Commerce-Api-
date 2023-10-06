using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.core.Entities.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserManager
    {


        public static async Task<ApplicationUser> GetUserWithAddressAsync(this UserManager<ApplicationUser> userManager , ClaimsPrincipal User)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user=  await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(e=>e.Email==email);
            return user;
        }
    }
}
