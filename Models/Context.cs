using Microsoft.EntityFrameworkCore;
	 
namespace WagDog.Models
{
    	public class Context : DbContext
    	{
        // base() calls the parent class' constructor passing the "options" parameter along
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Cohab> Cohabs { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<DogInterest> DogInterests { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Preference> Preferences { get; set; }
    	}
}