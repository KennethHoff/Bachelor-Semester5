using EFSuperHero.Domain.Enums;


namespace EFSuperHero.Domain
{
	public class Quote
	{
		public static Quote CreateInstance(string text, QuoteStyle style, SuperHero superHero)
		{
			return new Quote()
			{
				Text = text,
				QuoteStyle = style,
				SuperHero = superHero
			};
		}
		public int Id { get; set; }
		public string Text { get; set; }

		public QuoteStyle? QuoteStyle { get; set; }

		public int? SuperHeroId { get; set; }

		public SuperHero? SuperHero { get; set; }
	}
}