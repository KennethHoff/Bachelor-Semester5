using System;
using System.ComponentModel.DataAnnotations.Schema;


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

		public static RealIdentity CreateRandom(SuperHero superHero, Random rng)
		{
			var realName = RandomRealName(rng);
			return CreateInstance(superHero, realName);
		}

		private static string RandomRealName(Random rng)
		{
			var firstNames = new[] {"Nathan", "Kenneth", "Maximilian", "Claude", "Peter", "Harold"};
			var firstName = firstNames[rng.Next(0, firstNames.Length-1)];

			var lastNames = new[] {"Edwards", "Holland", "Walker", "Ray", "Philips", "Campbell"};
			var lastName = lastNames[rng.Next(0, lastNames.Length - 1)];

			return firstName + " " + lastName;
		}
	}
}