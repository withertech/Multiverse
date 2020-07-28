using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace Multiverse.ModWorlds
{
    public enum WorldSize
    {
        Small,
        Medium,
        Large
    }
    public enum WorldType
    {
        VoidWorld,
        FlatWorld,
        NormalWorld
    }
    public class WorldSettings
    {
        [Label("World Type")]
        public WorldType type { get; set; }
        [Label("World Size")]
        public WorldSize size { get; set; }
        [Label("Does World Save")]
        public bool save { get; set; }
    }
}
