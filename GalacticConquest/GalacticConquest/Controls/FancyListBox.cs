using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GalacticConquest.Controls
{
    public class FancyListBox
    {
        List<string> items = new List<string>();
        int startItem;
        int lineCount;
        Texture2D image;
        Texture2D cursor;
        Color selectedColor = Color.Red;
        int selectedItem;

        bool hasFocus;


        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }
        public int SelectedIndex
        {
            get { return selectedItem; }
            set { selectedItem = (int)MathHelper.Clamp(value, 0f, items.Count); }
        }
        public string SelectedItem
        {
            get { return Items[selectedItem]; }
        }
        public List<string> Items
        {
            get { return items; }
        }
        //public override bool HasFocus
        //{
        //    get { return hasFocus; }
        //    set
        //    {
        //        hasFocus = value;
        //        if (hasFocus)
        //            OnEnter(null);
        //        else
        //            OnLeave(null);
        //    }
        //}

        //public FancyListBox(Texture2D background, Texture2D cursor)
        //{
        //    hasFocus = false;
        //    //tabStop = false;
        //    this.image = background;
        //    this.image = cursor;
        //    this.Size = new Vector2(image.Width, image.Height);
        //    lineCount = image.Height / SpriteFont.LineSpacing;
        //    startItem = 0;
        //    Color = Color.Black;
        //}


    }
}
