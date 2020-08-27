using System;
using System.Collections.Generic;
using System.Linq;

using EFSuperHero.Data;
using EFSuperHero.Domain;

using Microsoft.EntityFrameworkCore;


namespace EFSuperHero.App.Hero
{
	public static class HeroAccessor
	{
		public static void ListAllSuperHeroNames()
		{
			var allSuperHeroes = RetrieveAllSuperHeroes();
			ListSuperHeroAliases(allSuperHeroes, $"Here's the hero alias for all _{allSuperHeroes.Count}_ superheroes:");
		}

		public static void ListAllSuperHeroNames_OrderByName()
		{
			var allSuperHeroes = RetrieveAllSuperHeroes_OrderByName();
			ListSuperHeroAliases(allSuperHeroes, $"Here's the hero alias for all _{allSuperHeroes.Count}_ superheroes - sorted alphabetically:");
		}

		public static void ListAllSuperHerNames_OrderByIdDescending()
		{
			var allSuperHeroes = RetrieveAllSuperHeroes_OrderByIdDescending();
			ListSuperHeroAliases(allSuperHeroes, $"Here's the hero alias for all _{allSuperHeroes.Count}_ superheroes - sorted by ID descending:");
		}

		private static void ListSuperHeroAliases(List<SuperHero> allSuperHeroes, string pretext)
		{
			Program.WriteHighlightedLine(pretext);
			
			allSuperHeroes.ForEach(hero =>
			{
				Console.WriteLine($"[{hero.Id}] {hero.Name}");
			});
		}
		
		

		public static void ListAllSuperHeroesAndTheirMostImportantData()
		{
			var allSuperHeroes = RetrieveAllSuperHeroesAndTheirRelatedData();

			allSuperHeroes.ForEach(hero =>
			{
				Console.WriteLine(
					$"[{hero.Id}] \"{hero.Name}\" is the superhero alias for \"{hero.RealIdentity?.RealName}\". " +
					(hero.SuperHeroBattles == null || hero.SuperHeroBattles.Count == 0
						? "They have not been in any battles"
						: "They have been in a total of " + hero.SuperHeroBattles.Count + " battles, the most recent of which was " + hero.SuperHeroBattles.First().Battle.Name));
			});
		}

		public static List<SuperHero> RetrieveAllSuperHeroes()
		{
			Program.WriteHighlightedLine("Starting the process of retrieving all superheroes");
			List<SuperHero> listOfHeroes;
			
			using (var context = new SuperHeroDbContext())
			{
				var dbSetOfHeroes = context.SuperHeroes.AsNoTracking();

				listOfHeroes = dbSetOfHeroes.ToList();
			}

			Program.WriteHighlightedLine($"Successfully retrieved all {listOfHeroes.Count} superheroes from database!");
			return listOfHeroes;
		}

		public static List<SuperHero> RetrieveAllSuperHeroes_OrderByName()
		{
			Program.WriteHighlightedLine("Starting the process of retrieving all superheroes - ordered by name");
			List<SuperHero> listOfHeroes;
			
			using (var context = new SuperHeroDbContext())
			{
				var dbSetOfHeroes = context.SuperHeroes.AsNoTracking().OrderBy(hero => hero.Name);

				listOfHeroes = dbSetOfHeroes.ToList();
			}

			Program.WriteHighlightedLine($"Successfully retrieved all {listOfHeroes.Count} superheroes from database! - ordered by name");
			return listOfHeroes;
		}

		public static List<SuperHero> RetrieveAllSuperHeroes_OrderByIdDescending()
		{
			Program.WriteHighlightedLine("Starting the process of retrieving all superheroes - ordered by name");

			List<SuperHero> listOfHeroes;

			using (var context = new SuperHeroDbContext())
			{
				var dbSetOfheroes = context.SuperHeroes.AsNoTracking()
					.OrderByDescending(hero => hero.Id);

				listOfHeroes = dbSetOfheroes.ToList();
			}
			Program.WriteHighlightedLine($"Successfully retrieved all {listOfHeroes.Count} superheroes from database! - ordered by name");

			return listOfHeroes;


		}
		
		public static List<SuperHero> RetrieveAllSuperHeroesAndTheirRelatedData()
		{
			Program.WriteHighlightedLine("Starting the process to retrieve all superheroes - and all their related data - from the Database");
			List<SuperHero> listOfHeroes;

				
			using (var context = new SuperHeroDbContext())
			{
				// Load All Heroes
				var dbSetOfHeroes = context.SuperHeroes
						// We don't need to track changes, as I'll only ever read from this request.
					.AsNoTracking()
						// Including their RealIdentity
					.Include(hero => hero.RealIdentity)
						// Including their Quotes
					.Include(hero => hero.Quotes)
						// Including their SuperHeroBattles
					.Include(hero => hero.SuperHeroBattles)
							// Inside of which, include the battle
						.ThenInclude(shb => shb.Battle)
							// Inside of which, include the BattleLog
							.ThenInclude(btl => btl.BattleLog)
								// Inside of which, include the BattleEvents
								.ThenInclude(btlLog => btlLog.BattleEvents);

				listOfHeroes = dbSetOfHeroes.ToList();
			}

			Program.WriteHighlightedLine($"Successfully retrieved all {listOfHeroes.Count} superheroes from database!");
			return listOfHeroes;
		}

		public static void FindSuperHeroWithRealName(string realName)
		{
			Program.WriteHighlightedLine($"Starting the process of finding a superhero with the real name of {realName}");

			SuperHero hero;
			using (var context = new SuperHeroDbContext())
			{
				hero = context.RealIdentities
						// In order to get the values of the SuperHero, I first need to retrieve the superhero.
					.Include(ri => ri.SuperHero)
					.First(ri => ri.RealName == realName).SuperHero;
			}

			if (hero == null)
			{
				Program.WriteHighlightedLine($"No SuperHero with realName {realName} exists.");
			}
			else
			{
				Program.WriteHighlightedLine($"The SuperHero {hero.Name} has the realName {realName}.. HOW DID YOU KNOW?");
			}
		}
	}
}