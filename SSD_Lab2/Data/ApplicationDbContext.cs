using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSD_Lab2.Models;

namespace SSD_Lab2.Data {
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {

        public DbSet<Company> Companies { get; set; }
    }
}
