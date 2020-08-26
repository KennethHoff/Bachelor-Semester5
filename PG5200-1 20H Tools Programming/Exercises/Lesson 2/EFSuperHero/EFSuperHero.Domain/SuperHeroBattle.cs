using System.ComponentModel.DataAnnotations;


namespace EFSuperHero.Domain
{
	public class SuperHeroBattle
	{
		public int SuperHeroId { get; set; }
		public SuperHero SuperHero { get; set; }

		public int BattleId { get; set; }
		public Battle Battle { get; set; }
	}
}