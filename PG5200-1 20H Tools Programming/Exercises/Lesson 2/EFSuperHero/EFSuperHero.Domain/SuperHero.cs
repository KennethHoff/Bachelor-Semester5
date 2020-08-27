using System.Collections.Generic;

using EFSuperHero.Domain.Enums;


namespace EFSuperHero.Domain
{
	public class SuperHero
	{
		public static SuperHero CreateInstance(string alias, HairStyle hairStyle = Enums.HairStyle.Normal)
		{
			return new SuperHero()
			{
				Name = alias,
				HairStyle = hairStyle,
				SuperHeroBattles = new List<SuperHeroBattle>()
			};
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public HairStyle? HairStyle { get; set; }

		public ICollection<Quote>? Quotes { get; set; }

		public RealIdentity RealIdentity { get; set; }

		public ICollection<SuperHeroBattle> SuperHeroBattles { get; set; }
	}
}