using Terraria;
using Terraria.ModLoader;
using SubworldLibrary;
using Multiverse.ModWorlds;
using Terraria.ID;
using Terraria.World.Generation;
using Terraria.IO;
using Terraria.GameContent.Generation;
using System;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using log4net.Repository.Hierarchy;

namespace Multiverse.ModCommands
{
    public class mvtp : ModCommand
    {


        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "mvtp";

        public override string Usage
            => "/mvtp <enter|leave> [World]";

        public override string Description
            => "Enter/Leave a world";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args[0].Equals("enter"))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Subworld.Enter(SubworldManager.WorldsEnter[args[1]]);
                    mod.Logger.Debug("test");
                    Main.NewText(caller.Player.name + " Entered " + args[1], 0, 255, 0);
                }
            }
            if (args[0].Equals("leave"))
            {
                Subworld.Exit();
                Main.NewText(caller.Player.name + " Entered Main World", 0, 255, 0);
            }

        }
    }
}