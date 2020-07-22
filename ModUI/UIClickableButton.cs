using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Multiverse.ModUI
{
	public class UIClickableButton : UIElement
	{

		// 1
		private object _text;
		private UIElement.MouseEvent _clickAction;
		private UIPanel _uiPanel;
		private UIText _uiText;
		public string World;

		// 2
		public string Text
		{
			get => _uiText?.Text ?? string.Empty; // 3
			set => _text = value;
		}

		public UIClickableButton(object text, UIElement.MouseEvent clickAction) : base()
		{ // 4
			_text = text?.ToString() ?? string.Empty;
			_clickAction = clickAction;
		}
		public override void OnInitialize()
		{
			base.OnInitialize();
			_uiPanel = new UIPanel(); // 5
			_uiPanel.Width = StyleDimension.Fill; // 5
			_uiPanel.Height = StyleDimension.Fill; // 5
			Append(_uiPanel);

			_uiText = new UIText(""); // 6
			_uiText.VAlign = _uiText.HAlign = 0.5f; // 6
			_uiPanel.Append(_uiText);

			_uiPanel.OnClick += _clickAction; // 7
		}

		public override void Update(GameTime gameTime)
		{
			if (_text != null)
			{ // 8
				_uiText.SetText(_text.ToString());
				_text = null;
				Recalculate(); // 9
				base.MinWidth = _uiText.MinWidth; // 9
				base.MinHeight = _uiText.MinHeight; // 9
			}
		}
	}
}
