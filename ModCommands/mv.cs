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
    public class mv : ModCommand
    {


        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "mv";

        public override string Usage
            => "/mv <enter> <world>\n" +
               "/mv <leave>\n" +
               "/mv <list>";

        public override string Description
            => "Enter/Leave/List worlds";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length > 0 && Main.netMode != NetmodeID.Server)
            {
                switch (args[0])
                {
                    case "enter":
                        Subworld.Enter(SubworldManager.WorldsEnter[args[1]]);
                        Main.NewText(caller.Player.name + " Entered " + args[1], 0, 255, 0);
                        break;
                    case "leave":
                        Subworld.Exit();
                        Main.NewText(caller.Player.name + " Entered Main World", 0, 255, 0);
                        break;
                    case "list":
                        foreach (KeyValuePair<string, string> entry in SubworldManager.WorldsEnter)
                        {
                            Main.NewText(entry.Key, 0, 255, 0);
                        }
                        break;
                    case "gui":
                        switch (args[1])
                        {
                            case "on":
                                Multiverse.instance.ShowMyUI();
                                break;
                            case "off":
                                Multiverse.instance.HideMyUI();
                                break;
                        }
                        break;
                }
            }
            else
            {
                throw new UsageException();
            }
        }
    }
}