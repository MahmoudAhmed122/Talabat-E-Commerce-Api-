using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class IdentityStoreContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityStoreContext(DbContextOptions<IdentityStoreContext> options):base(options)
        {

        }

    }
}
