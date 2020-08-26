namespace EFSuperHero.Domain
{
	public class BattleEvent
	{
		public int id { get; set; }
		public int Order { get; set; }
		public string Summary { get; set; }
		public string Description { get; set; }
		
		public BattleLog BattleLog { get; set; }
	}
}