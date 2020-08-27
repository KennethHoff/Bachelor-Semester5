using System;
using System.Collections.Generic;

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

		public static List<Quote> CreateRandom(int amount, Random rng, SuperHero hero)
		{
			// Console.WriteLine($"Started the process creating {amount} random quotes!");

			var result = new List<Quote>(amount);

			for (int i = 0; i < amount; i++)
			{
				result.Add(CreateRandom(rng, hero));
			}

			// Console.WriteLine($"Finalized the creation of {amount} random quotes!");

			return result;
		}

		public static Quote CreateRandom(Random rng, SuperHero hero)
		{
			var length = rng.Next(1, 10);

			var words = new List<string>()
			{
				"do", "since", "the", "man", "work", "money", "love", "self-importance", "smash", "important", "life", "lives", "hero"
			};

			string text = "";

			for (int i = 0; i < length; i++)
			{
				var currRngIndex = rng.Next(0, words.Count);
				text += words[currRngIndex];
				words.RemoveAt(currRngIndex);
			}
			
			var quoteStyleRng = rng.Next(0, 4);

			// if RNG lands on either 2 or 3, then it'll be 'Awesome'
			QuoteStyle quoteStyle = Enums.QuoteStyle.Awesome;


			switch (quoteStyleRng)
			{
				case 0:

					quoteStyle = Enums.QuoteStyle.Cheesy;
					break;

				case 1:

					quoteStyle = Enums.QuoteStyle.Lame;
					break;
			}

			return Quote.CreateInstance(text, quoteStyle, hero);
		}
	}
}