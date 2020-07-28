using Multiverse.ModWorlds;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Multiverse
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [Label("Subworlds")]
        public Dictionary<string, WorldSettings> WorldsRegistry = new Dictionary<string, WorldSettings>()
        {
            { "VoidWorld", new WorldSettings {type = WorldType.VoidWorld, size = WorldSize.Medium, save = true} },
            { "FlatWorld", new WorldSettings {type = WorldType.FlatWorld, size = WorldSize.Medium, save = true} },
            { "NormalWorld", new WorldSettings {type = WorldType.NormalWorld, size = WorldSize.Medium, save = true} }
        };
    }
}
