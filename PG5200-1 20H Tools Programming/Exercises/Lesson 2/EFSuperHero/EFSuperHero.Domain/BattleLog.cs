using System.Collections.Generic;


namespace EFSuperHero.Domain
{
	public class BattleLog
	{
		public int id { get; set; }
		public string Name { get; set; }

		public int BattleId { get; set; }
		public Battle Battle { get; set; }
		
		public ICollection<BattleEvent> BattleEvents { get; set; }
	}
}