using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest
{
    public class GameDrawClassComponents
    {


        public ContentManager _Content;
        public Model.StaticTextureImages _staticTextureImages;
        public Model.StaticFonts _staticFonts;
        public Game1 _this;

        public SpriteBatch _spriteBatch;
        public GraphicsDevice _graphicsDevice;



        public Model.StaticDataCards _staticDataCards;


        public int _screenWidth,_screenHeight;


        public bool _isNull;

        public GameDrawClassComponents()
        {
            _isNull = true;
            //_staticDataCards._isNull = true;
        }

        public GameDrawClassComponents(ContentManager Content, Model.StaticTextureImages staticTextureImages, Model.StaticFonts staticFonts, Game1 _This, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int screenWidth, int screenHeight)
        {
            _Content = Content;
            _staticTextureImages = staticTextureImages;
            _staticFonts = staticFonts;
            _this = _This;


             _spriteBatch = spriteBatch;
             _graphicsDevice = graphicsDevice;

             _screenWidth = screenWidth;
             _screenHeight = screenHeight;



            _isNull = false;
        }
    }
}
