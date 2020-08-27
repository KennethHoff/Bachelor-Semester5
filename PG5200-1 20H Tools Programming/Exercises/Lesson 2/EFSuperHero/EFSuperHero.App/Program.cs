using System;
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
			
			HeroAccessor.ListAllSuperHeroes();
			
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

		public static void WriteHighlightedLine(string text)
		{
			var startColor = Console.ForegroundColor;
			
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(text);
			
			Console.ForegroundColor = startColor;
		}
	}
}