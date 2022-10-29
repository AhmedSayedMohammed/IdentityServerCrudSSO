using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ASPNetIdentityDbContext : IdentityDbContext
    {
        public ASPNetIdentityDbContext(DbContextOptions<ASPNetIdentityDbContext> options) : base(options)
        { }
    }
}
