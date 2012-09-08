using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Model
{
    public class StaticFonts
    {
        public SpriteFont _courierNew;





        public StaticFonts(ContentManager Content)
        {
            _courierNew = Content.Load<SpriteFont>("Courier New");
        }
    }
}
