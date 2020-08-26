namespace EFSuperHero.Domain
{
	public class RealIdentity
	{
		public int Id { get; set; }
		public int RealName { get; set; }

		public int SuperHeroId { get; set; }
		public SuperHero SuperHero { get; set; }
	}
}