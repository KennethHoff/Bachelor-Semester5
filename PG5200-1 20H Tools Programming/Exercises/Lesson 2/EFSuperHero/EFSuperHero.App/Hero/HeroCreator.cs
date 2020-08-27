using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

using EFSuperHero.Data;
using EFSuperHero.Domain;
using EFSuperHero.Domain.Enums;

using Microsoft.EntityFrameworkCore.Internal;


namespace EFSuperHero.App.Hero
{
	public static class HeroCreator
	{
		public static void AddOneSuperHero()
		{
			Console.WriteLine("Creating a new Hero");

			var newHero = SuperHero.CreateInstance("FoxBear");

			using (var context = new SuperHeroDbContext())
			{
				context.SuperHeroes.Add(newHero);
				context.SaveChanges();
			}

			Console.WriteLine("Successfully created a new Hero!");
		}

		public static void AddSomeSuperHeroes(int heroAmount)
		{
			Console.WriteLine("Creating some heroes");


			var someNewHeroes = new Domain.SuperHero[heroAmount];
			for (var i = 0; i < heroAmount; i++)
			{
				someNewHeroes[i] = SuperHero.CreateInstance("Hero" + i);
			}

			using (var context = new SuperHeroDbContext())
			{
				context.SuperHeroes.AddRange(someNewHeroes);
				context.SaveChanges();
			}

			Console.WriteLine("Successfully created some new heroes!");
		}

		// ReSharper disable once InconsistentNaming
		// ReSharper disable once IdentifierTypo
		public static SuperHero CreateAllMightFromBNHA()
		{
			Console.WriteLine("Starting the creation of All Might!");


			var hero = SuperHero.CreateInstance("All Might", HairStyle.Radiant);

			var realIdentify = RealIdentity.CreateInstance(hero, "Toshinori Yagi");

			var quotes = new Quote[]
			{
				Quote.CreateInstance("The Symbol of Peace", QuoteStyle.Cheesy, hero),
				Quote.CreateInstance("UNITED STATES OF SMASH!", QuoteStyle.Awesome, hero),
				Quote.CreateInstance("Because I am here!", QuoteStyle.Cheesy, hero)
			};

			hero.RealIdentity = realIdentify;
			hero.Quotes = quotes;

			Console.WriteLine("Sucessfully created All Might!");
			return hero;
		}

		public static SuperHero CreateRandom(Random rng)
		{
			// Console.WriteLine("Started the process of creating a random SuperHero!");

			var heroAlias = RandomHeroAlias(rng);

			var hairStyleRNG = rng.Next(0, 4);

			// if RNG lands on either 2 or 3, then it'll be 'Normal'
			HairStyle hairStyle = HairStyle.Normal;


			switch (hairStyleRNG)
			{
				case 0:

					hairStyle = HairStyle.Covered;
					break;

				case 1:

					hairStyle = HairStyle.Radiant;
					break;
			}

			var hero = SuperHero.CreateInstance(heroAlias, hairStyle);

			hero.RealIdentity = RealIdentity.CreateRandom(hero, rng);

			hero.Quotes = Quote.CreateRandom(rng.Next(0, 10), rng, hero);

			// Console.WriteLine("Finalized the creation of a random SuperHero!");

			return hero;
		}


		/// <summary>
		/// Creates a list of random superheroes with random names (based on a list of predefined first and last names, as well as prefix-, and suffix-based superhero aliases
		/// </summary>
		/// <param name="amount">Number of heroes to create</param>
		/// <param name="rng">Used for randomizing the names</param>
		/// <returns></returns>
		public static List<SuperHero> CreateRandom(int amount, Random rng)
		{
			// Console.WriteLine($"Started the process creating {amount} random SuperHeroes!");

			List<SuperHero> result = new List<SuperHero>(amount);
			for (int i = 0; i < amount; i++)
			{
				result.Add(CreateRandom(rng));
			}

			// Console.WriteLine($"Finalized the creation of {amount} random SuperHeroes!");

			return result;
		}

		private static string RandomHeroAlias(Random rng)
		{
			var prefixes = new[] {"Phantom", "Doctor", "Master", "Commander", "Captain", "The Fantastic", "The Defiant", "Protector"};
			var prefix = prefixes[rng.Next(0, prefixes.Length-1)];

			var suffixes = new[] {"Might", "Archer", "Sword", "Blade", "Mastermind", "Monarch"};
			var suffix = suffixes[rng.Next(0, suffixes.Length-1)];


			return prefix + " " + suffix;
		}
	}
}