namespace EFSuperHero.Domain
{
	public class BattleEvent
	{
		public static BattleEvent CreateInstance(int order, string summary, string description)
		{
			// Console.WriteLine($"Creating a new BattleEvent (#{order})");

			var newBattleEvent = new BattleEvent()
			{
				Order = order,
				Summary = summary,
				Description = description
			};
			// Console.WriteLine("Created a new BattleEvent");

			return newBattleEvent;
		}

		public int id { get; set; }

		public int Order { get; set; }

		public string Summary { get; set; }

		public string Description { get; set; }

		// How do I add this value? Retroactively by passing in the BattleLog retroactively?
		public BattleLog BattleLog { get; set; }
	}
}