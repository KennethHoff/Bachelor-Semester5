namespace EFSuperHero.Domain
{
	public class SuperHeroBattle
	{
		public static SuperHeroBattle CreateInstance(SuperHero hero, Battle battle)
		{
			var newSuperHeroBattle =  new SuperHeroBattle()
			{
				Battle = battle,
				SuperHero = hero
			};
			
			
			return newSuperHeroBattle;
		}
		public int SuperHeroId { get; set; }
		public SuperHero SuperHero { get; set; }

		public int BattleId { get; set; }
		public Battle Battle { get; set; }

	}
}