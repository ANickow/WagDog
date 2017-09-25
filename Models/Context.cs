using Microsoft.EntityFrameworkCore;
	 
namespace WagDog.Models
{
    	public class Context : DbContext
    	{
        // base() calls the parent class' constructor passing the "options" parameter along
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Dog> Dogs { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<DogInterest> DogInterests { get; set; }
    	}
}