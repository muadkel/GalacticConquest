using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GalacticConquest.Controls
{
    public class DataCardIconControl
    {
        Model.DataCardType _type;

        public string ID;

        public string strControlText;

        public Texture2D imgControl;

        public Vector2 vectorPos;

        public Rectangle rectControl;

        public bool _isNull;


        public DataCardIconControl()
        {
            _isNull = true;
            ID = "";
            vectorPos = Vector2.Zero;
        }



    }
}
