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
		public static void ListAllSuperHeroes()
		{
			var allSuperHeroes = RetrieveAllSuperHeroes();
			
			allSuperHeroes.ForEach(hero =>
			{
				Console.WriteLine($"({hero.Id}) {hero.Name} is the superhero alias for {hero.RealIdentity?.RealName}.");

				// $"They've been in a total of {hero.SuperHeroBattles?.Count} battles");
			});
		}
		
		public static List<SuperHero> RetrieveAllSuperHeroes()
		{
			Program.WriteHighlightedLine("Starting the process to retrieve all superheroes from Database");
			List<SuperHero> listOfHeroes;

			using (var context = new SuperHeroDbContext())
			{
				var dbSetOfHeroes = context.SuperHeroes;

				Console.WriteLine(dbSetOfHeroes.Count());

				listOfHeroes = dbSetOfHeroes.ToList();
			}

			Program.WriteHighlightedLine($"Successfully retrieved all {listOfHeroes.Count} superheroes from database!");
			return listOfHeroes;
		}
	}
}