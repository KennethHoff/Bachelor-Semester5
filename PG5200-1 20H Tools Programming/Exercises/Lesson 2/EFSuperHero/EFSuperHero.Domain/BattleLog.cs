using System;
using System.Collections.Generic;


namespace EFSuperHero.Domain
{
	public class BattleLog
	{
		public static BattleLog CreateInstance(string name, int numberOfBattleEvents)
		{
			Console.WriteLine("Creating a new BattleLog");
			
			var newBattleLog = new BattleLog()
			{
				Name = name,
				BattleEvents = new List<BattleEvent>(numberOfBattleEvents)
			};
			
			Console.WriteLine("Created a new BattleLog");

			for (var i = 1; i <= numberOfBattleEvents; i++)
			{
				newBattleLog.BattleEvents.Add(BattleEvent.CreateInstance(i, $"Battle Event #{i}", $"This was Battle Event #{i}"));
			}
			
			return newBattleLog;
		}
		
		public int id { get; set; }
		public string Name { get; set; }

		// How do I add this value? Retroactively by passing in the Battle retroactively?
		public int BattleId { get; set; }
		public Battle Battle { get; set; }

		public ICollection<BattleEvent> BattleEvents { get; set; }
	}
}