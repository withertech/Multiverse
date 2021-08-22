using Multiverse.ModWorlds;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using Multiverse.ModUI;
using Terraria;
using Terraria.IO;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace Multiverse.ModWorlds
{
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
		

		public static string CreateVoidWorld(string name, WorldSize size, bool save)
		{
			int x;
			int y;
			switch (size)
			{
				case WorldSize.Small:
					x = 4200;
					y = 1200;
					break;
				case WorldSize.Medium:
					x = 6400;
					y = 1800;
					break;
				case WorldSize.Large:
					x = 8400;
					y = 2400;
					break;
				default:
					x = 0;
					y = 0;
					break;
			}
			subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (subworldLibrary != null)
			{
				object result = subworldLibrary.Call(
					"Register",
					/*Mod mod*/ ModContent.GetInstance<Multiverse>(),
					/*string name*/ name,
					/*int width*/ x,
					/*int height*/ y,
					/*List<GenPass> tasks*/ VoidGenPassList(),
					/*the following ones are optional, I've included three here (technically two but since order matters, had to pass null for the unload argument)
					/*Action load*/ (Action)LoadWorld,
					/*Action unload*/ null,
					/*ModWorld modWorld*/ null,
					/*bool saveSubworld*/ save
					);

				if (result != null && result is string id)
				{
					return id;
				}
			}
			return string.Empty;
		}
		
		public static string CreateFlatWorld(string name, WorldSize size, bool save)
		{
			int x;
			int y;
			switch (size)
			{
				case WorldSize.Small:
					x = 4200;
					y = 1200;
					break;
				case WorldSize.Medium:
					x = 6400;
					y = 1800;
					break;
				case WorldSize.Large:
					x = 8400;
					y = 2400;
					break;
				default:
					x = 0;
					y = 0;
					break;
			}
			subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (subworldLibrary != null)
			{
				object result = subworldLibrary.Call(
					"Register",
					/*Mod mod*/ ModContent.GetInstance<Multiverse>(),
					/*string name*/ name,
					/*int width*/ x,
					/*int height*/ y,
					/*List<GenPass> tasks*/ FlatGenPassList(),
					/*the following ones are optional, I've included three here (technically two but since order matters, had to pass null for the unload argument)
					/*Action load*/ (Action)LoadWorld,
					/*Action unload*/ null,
					/*ModWorld modWorld*/ null,
					/*bool saveSubworld*/ save
					);

				if (result != null && result is string id)
				{
					return id;
				}
			}
			return string.Empty;
		}

		public static string CreateNormalWorld(string name, WorldSize size, bool save)
		{
			int x;
			int y;
			switch (size)
			{
				case WorldSize.Small:
					x = 4200;
					y = 1200;
					break;
				case WorldSize.Medium:
					x = 6400;
					y = 1800;
					break;
				case WorldSize.Large:
					x = 8400;
					y = 2400;
					break;
				default:
					x = 0;
					y = 0;
					break;
			}
			
			subworldLibrary = ModLoader.GetMod("SubworldLibrary");
			if (subworldLibrary != null)
			{
				object result = subworldLibrary.Call(
					"Register",
					/*Mod mod*/ ModContent.GetInstance<Multiverse>(),
					/*string name*/ name,
					/*int width*/ x,
					/*int height*/ y,
					/*List<GenPass> tasks*/ NormalGenPassList(),
					/*the following ones are optional, I've included three here (technically two but since order matters, had to pass null for the unload argument)
					/*Action load*/ (Action)LoadNormalWorld,
					/*Action unload*/ null,
					/*ModWorld modWorld*/ null,
					/*bool saveSubworld*/ save
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
			SLWorld.drawUnderworldBackground = false;
		}

		public static void LoadNormalWorld()
		{
			Main.dayTime = true;
			Main.time = 27000;
			SLWorld.drawUnderworldBackground = true;
		}

		public static List<GenPass> VoidGenPassList()
		{
			List<GenPass> list = new List<GenPass>
			{
				new SubworldGenPass(progress =>
				{
					progress.Message = "Loading"; //Sets the text above the worldgen progress bar
					Main.worldSurface = Main.maxTilesY - 42; //Hides the underground layer just out of bounds
					Main.rockLayer = Main.maxTilesY - 64; //Hides the cavern layer way out of bounds
					//Create three tiles for the player to stand on when he spawns
					for (int i = -1; i < 2; i++)
					{
						WorldGen.PlaceTile(Main.spawnTileX - i,  Main.spawnTileY + 2, TileID.Dirt, true, true);
					}
				})
			};

			return list;
		}
		public static List<GenPass> FlatGenPassList()
		{
			List<GenPass> list = new List<GenPass>
			{
				new SubworldGenPass(progress =>
				{
					progress.Message = "Placing Dirt"; //Sets the text above the worldgen progress bar
					Main.worldSurface = Main.maxTilesY - 42; //Hides the underground layer just out of bounds
					Main.rockLayer = Main.maxTilesY - 64; //Hides the cavern layer way out of bounds
					for (int i = -Main.maxTilesX; i < Main.maxTilesX; i++)
					{
						float percent = (float) i /  Main.maxTilesX;
						progress.Set(percent);
						for (int ii = Main.spawnTileY + 30; ii > Main.spawnTileY; ii--)
						{
							WorldGen.PlaceTile(i,  ii, TileID.Dirt, true, true);
						}
					}
				}),
				new SubworldGenPass(progress =>
				{
					progress.Message = "Placing Stone";
					for (int i = -Main.maxTilesX; i < Main.maxTilesX; i++)
					{
						float percent = (float) i /  Main.maxTilesX;
						progress.Set(percent);
						for (int ii = Main.maxTilesY; ii > Main.spawnTileY + 30; ii--)
						{
							WorldGen.PlaceTile(i,  ii, TileID.Stone, true, true);
						}
					}
				}),
				new SubworldGenPass(progress =>
				{
					progress.Message = "Spreading Grass";
					for (int i = -Main.maxTilesX; i < Main.maxTilesX; i++)
					{
						WorldGen.SpreadGrass(i, Main.spawnTileY + 1);
					}
				}),
				new SubworldGenPass(progress =>
				{
					if (ModContent.GetInstance<Config>().FlatWorldsHaveTrees)
					{
						progress.Message = "Growing Trees";
						WorldGen.AddTrees();
					}
					progress.Set(100.0f);
				}),
				new SubworldGenPass(progress =>
				{
					progress.Message = "Growing Plants";
					WorldGen.AddPlants();
					progress.Set(100.0f);
				})
			};
			return list;
		}

		public static List<GenPass> NormalGenPassList()
		{
			List<GenPass> list = new List<GenPass>
			{
				new SubworldGenPass(progress =>
				{
					WorldGen.generateWorld(new UnifiedRandom().Next(), progress);
				})
			};

			return list;
		}
	}
}