using Microsoft.EntityFrameworkCore;
	 
namespace WagDog.Models
{
    	public class Context : DbContext
    	{
        // base() calls the parent class' constructor passing the "options" parameter along
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        // public DbSet<MODELNUMBERTWO> ModelNumberTwos { get; set; }

        // public DbSet<MODELNUMBERTHREE> ModelNumberThrees { get; set; }
    	}
}