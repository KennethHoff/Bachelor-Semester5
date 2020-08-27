using System;
using System.Collections.Generic;


namespace EFSuperHero.Domain
{
	public class BattleLog
	{

		public static BattleLog CreateInstance(string name, int numberOfBattleEvents)
		{
			// Console.WriteLine("Creating a new BattleLog");
			
			var newBattleLog = new BattleLog()
			{
				Name = name,
				BattleEvents = new List<BattleEvent>(numberOfBattleEvents)
			};
			
			for (var i = 1; i <= numberOfBattleEvents; i++)
			{
				newBattleLog.BattleEvents.Add(BattleEvent.CreateInstance(i, $"Battle Event #{i}", $"This was Battle Event #{i}"));
			}
			// Console.WriteLine("Created a new BattleLog");

			
			return newBattleLog;
		}
		
		public int id { get; set; }
		public string Name { get; set; }

		public int BattleId { get; set; }
		public Battle Battle { get; set; }

		public ICollection<BattleEvent> BattleEvents { get; set; }
	}
}