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
			foreach(string name in GetInstance<Config>().WorldsRegistry.Keys)
			{
				switch (GetInstance<Config>().WorldsRegistry[name])
				{
					case WorldType.VoidWorld:
						string voidId = SubworldManager.CreateVoidWorld(name);
						if (!SubworldManager.WorldsEnter.ContainsKey(name))
						{
							SubworldManager.WorldsEnter.Add(name, voidId);
						}
						break;
					case WorldType.NormalWorld:
						string normalId = SubworldManager.CreateNormalWorld(name);
						if (!SubworldManager.WorldsEnter.ContainsKey(name))
						{
							SubworldManager.WorldsEnter.Add(name, normalId);
						}
						break;
					default:
						break;
				}
			}
		}
	}
}