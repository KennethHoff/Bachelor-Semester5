using System.Collections.Generic;


namespace EFSuperHero.Domain
{
	public class SuperHero
	{
		public static SuperHero CreateInstance(string name)
		{
			return new SuperHero()
			{
				Name = name
			};
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public HairStyle? HairStyle { get; set; }

		public ICollection<Quote>? Quotes { get; set; }

		public RealIdentity RealIdentity { get; set; }

		public ICollection<SuperHeroBattle>? Battles { get; set; }
	}
}