using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Multiverse.ModItems
{
    class DimensionalPhaseShiftingMirror : ModItem
    {
		public override string Texture => "Terraria/Item_" + ItemID.MagicMirror;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Use to traverse the Multiverse");
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 34;
			item.height = 38;
			item.rare = 12;
			item.useStyle = 4;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.Item1;
		}

		public override bool UseItem(Player player)
		{
			//Enter should be called on exactly one side, which here is either the singleplayer player, or the server
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				
				Multiverse.instance.ShowMyUI();
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
