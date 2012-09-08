using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class GalacticMapControl
    {
        public string ID;

        public Texture2D imgControl;

        public Vector2 vectorPos;

        public Rectangle rectControl;

        public bool _isNull;


        public GalacticMapControl()
        {
            _isNull = true;
            ID = "";
            vectorPos = Vector2.Zero;
        }

        public GalacticMapControl(string id, Texture2D curImgControl, Vector2 curVectorPos, Rectangle curRect)
        {
            _isNull = false;
            ID = id;

            imgControl = curImgControl;
            vectorPos = curVectorPos;
            rectControl = curRect;


        }
    }
}
