//=================================================
// xWinForms
// Copyright ?2007 by Eric Grossinger
// http://psycad007.spaces.live.com/
//=================================================
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SmartTank.Draw;
namespace GalacticConquest
{
    public partial class Control
    {
        public static TextureUsage resourceUsage = TextureUsage.None;
        //public static ResourceManagementMode resourceMode = ResourceManagementMode.Automatic;

        public static string fontName = GameFonts.Lucida;

        public const float fontScale = 0.5f;


        public Vector2 position;
        //public Vector2 Origin
        //{
        //    get { return origin; }
        //    set { origin = value; }
        //}
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float alpha = 1f;
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { text = value; /*OnTextChange(null, null);*/ }
        }

        public bool bchecked = false;
        public bool bChecked
        {
            get { return bchecked; }
            set { bchecked = value; }
        }

        public bool bvisible = true;
        public bool bVisible
        {
            get { return bvisible; }
            set { bvisible = value; }
        }
        //new add
        public bool benable = true;
        public bool bEnable 
        {
            get { return benable; }
            set { benable = value; }
        }
        //new add

        public string selectedItem = string.Empty;
        public string SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        public int min = 0;
        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        public int max = 1;
        public int Max
        {
            get { return max; }
            set { max = value; }
        }
        public int value = 0;
        public int Value
        {
            get { return value; }
            set { value = this.value; }
        }

        private ControlType type = ControlType.Null;
        public ControlType Type
        {
            get { return type; }
            set { type = value; }
        }
        public enum ControlType
        {
            Null = 0,
            Button,
            Checkbox,
            Combo,
            Label,
            Listbox,
            Listview,
            NumericUpDown,
            Potentiometer,
            ProgressBar,
            RadioButton,
            Slider,
            Textbox,
            Menu,
            Submenu,
            TextButton,
            Scrollbar
        }
        //public enum Style
        //{
        //    Default,
        //    Futuristic
        //}
        public Control()
        {
        }

        public virtual void Update() { }
        //public virtual void Update(Vector2 formPosition, Vector2 formSize)
        //{
        //    position = origin + formPosition;            
        //}

        public virtual void Draw(SpriteBatch spriteBatch, float alpha)
        {
            this.alpha = alpha; 
        }
    }
}
