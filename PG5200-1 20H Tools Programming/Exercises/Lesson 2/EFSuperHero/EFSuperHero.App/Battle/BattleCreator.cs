using System;

using EFSuperHero.Data;
using EFSuperHero.Domain;


namespace EFSuperHero.App
{
	public class BattleCreator
	{
		public static void AddOneBattle()
		{
			Console.WriteLine("Starting the process of adding a battle to the database!");

			const string battleName = "An Added Battle";

			const string battleDescription = "Was added via the 'AddOneBattle' function in the BattleCreator Class";
			const bool isBrutal = false;

			var today = DateTime.Now;
			var yesterday = today.AddDays(-1);

			const int numberOfBattleEvents = 10;

			var newBattleLog = BattleLog.CreateInstance("Battle Log Of: \n " + battleName, numberOfBattleEvents);

			var newBattle = Battle.CreateInstance(battleName, 
				battleDescription,
				isBrutal,
				yesterday);

			newBattle.EndDate = today;
			newBattle.BattleLog = newBattleLog;

			using (var context = new SuperHeroDbContext())
			{
				context.Battles.Add(newBattle);
				context.SaveChanges();
			}

			Console.WriteLine("Finalized the process of adding a battle to the database!");
		}
		
		public static void AddSomeBattles()
		{
			Program.WriteHighlightedLine("Starting the process of adding multiple battle to the database!");

			const int numberOfBattles = 10;

			const int numberOfBattleEvents = 10;

			
			using (var context = new SuperHeroDbContext())
			{
				for (var i = 0; i < numberOfBattles; i++)
				{
					var name = $"Battle #{i}";
					var description = $"Description for battle #{i}";
					var isBrutal = (i % 2 == 0);
					var battleLog = BattleLog.CreateInstance("Battle Log Of: \n " + name, numberOfBattleEvents);
					var startDate = DateTime.Now;
					var endDate = DateTime.Now.AddDays(-1);
					
					var newBattle = Battle.CreateInstance(name, description, isBrutal, startDate);
					
					newBattle.EndDate = endDate;
					newBattle.BattleLog = battleLog;
					
					context.Battles.Add(newBattle);
				}
				context.SaveChanges();
			}

			Program.WriteHighlightedLine("Finalized the process of adding a battle to the database!");
		}

		public static Battle CreateRandom()
		{
			Program.WriteHighlightedLine("Started the process of creating a random battle!");


			#region Date Generation ( WAAAAY too complex for this :|)

			var RNG = new Random();

			var year = RNG.Next(0, 9999);
			
			var century = Math.Floor(year / 100f);

			var nthCentury = century + 1;
			
			var lastDigit = nthCentury % 10;

			var counter = lastDigit switch
			{
				1 => "st", 2 => "nd", 3 => "rd", _ => "th"
			};
			
			var startDate = new DateTime(year, 1, 1);
			
			// How convenient it lasted exactly a second less than an entire year..
			var endDate = startDate.AddYears(1).AddSeconds(-1);

			
			var isBrutal = (year * RNG.Next() % 1) == 0;

			#endregion
			

			var battleName = $"battle of the {nthCentury}{counter} century";
			var battleDescription = $"The {battleName} was a {(isBrutal ? "devastating battle" : "battle soon to be forgotten")}";


			Console.WriteLine(startDate);
			var battle = Battle.CreateInstance(battleName, battleDescription, isBrutal, startDate);

			var numberOfEvents = RNG.Next(1, 25);
			
			var battleLog = BattleLog.CreateInstance($"BattleLog for the {battleName}", numberOfEvents);
			

			battle.BattleLog = battleLog;
			battle.EndDate = endDate;
			
			Program.WriteHighlightedLine("Finalized the creation of a random battle!");

			return battle;
		}
	}
}