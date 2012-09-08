using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls.TabPanel
{
    public class TabPanel
    {
        public string Name;

        public string TabText;

        public bool selected;

        public bool _isNull;

        //public Texture2D BackgroundTexture;

        public Controls.PanelControl panel;

        //public List<Controls.FancyButtonControl> menuButtons;


        public TabPanel()
        {
            Name = "";
            TabText = "";
            _isNull = true;
            selected = false;

            panel = new Controls.PanelControl();
        }


        //public void load(Texture2D curBgImg)
        //{
        //    BackgroundTexture = curBgImg;
        //}

    }
}
