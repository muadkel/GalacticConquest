using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;



namespace GalacticConquest
{
    public class GameUpdateClassComponents
    {
        public MouseState _curMouseState;
        public ContentManager _Content;
        public Model.StaticTextureImages _staticTextureImages;
        public Model.StaticFonts _staticFonts;
        public Game1 _this;
        public GameComponentCollection _Components;

        public GraphicsDevice _graphicsDevice;


        public int _screenWidth, _screenHeight;

        public bool _isNull;

        public GameUpdateClassComponents()
        {
            _isNull = true;
        }

        public GameUpdateClassComponents(MouseState curMouseState, ContentManager Content, Model.StaticTextureImages staticTextureImages, Model.StaticFonts staticFonts, Game1 _This, GameComponentCollection Components, GraphicsDevice graphicsDevice, int screenWidth, int screenHeight)
        {
            _curMouseState = curMouseState;
            _Content = Content;
            _staticTextureImages = staticTextureImages;
            _staticFonts = staticFonts;
            _this = _This;
            _Components = Components;

            _graphicsDevice = graphicsDevice;

            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            _isNull = false;
        }

    }
}
