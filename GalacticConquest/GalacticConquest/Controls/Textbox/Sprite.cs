#region Using Statements

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

#endregion

namespace GalacticConquest.Controls.Textbox.TextInput
{
    public class Sprite : DrawableGameComponent
    {
        #region Fields( _content, _assetName )

        protected ContentManager _content;
        private string _assetName;

        #endregion

        #region Properties ( AssetName, Image, Position, Origin, Rotation, Color, SourceRectangle, SpriteEffects, Layer, Scale, Width, Height, SpriteBatch )

        /// <summary>
        /// is the name of the Texture2D object to load. 
        /// </summary>
        public string AssetName
        {
            get { return this._assetName; }
            set
            {
                if (value == "" || value == null)
                {
                    throw new Exception("Asset Name can not be Null or Empty!");
                }
                this._assetName = value;
            }
        }
        /// <summary>
        /// stores the image that is loaded. 
        /// </summary>
        protected Texture2D Image { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public Rectangle? SourceRectangle { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public float Layer { get; set; }

        /// <summary>
        /// Nullable Width and Height properties to insure that if used indepent and not as a component, that
        /// value can be tested incase it is being querried before the Texture2D Asset has been loaded. 
        /// </summary>
        public float? Width { get; protected set; }
        public float? Height { get; protected set; }
        public SpriteBatch SpriteBatch { get; set; }

        #endregion

        #region Methods( CalculateOrigin, Constructors(3) )

        /// <summary>
        /// calculates the origin of the Texture2D. Called during LoadContent.
        /// </summary>
        protected virtual void CalculateOrigin()
        {
            this.Origin = new Vector2(this.Image.Width / 2, this.Image.Height / 2);
        }

        public Sprite( Game game, string assestName, Color color, Vector2 position, float rotation, Vector2 scale, Rectangle? sourceRectangle) : base( game )
        {
            this._content = game.Content;
            this.AssetName = assestName;
            this.Color = color;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.SourceRectangle = sourceRectangle;
            this.SpriteEffects = SpriteEffects.None;
            this.Layer = 0.5f;
            this.Height = this.Width = null;
            this.SpriteBatch = null;
        }

        public Sprite(Game game, string assestName, Color color, Vector2 position)
            : this(game,  assestName, color, position, 0.0f, Vector2.One, null)
        {
        }

        public Sprite(Game game, string assestName)
            : this(game, assestName, Color.White, Vector2.Zero)
        {
        }

        #endregion

        #region Methods ( Draw, Update, LoadContent )

        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(
                this.Image, 
                this.Position, 
                this.SourceRectangle, 
                this.Color,
                this.Rotation, 
                this.Origin, 
                this.Scale, 
                SpriteEffects.None, 
                0.5f);
            this.SpriteBatch.End();
        }

        protected override void LoadContent( )
        {
            // checks to see if a ContentManger is available to the sprite to load assest. 
            if ( this._content == null)
            {
                // no ContentManager was found, try to load content manager from services. 
                this._content = (ContentManager)Game.Services.GetService(typeof(ContentManager));
            }

            this.Image = this._content.Load<Texture2D>(this.AssetName); 
            this.CalculateOrigin();
            // Set the nullable Width and Height Properties so with image Width and Height. 
            this.Width = this.Image.Width;
            this.Height = this.Image.Height;

            // checks to see if SpriteBatch has been created for this sprite.
            if (this.SpriteBatch == null)
            {
                // no SpriteBatch found, check to see one is stored in the Game Services, if so use it. 
                this.SpriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
                // checks to see if a SpriteBatch was successful retrieved from the Game Services. 
                if (this.SpriteBatch == null)
                {
                    // no SpriteBatch found in Game Services, create one from the GraphicDevice stored
                    // in the image asset. 
                    this.SpriteBatch = new SpriteBatch(this.Image.GraphicsDevice);
                }
            }
        }

        #endregion
    }
}
