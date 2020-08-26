using Microsoft.EntityFrameworkCore;

using EFSuperHero.Domain;


namespace EFSuperHero.Data
{
	public class SuperHeroDbContext : DbContext
	{
		public DbSet<SuperHero> SuperHeroes { get; set; }

		public DbSet<Quote> Quotes { get; set; }

		public DbSet<RealIdentity> RealIdentities { get; set; }

		public DbSet<SuperHeroBattle> SuperHeroBattles { get; set; }

		public DbSet<Battle> Battles { get; set; }

		public DbSet<BattleEvent> BattleEvents { get; set; }
		public DbSet<BattleLog> BattleLogs { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// optionsBuilder.UseSqlServer(@"Server = DESKTOP-L2O7IRM; Database = SuperHeroDb; Trusted_Connection = True;");
			optionsBuilder.UseSqlServer(@"Server = KENNYBOIII-DESK; Database = SuperHeroDb; Trusted_Connection = True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SuperHeroBattle>().HasKey(b => new {b.BattleId, b.SuperHeroId});
		}
	}
}