using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.DrawObj
{
    public class DrawHelper
    {
        public static void DrawRectangle(GameDrawClassComponents curGameDrawClassComponents, Color backColor, Rectangle panelRect, Color borderColor, int borderSize)
        {
            Texture2D t = new Texture2D(curGameDrawClassComponents._graphicsDevice, 1, 1);
            t.SetData(new[] { backColor });


            curGameDrawClassComponents._spriteBatch.Draw(t, panelRect, backColor);


            //Draw Border
            curGameDrawClassComponents._spriteBatch.Draw(t, new Rectangle(panelRect.Left, panelRect.Top, borderSize, panelRect.Height), borderColor); // Left
            curGameDrawClassComponents._spriteBatch.Draw(t, new Rectangle(panelRect.Right, panelRect.Top, borderSize, panelRect.Height), borderColor); // Right
            curGameDrawClassComponents._spriteBatch.Draw(t, new Rectangle(panelRect.Left, panelRect.Top, panelRect.Width, borderSize), borderColor); // Top
            curGameDrawClassComponents._spriteBatch.Draw(t, new Rectangle(panelRect.Left, panelRect.Bottom, panelRect.Width, borderSize), borderColor); // Bottom

        }
    }
}
