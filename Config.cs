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
        public List<string> WorldsRegistry = new List<string>() { "VoidWorld" };
    }
}
