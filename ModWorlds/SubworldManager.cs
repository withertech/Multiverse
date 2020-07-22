using Multiverse.ModWorlds;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Multiverse.ModWorlds
{
	//This class showcases how to organize your SubworldLibrary reference
	public static class SubworldManager
	{
		public static Dictionary<string, string> WorldsEnter = new Dictionary<string, string>();
		#region Helper fields and methods
		public static Mod subworldLibrary = null;

		public static bool Loaded => subworldLibrary != null;

		public static bool? Enter(string id)
		{
			if (!Loaded) return null;
			return subworldLibrary.Call("Enter", id) as bool?;
		}

		public static bool? Exit()
		{
			if (!Loaded) return null;
			return subworldLibrary.Call("Exit") as bool?;
		}

		public static bool? IsActive(string id)
		{
			if (!Loaded) return null;
			return subworldLibrary.Call("IsActive", id) as bool?;
		}

		public static bool? AnyActive(Mod mod)
		{
			if (!Loaded) return null;
			return subworldLibrary.Call("AnyActive", mod) as bool?;
		}
		#endregion

		//Call this in ModCallExampleMod.PostSetupContent()
		public static string CreateVoidWorld(string name)
		{
			subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (subworldLibrary != null)
			{
				object result = subworldLibrary.Call(
					"Register",
					/*Mod mod*/ ModContent.GetInstance<Multiverse>(),
					/*string name*/ name,
					/*int width*/ 600,
					/*int height*/ 400,
					/*List<GenPass> tasks*/ MySubworldGenPassList(),
					/*the following ones are optional, I've included three here (technically two but since order matters, had to pass null for the unload argument)
					/*Action load*/ (Action)LoadWorld,
					/*Action unload*/ null,
					/*ModWorld modWorld*/ null,
					/*bool saveSubworld*/ true
					);

				if (result != null && result is string id)
				{
					return id;
				}
			}
			return string.Empty;
		}

		//Call this in ModCallExampleMod.Unload()
		public static void Unload()
		{
			subworldLibrary = null;
		}

		//Passed into subworldLibrary.Call()
		public static void LoadWorld()
		{
			Main.dayTime = true;
			Main.time = 27000;
		}

		//Called in subworldLibrary.Call()
		public static List<GenPass> MySubworldGenPassList()
		{
			List<GenPass> list = new List<GenPass>
			{
				new SubworldGenPass(progress =>
				{
					progress.Message = "Loading"; //Sets the text above the worldgen progress bar
					Main.worldSurface = Main.maxTilesY - 42; //Hides the underground layer just out of bounds
					Main.rockLayer = Main.maxTilesY; //Hides the cavern layer way out of bounds
					//Create three tiles for the player to stand on when he spawns
					for (int i = -1; i < 2; i++)
					{
						WorldGen.PlaceTile(Main.spawnTileX - i,  Main.spawnTileY + 2, TileID.Dirt, true, true);
					}
				})
			};

			return list;
		}
	}
}