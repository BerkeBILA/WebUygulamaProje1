using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUygulamaProje1.Models;

namespace WebUygulamaProje1.Utility
{
	public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<AnketTuru> AnketTurleri { get; set; }
        public DbSet<Anket>Anketler {  get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
