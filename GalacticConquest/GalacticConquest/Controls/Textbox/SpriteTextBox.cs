#region Using Statements

using System;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace GalacticConquest.Controls.Textbox.TextInput
{
    public class SpriteTextBox : Sprite
    {
        #region Fields( ScaleType )      
  
        // used to set the FormFactor of the textbox. 
        public enum ScaleType
        {
            None,
            ToText,
            ToTextbox
        };

        #endregion

        #region Properties( DisplayWidth, DisplayHeight, TextboxMarginX, TextboxMarginY, TextScale, Text, TextOffset, Font, FontName, FromFactor, WrapText )

        // Pixel width and height of TextBox after Scaling the Textbox
        // Only guarenteed to be valid if SetDisplayProperties() has been called before use.
        public float DisplayWidth { get; protected set; }
        public float DisplayHeight { get; protected set; }

        // the Pixel offset for the approriate Margins
        public float TextboxMarginX { get; set; }
        public float TextboxMarginY { get; set; }

        // Property to Scale the Text in the Textbox.
        public Vector2 TextScale { get; set; }
        // Property storing the Text to be displayed in Textbox.
        public string Text { get; set; }
        // Property to Offest the text by used primarily by Scaling Functions as margins need 
        // to be scaled in some cases but not all cases
        protected Vector2 TextOffset { get; set; }

        // Property to store the Font used by the Textbox.
        public SpriteFont Font { get; set; }
        // used for the LoadContent() is the assest that will be loaded. 
        public string FontName { get; set; }

        // Used to determine how to Scale the Textbox set to an value of enum ScaleType
        public ScaleType FormFactor { get; set; }
        // flag used to determine if Wrapping Text should be enabled.
        public bool WrapText { get; set; }

        #endregion

        #region Helper Methods( GetTextBoundingArea, GetTextSafeArea, SetDisplayProperties, ScaleTextToTextbox, ScaleTextboxToText, parseText, ToString )
 
        /// <summary>
        /// Calculates the bounding rectangle of the text and return a Vector2 for the lower right corner 
        /// </summary>
        /// <returns></returns>
        public Vector2 GetTextBoundingArea()
        {
            // The text is single line so just return the pixel messurments provide by SpriteFont.MeasureString()
            return this.Font.MeasureString( this.WrapText ? this.parseText(this.Text) : this.Text );
        }

        /// <summary>
        /// Returns and Sets the Area within the Textbox that the text should appear on, used in Text Wrapping,
        /// Clipping, and Scaling. 
        /// </summary>
        /// <returns></returns>
        public Vector2 GetTextSafeArea()
        {
            // make sure that the Display Properties have current Dimensions
            this.SetDisplayProperties();

            // Scale the Margins and added total margin distance.
            float marginOffsetX = (this.TextboxMarginX * this.Scale.X) * 2;
            float marginOffsetY = (this.TextboxMarginY * this.Scale.Y) * 2;
            // Set and return the TextSafeArea.
            return new Vector2(this.DisplayWidth - marginOffsetX, this.DisplayHeight - marginOffsetY);
        }

        /// <summary>
        /// Calculates the Dimensions of the Textbox that are actually displayed.
        /// </summary>
        public void SetDisplayProperties()
        {
            // make sure that the Height and Width properties have been assigned valid values first.
            if (this.Height != null && this.Width != null)
            {
                // Scale the texture width and height to find display width and height.
                this.DisplayHeight = (float)this.Height * this.Scale.Y;
                this.DisplayWidth = (float)this.Width * this.Scale.X;
            }
        }

        /// <summary>
        /// Scales the Text size so that it fits completely within the message box, scales along both x and y 
        /// axis of the text. 
        /// </summary>
        protected void ScaleTextToTextbox()
        {
            // Gets the values needed to set the Ratio for the Text Scaling. 
            Vector2 textSafeArea = this.GetTextSafeArea();
            Vector2 textBoundingArea = this.GetTextBoundingArea();
            
            
            // Calculate the Scaling Ratio for the Text so it fits within the TextSafeArea.
            textBoundingArea.X = textSafeArea.X / textBoundingArea.X;
            textBoundingArea.Y = textSafeArea.Y / textBoundingArea.Y;

            // Assigns the Scale to the proper scaling Property. 
            this.TextScale = textBoundingArea;
            // Sets the TextOffset to properly align the text durning rendering. 
            this.TextOffset = new Vector2(this.TextboxMarginX * this.Scale.X, this.TextboxMarginY * this.Scale.Y);
        }

        /// <summary>
        /// Scales the Textbox to size of the text. Not very effective if wrapping is turned on. 
        /// </summary>
        protected void ScaleTextboxToText()
        {
            // Get the size of the Text to get bounds for the scale ratio calculations. 
            Vector2 textBoundingArea;
            if (this.Text == String.Empty)
            {
                textBoundingArea = Font.MeasureString(" ");
            }
            else
            {
                textBoundingArea = this.GetTextBoundingArea();
            }
            textBoundingArea *= this.TextScale;
            // Adds the margins to the text bounding area, not scaled no way to know what scaling factor
            // should be applied to margins. Relies on coder to adjust Margins manually for visual accuracy. 
            textBoundingArea.X += (this.TextboxMarginX * 2);
            textBoundingArea.Y += (this.TextboxMarginY * 2);

            // Calculates the Scaling Ratio for the Textbox to contain the Text with Margin buffer. 
            textBoundingArea.X = textBoundingArea.X / (float)this.Width;
            textBoundingArea.Y = textBoundingArea.Y / (float)this.Height;

            // Assigns the Scale to the proper scaling Property. 
            this.Scale = textBoundingArea;
            // Sets the TextOffset to properly align the text durning rendering.
            this.TextOffset = new Vector2(this.TextboxMarginX, this.TextboxMarginY);
        }

        /// <summary>
        /// returns a copied version of Text string with '/n' added to insure text does not exceed 
        /// the text safe area of the textbox. 
        /// code was found at:
        /// http://danieltian.wordpress.com/2008/12/24/xna-tutorial-typewriter-text-box-with-proper-word-wrapping-part-2/
        /// author of code is: Danial Tian.
        /// I modified the code some to account for Vertical Clipping. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            // breaks the text string into an array that contains each word in the string as seperate element.  
            String[] wordArray = text.Split(' ');
            // Added line to work with my Textbox implementation, instremental in my Clipping algorithim. 
            Vector2 textSafeArea = this.GetTextSafeArea();

            foreach (String word in wordArray)
            {
                // Checks to see if the width of the line exceeds text safe area's width, 
                // does this by piecing together the words one at a time and checking to see if
                // that current line width + new word width exceeds length. 
                // Modified this line slightly from original by adding the .X to MeasureString(line + word) as 
                // it was only nessisary to check against width. 
                if (this.Font.MeasureString(line + word).X * this.TextScale.X > textSafeArea.X)
                {
                    // Added this line to clip along the y axis of the text safe area.  
                    if( ( (this.Font.MeasureString(returnString).Y + this.Font.LineSpacing) * this.TextScale.Y ) > textSafeArea.Y ) 
                    {
                        // breaks out of the loop ending the parsing.
                        break;
                    }
                    // adds the line with the '/n' added to the return string. 
                    returnString = returnString + line + '\n';
                    // resets the line to empty so a new line can be constructed with no contamination. 
                    line = String.Empty;
                }

                // add the word to the current line as it has not exceed the width of the text safe area. 
                line = line + word + ' ';
            }
            // parsing complete return the constructed string. 
            return returnString + line;
        }

        public override string ToString()
        {
            return this.Text;
        }

        #endregion

        #region Constructors, Draw, Update

        /// <summary>
        /// Instanciates a SpriteTextbox. AssetName is the Textbox background assest to load. 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="assetName">Name of background Image to load</param>
        /// <param name="fontName"></param>
        /// <param name="text"></param>
        /// <param name="wrapText"></param>
        /// <param name="scale"></param>
        /// <param name="textScale"></param>
        /// <param name="formFactor"></param>
        public SpriteTextBox(Game game, string assetName, string fontName, string text, bool wrapText, Vector2 scale, Vector2 textScale, ScaleType formFactor)
            : base(game, assetName)
        {
            this.Text = text;
            this.WrapText = wrapText;
            this.Scale = scale;
            this.TextScale = textScale;
            this.TextOffset = Vector2.Zero;
            this.FontName = fontName;
            this.FormFactor = formFactor;
        }

        /// <summary>
        /// Instanciates a SpriteTextbox.  AssetName is the Textbox background assest to load. 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="assetName">Name of background Image to load</param>
        /// <param name="fontName"></param>
        public SpriteTextBox(Game game, string assetName, string fontName)
            : this(game, assetName, fontName, String.Empty, true, Vector2.One, Vector2.One, ScaleType.None )
        {
        }

        protected override void LoadContent( )
        {
            // Loads the textbox image, which autosets the origin properties which needs to be overriden. 
            base.LoadContent();
            this.Font = this._content.Load<SpriteFont>(this.FontName);
            // Sets the Origin to (0,0) top left corner needed for scaling algorithims to work properly.
            // and TextOffset to line up properly. 
            this.Origin = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime)
        {
            // Draws the TextBox background image first. 
            base.Draw(gameTime);

            //string tempStr;
            if( this.Visible )
            {
                // Check to see what FormFactor has been set and call appropriate function. 
                switch (this.FormFactor)
                {
                    case ScaleType.ToText:
                        this.ScaleTextboxToText();
                        break;
                    case ScaleType.ToTextbox:
                        this.ScaleTextToTextbox();
                        break;
                    default:
                        this.TextOffset = new Vector2(TextboxMarginX * this.Scale.X, TextboxMarginY * this.Scale.Y);
                        break;
                }

                if (this.Text != String.Empty)
                {
                    this.SpriteBatch.Begin();
                    this.SpriteBatch.DrawString(
                        this.Font,
                        this.WrapText ? this.parseText(this.Text) : this.Text,
                        this.Position + this.TextOffset,
                        this.Color,
                        this.Rotation,
                        this.Origin,
                        this.TextScale,
                        SpriteEffects.None,
                        0.5f);
                    this.SpriteBatch.End();
                }
            }

        }

        #endregion
    }
}
