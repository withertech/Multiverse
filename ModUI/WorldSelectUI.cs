using IL.Terraria;
using Multiverse.ModWorlds;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader;
using Terraria.UI;

namespace Multiverse.ModUI
{
    class WorldSelectUI : UIState
    {
        public override void OnInitialize()
        { // 1
            UIPanel panel = new UIPanel(); // 2
            panel.Width.Set(1000, 0); // 3
            panel.Height.Set(1000, 0); // 3
            panel.HAlign = 0.5f;
            panel.VAlign = 0.5f;
            Append(panel); // 4
            UIList worldSelect = new UIList();
            worldSelect.Width.Set(800, 0);
            worldSelect.Height.Set(800, 0);
            worldSelect.HAlign = 0.5f;
            worldSelect.VAlign = 0.5f;
            int i = 5;
            UIClickableButton worldButton;
            foreach (string name in SubworldManager.WorldsEnter.Keys)
            {
                
                void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
                {
                    Subworld.Enter(SubworldManager.WorldsEnter[name]);
                    Multiverse.instance.HideMyUI();
                }
                worldButton = new UIClickableButton(name, OnButtonClick);
                worldButton.Width.Set(600, 0);
                worldButton.Height.Set(100, 0);
                worldButton.HAlign = 0.5f;
                worldButton.Top.Set(i, 0);
                i += 105;
                worldSelect.Add(worldButton); 
            }
            panel.Append(worldSelect);
            
        }


    }
}
