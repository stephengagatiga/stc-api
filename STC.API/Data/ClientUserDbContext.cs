using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Data
{
    public class ClientUserDbContext : IdentityDbContext<IdentityUser>
    {
        public ClientUserDbContext(DbContextOptions<ClientUserDbContext> options) : base(options)
        {

        }
    }
}
