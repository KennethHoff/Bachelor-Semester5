using System;
using System.Collections.Generic;
using System.Linq;

using EFSuperHero.App.Hero;
using EFSuperHero.Data;
using EFSuperHero.Domain;


namespace EFSuperHero.App
{
	class Program
	{
		private static void Main(string[] args)
		{
			ClearDataBase();
			// HeroCreator.AddOneSuperHero();
			// HeroCreator.AddSomeSuperHeroes(50);
			
			// BattleCreator.AddOneBattle();
			// BattleCreator.AddSomeBattles();
			
			AddOneSuperHeroWithRelatedData();

			// AddRandomData();
			
			// HeroAccessor.ListAllSuperHeroNames();
			// HeroAccessor.ListAllSuperHeroNames_OrderByName();
			// HeroAccessor.ListAllSuperHerNames_OrderByIdDescending();
			
			HeroAccessor.FindSuperHeroWithRealName("Toshinori Yagi");

			// HeroAccessor.ListAllSuperHeroesAndTheirMostImportantData();
		}


		private static void ClearDataBase()
		{
			WriteHighlightedLine("Starting the process of removing all battles and superheroes, and hopefully (by the way of cascading deletion) deleted all other entities as well");
			using (var context = new SuperHeroDbContext())
			{
				context.Battles.RemoveRange(context.Battles.Where(b => b.Id >= 0));
				context.SuperHeroes.RemoveRange(context.SuperHeroes.Where(b => b.Id >= 0));
				context.SaveChanges();
			}

			WriteHighlightedLine("Sucessfully cleared the database!");
		}

		private static void AddOneSuperHeroWithRelatedData()
		{
			var hero = HeroCreator.CreateAllMightFromBNHA();


			var battle = BattleCreator.CreateRandom();


			var superHeroBattle = SuperHeroBattle.CreateInstance(hero, battle);

			hero.SuperHeroBattles.Add(superHeroBattle);

			battle.SuperHeroBattles.Add(superHeroBattle);
			
			using (var context = new SuperHeroDbContext())
			{
				context.Battles.Add(battle);
				context.SuperHeroes.Add(hero);
				context.SaveChanges();
			}
		}

		private static void AddRandomData()
		{
			var RNG = new Random();

			// For each battle, what is the chance that the hero was apart of it.
			const int heroChanceToBeInABattle = 80; // Out of 100


			List<SuperHero> heroes = HeroCreator.CreateRandom(10, RNG);

			List<Battle> battles = BattleCreator.CreateRandom(10);


			WriteHighlightedLine("Assigning battles to heroes.. Randomly!");
			heroes.ForEach(hero =>
			{
				battles.ForEach(battle =>
				{
					if (RNG.Next(1, 101) < 80)
					{
						hero.SuperHeroBattles.Add(SuperHeroBattle.CreateInstance(hero, battle));
					}
				});
			});
			WriteHighlightedLine("Assignment succeeded");

			using (var context = new SuperHeroDbContext())
			{
				context.SuperHeroes.AddRange(heroes);
				context.Battles.AddRange(battles);
				
				context.SaveChanges();
			}
		}

		public static void WriteHighlightedLine(string text)
		{
			var startColor = Console.ForegroundColor;
			
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(text);
			
			Console.ForegroundColor = startColor;
		}
	}
}