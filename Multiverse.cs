using Multiverse.ModWorlds;
using SubworldLibrary;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;

namespace Multiverse
{
	public class Multiverse : Mod
	{
		public override void PostSetupContent()
		{
			base.PostSetupContent();
			foreach(string name in GetInstance<Config>().WorldsRegistry)
			{
				string id = SubworldManager.CreateVoidWorld(name);
				if (!SubworldManager.WorldsEnter.ContainsKey(name))
				{
					SubworldManager.WorldsEnter.Add(name, id);
					Logger.Debug(name);
					Logger.Debug(id);
				}
			}
		}
	}
}