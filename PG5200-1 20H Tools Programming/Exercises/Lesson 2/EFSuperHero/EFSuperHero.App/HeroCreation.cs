using System;

using EFSuperHero.Data;
using EFSuperHero.Domain;


namespace EFSuperHero.App
{
	public class HeroCreation
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

		public  static void AddSomeSuperHeroes(int heroAmount)
		{
			Console.WriteLine("Creating some heroes");


			var someNewHeroes = new SuperHero[heroAmount];
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
	}
}