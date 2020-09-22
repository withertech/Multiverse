using Multiverse.ModWorlds;
using SubworldLibrary;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;
using Terraria.UI;
using Multiverse.ModUI;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.IO;

namespace Multiverse
{
	public class Multiverse : Mod
	{
		string id;
		internal UserInterface worldSelectInterface;
		internal WorldSelectUI worldSelectUI;
		private GameTime _lastUpdateUiGameTime;
		public static Multiverse instance;

		public override void UpdateUI(GameTime gameTime)
		{
			_lastUpdateUiGameTime = gameTime;
			if (worldSelectInterface?.CurrentState != null)
			{
				worldSelectInterface.Update(gameTime);
			}
		}
		internal void ShowMyUI()
		{
			worldSelectInterface?.SetState(worldSelectUI);
		}

		internal void HideMyUI()
		{
			worldSelectInterface?.SetState(null);
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"Multiverse: worldSelectInterface",
					delegate
					{
						if (_lastUpdateUiGameTime != null && worldSelectInterface?.CurrentState != null)
						{
							worldSelectInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
						}
						return true;
					},
					   InterfaceScaleType.UI));
			}
		}
		public override void Load()
		{
			base.Load();
			instance = this;

		}
		public override void Unload()
		{
			base.Unload();
			worldSelectUI = null;
		}
		public override void MidUpdateTimeWorld()
		{
			if (Subworld.AnyActive<Multiverse>() && GetInstance<Config>().WorldsRegistry[Util.KeyByValue(SubworldManager.WorldsEnter, SLWorld.currentSubworld.Current)].type == WorldType.NormalWorld)//lotta stuff copied from vanilla
			{
				Main.UpdateSundial();
				Main.time += Main.dayRate;
				Terraria.GameContent.Events.BirthdayParty.UpdateTime();
				Terraria.GameContent.Events.DD2Event.UpdateTime();

				if (Main.time > 54000 && Main.dayTime)//replace main.daytime with speed adjust (add/mult)
				{
					Main.time = 0;
					Main.dayTime = !Main.dayTime;
				}
				else if (Main.time > 32400 && !Main.dayTime)
				{
					if (Main.fastForwardTime)
					{
						Main.fastForwardTime = false;
						Main.UpdateSundial();
					}
					Main.checkXMas();
					Main.checkHalloween();
					Main.AnglerQuestSwap();
					Terraria.GameContent.Events.BirthdayParty.CheckMorning();
					Main.time = 0;
					Main.dayTime = !Main.dayTime;
					if (Main.sundialCooldown > 0)
					{
						Main.sundialCooldown--;
					}
					Main.moonPhase++;
					if (Main.moonPhase >= 8)
					{
						Main.moonPhase = 0;
					}
				}
			}
		}
		public override void PostSetupContent()
		{
			base.PostSetupContent();
			foreach(string name in GetInstance<Config>().WorldsRegistry.Keys)
			{
				switch (GetInstance<Config>().WorldsRegistry[name].type)
				{
					case WorldType.VoidWorld:
						id = SubworldManager.CreateVoidWorld(name, GetInstance<Config>().WorldsRegistry[name].size, GetInstance<Config>().WorldsRegistry[name].save);
						if (!SubworldManager.WorldsEnter.ContainsKey(name))
						{
							SubworldManager.WorldsEnter.Add(name, id);
						}
						break;
					case WorldType.FlatWorld:
						id = SubworldManager.CreateFlatWorld(name, GetInstance<Config>().WorldsRegistry[name].size, GetInstance<Config>().WorldsRegistry[name].save);
						if (!SubworldManager.WorldsEnter.ContainsKey(name))
						{
							SubworldManager.WorldsEnter.Add(name, id);
						}
						break;
					case WorldType.NormalWorld:
						id = SubworldManager.CreateNormalWorld(name, GetInstance<Config>().WorldsRegistry[name].size, GetInstance<Config>().WorldsRegistry[name].save);
						if (!SubworldManager.WorldsEnter.ContainsKey(name))
						{
							SubworldManager.WorldsEnter.Add(name, id);
						}
						
						break;
					default:
						break;
				}
				id = string.Empty;
			}
			if (!Main.dedServ)
			{
				worldSelectInterface = new UserInterface();

				worldSelectUI = new WorldSelectUI();
				worldSelectUI.Activate(); // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
			}
		}
	}
}