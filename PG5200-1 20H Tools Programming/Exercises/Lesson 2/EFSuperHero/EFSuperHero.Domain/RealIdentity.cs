namespace EFSuperHero.Domain
{
	public class RealIdentity
	{
		public static RealIdentity CreateInstance(SuperHero superHero, string realName)
		{
			return new RealIdentity()
			{
				SuperHero = superHero,
				RealName = realName
			};
		}
		public int Id { get; set; }
		public string RealName { get; set; }

		public int SuperHeroId { get; set; }
		public SuperHero SuperHero { get; set; }
	}
}