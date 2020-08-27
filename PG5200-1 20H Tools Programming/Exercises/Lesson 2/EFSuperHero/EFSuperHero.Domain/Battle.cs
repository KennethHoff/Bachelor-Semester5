using System;
using System.Collections.Generic;


namespace EFSuperHero.Domain
{
	public class Battle
	{
		public static Battle CreateInstance(string name, string description, bool isBrutal, DateTime startDate)
		{
			Console.WriteLine("Creating a new Battle");

			var newBattle = new Battle()
			{
				Name = name,
				Description = description,
				IsBrutal = isBrutal,
				StartDate = startDate,
				SuperHeroBattles = new List<SuperHeroBattle>()
			};
			
			Console.WriteLine("Created a new Battle");

			return newBattle;
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool IsBrutal { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public ICollection<SuperHeroBattle> SuperHeroBattles { get; set; }

		public BattleLog BattleLog { get; set; }
	}
}