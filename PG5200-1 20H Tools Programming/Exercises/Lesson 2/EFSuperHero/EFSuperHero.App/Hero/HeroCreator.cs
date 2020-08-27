using System;

using EFSuperHero.Data;
using EFSuperHero.Domain;
using EFSuperHero.Domain.Enums;


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
	}
}