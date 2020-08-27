using Microsoft.EntityFrameworkCore;

using EFSuperHero.Domain;


namespace EFSuperHero.Data
{
	public class SuperHeroDbContext : DbContext
	{

		#region SETUP

		private const string ServerName = "KENNYBOIII-DESK";

		#endregion
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
			optionsBuilder.UseSqlServer($@"Server = {ServerName}; Database = SuperHeroDb; Trusted_Connection = True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			#region Composite Keys

			modelBuilder.Entity<SuperHeroBattle>()
				.HasKey(b => new {b.BattleId, b.SuperHeroId});

			#endregion

			

			#region OnDelete Events
			
			// The following should be altered if a Battle is deleted:
			// BattleLog (Deleted)
			// SuperHeroBattle (Deleted) <-
				
			// The following should be altered if a Hero is deleted:
			// Quote (Hero set to NULL... Actually, it too will be deleted :> )
			// RealIdentity (Deleted)
			// SuperHeroBattle (Deleted) <-
				
			// The following should be altered if BattleLog is deleted (For example, a battle was deleted):
			// BattleEvent (Deleted)


			// A Quote needs a hero, but we don't want to forget such a good quote do we, so we're saving it for future generations to learn (That said, I didn't get it to work, so it's deleted too.. whoops)
			modelBuilder.Entity<Quote>()
				.HasOne(q => q.SuperHero)
				.WithMany(sh => sh.Quotes)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);


			// A RealIdentity needs a hero, and when said hero is deleted, so is the RealIdentity.. "You'll never know who they were" 
			modelBuilder.Entity<RealIdentity>()
				.HasOne(ri => ri.SuperHero)
				.WithOne(sh => sh.RealIdentity)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			
			// A BattleEvent needs a battleLog - "If a tree fell in the forest, <bla bla bla>"
			modelBuilder.Entity<BattleEvent>()
				.HasOne(be => be.BattleLog)
				.WithMany(bl => bl.BattleEvents)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
			
			
			// What is a battle without a set of events, and what is a BattleLog if not a set of events.
			modelBuilder.Entity<BattleLog>()
				.HasOne(bl => bl.Battle)
				.WithOne(b => b.BattleLog)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			
			// If a Battle or Hero gets destroyed, delete all SuperHeroBattles that they were apart of.
			modelBuilder.Entity<SuperHeroBattle>()
				.HasOne(shb => shb.Battle)
				.WithMany(b => b.SuperHeroBattles)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
			
			modelBuilder.Entity<SuperHeroBattle>()
				.HasOne(shb => shb.SuperHero)
				.WithMany(sh => sh.SuperHeroBattles)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
			
			#endregion

		}
	}
}