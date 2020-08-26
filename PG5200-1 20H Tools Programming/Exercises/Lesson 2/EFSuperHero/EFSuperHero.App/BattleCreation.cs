using System;

using EFSuperHero.Data;
using EFSuperHero.Domain;


namespace EFSuperHero.App
{
	public class BattleCreation
	{
		
		public static void AddOneBattle()
		{
			Console.WriteLine("Starting the process of adding a battle to the database!");

			const string battleName = "An Added Battle";

			const string battleDescription = "Was added via the 'AddOneBattle' function in the BattleCreation Class";
			const bool isBrutal = false;

			var today = DateTime.Now;
			var yesterday = today.AddDays(-1);

			const int numberOfBattleEvents = 10;

			var newBattleLog = BattleLog.CreateInstance("Battle Log Of: \n " + battleName, numberOfBattleEvents);

			var newBattle = Battle.CreateInstance(battleName, 
				battleDescription,
				isBrutal,
				newBattleLog,
				yesterday,
				today);

			using (var context = new SuperHeroDbContext())
			{
				context.Battles.Add(newBattle);
				context.SaveChanges();
			}

			Console.WriteLine("Finalized the process of adding a battle to the database!");
		}
		
		public static void AddSomeBattles()
		{
			Console.WriteLine("Starting the process of adding multiple battle to the database!");

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
					
					var newBattle = Battle.CreateInstance(name, description, isBrutal, battleLog, startDate, endDate);
					context.Battles.Add(newBattle);
				}
				context.SaveChanges();
			}

			Console.WriteLine("Finalized the process of adding a battle to the database!");
		}
	}
}