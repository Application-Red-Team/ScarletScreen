using Microsoft.EntityFrameworkCore;

namespace ScarletScreen.Model
{
    public class AccountsContext : DbContext
    {
        public DbSet<Accounts> users { get; set; }

        public AccountsContext(DbContextOptions options) : base(options)
        { 
            
        }
    }
}
