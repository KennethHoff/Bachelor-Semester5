using System.ComponentModel.DataAnnotations.Schema;


namespace EFSuperHero.Domain
{
	public class Quote
	{
		public int Id { get; set; }
		public string Text { get; set; }

		public QuoteStyle? QuoteStyle { get; set; }

		[ForeignKey("Super Hero Owner")]
		public int SuperHeroId { get; set; }

		public SuperHero SuperHero { get; set; }
	}
}