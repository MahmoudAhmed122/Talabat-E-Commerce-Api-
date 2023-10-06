using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;

namespace Talabat.Repository.Identity.Seeds
{
    public  static class IdentitySeeding
    {
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager) {
            if (!userManager.Users.Any()) {
                var user = new ApplicationUser()
                {

                    DisplayName = "Mahmoud Ahmed ",
                    Email = "tayarcoo@gmail.com",
                    UserName = "Mahmoud.Ahmed",
                    PhoneNumber = "01024248263"

                };
                await userManager.CreateAsync(user,"tT@123");
            }
      
        
        }

    }
}


