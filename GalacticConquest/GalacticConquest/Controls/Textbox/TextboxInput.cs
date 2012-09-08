#region Using Statements

using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace GalacticConquest.Controls.Textbox.TextInput
{
    public class TextboxInput : SpriteTextBox
    {
        public bool _isEnabled = false;

        #region Fields( _input, _handle, _blinkDelay, _blinkTimer, blink, _cursorPosition, _displayCursor )

        /// <summary>
        /// TextInput and IntPtr are used to tie a textbox to the window keyboard events. 
        /// </summary>
        static private TextInput _input;
        static private IntPtr _handle;

        private int _blinkTimer;
        /// <summary>
        /// _blink is a flag used in draw method to determine if cursor should be drawn. 
        /// </summary>
        private bool _blink;

        private Vector2 _cursorPosition;
        /// <summary>
        /// _displayCursor is used in algorithim to remove the cursor when text is clipped on the y axis
        /// of the textbox safe text area. 
        /// </summary>
        private bool _displayCursor;

        #endregion

        #region Properties( HasFocus, EnterPressed, DisplayCursor, CursorString, CursorPosition, CursorOffset

        /// <summary>
        /// returns bool that indicates wheter this textbox is hooked into the windows keyboard events. 
        /// </summary>
        public bool HasFocus { get; protected set; }
        /// <summary>
        /// returns bool that indicates wheter Enter is pressed. (current implementation Blurs textbox when enter
        /// is pressed.)
        /// </summary>
        public bool EnterPressed { get; protected set; }
        
        /// <summary>
        /// Property to set wheter or not a textbox should display a cursor. 
        /// </summary>
        public bool DisplayCursor { get; set; }
        /// <summary>
        /// This is the string used as a cursor. (Future implemenations may allow for animated sprite)
        /// </summary>
        public string CursorString { get; set; }
        /// <summary>
        /// Property allowing position of cursor to be set, it is calculated internally so it is a protected property. 
        /// </summary>
        protected Vector2 CursorPosition { get { return this._cursorPosition; } set { this._cursorPosition = value; } }
        /// <summary>
        /// Offset value to add to Cursors Position to allow for alignment tweeks. 
        /// </summary>
        public Vector2 CursorOffset { get; set; }
        /// <summary>
        /// BlinkDelay is a Property to set the interval at which the cursor is blinked.
        /// </summary>
        public int BlinkDelay { get; set; }

        public int MaxTextLength { get; set; }

        #endregion

        #region Constructor, Update, Draw

        /// <summary>
        /// Creates a textbox with default settings.
        /// BlinkDelay = 350 milliseconds
        /// CursorString = "_"
        /// DisplayCursor = true
        /// CursorOffset = (0,0)
        /// </summary>
        /// <param name="game">Instance of Game class</param>
        /// <param name="assetName">Name of the background Texture2D to be loaded for textbox</param>
        /// <param name="fontName">Name of the font to used in the textbox</param>
        /// 
        public TextboxInput( Game game, string assetName, string fontName ) : base( game, assetName, fontName )
        {
            // check to see if a handle has already been assigned.
            if (TextboxInput._handle != null)
            {
                // no window handle has been previously assigned so set handle from the game parameter. 
                TextboxInput._handle = game.Window.Handle;
            }

            this.EnterPressed = false;
            this.BlinkDelay = 350;
            this._blinkTimer = 0;
            this._blink = false;
            this.CursorOffset = Vector2.Zero;
            this.CursorString = "_";
            this.DisplayCursor = true;
            this._displayCursor = true;
            this._isEnabled = true;
        }

        public override void Update(GameTime gameTime)
        {
            // increment the blink timer by amount of elapsed game time since last update.
            this._blinkTimer += gameTime.ElapsedGameTime.Milliseconds;
            // check to see if blink timer exceed the amount of blink delay. 
            if (this._blinkTimer > this.BlinkDelay)
            {
                // toggle the blink indicator
                this._blink = !this._blink;
                // reset the blink timer to 0
                this._blinkTimer = 0;
            }

            // check to see if this textbox has focus. 
            if ( this.HasFocus == true)
            {
                // it has focus, make sure it is hooked into windows keyboard events, if not hook it into the windows keyboard events
                if (TextboxInput._input == null)
                {
                    // hook the windows keyboard events. 
                    this.SetTextInput();
                }
                
                // check to see if the EnterKey was pressed. 
                if (TextboxInput._input.EnterKey)
                {
                    // blur this text box and set EnterPressed Property of this Textbox. 
                    this.EnterPressed = true;
                    this.Blur();
                }
                else
                {
                    // Enter has not been pressed to process the windows events if present. 
                    // poll the keys from event buffer and add it to the Text Property. 
                    this.Text += TextboxInput._input.Buffer;
                    // clear buffer so next poll does not contain characters from previous poll. 
                    TextboxInput._input.clearBuffer();
                    // check to see if backspace was pressed.
                    if (TextboxInput._input.BackSpace)
                    {
                        // ensure Text Property is not empty. 
                        if (this.Text.Length > 0)
                        {
                            // set Text Property to itself minus the last character. 
                            this.Text = this.Text.Substring(0, this.Text.Length - 1);
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // call the draw routine of the SprtieTextBox to draw the background image and Text. 
            base.Draw(gameTime);

            // make sure textbox has focus, that the cursor is suppose to be displayed, and if cursor is current blinked.
            if (HasFocus && this._displayCursor && this._blink )
            {
                // to correct cursor position for empty string.
                if (this.WrapText && this.Text == String.Empty)
                {
                    // Kludge: cursor positioning extracted by calling parse, ensuring it is only called when a string is empty and not on
                    // every call as base class does not call parse when Text Property is empty for optimization. 
                    this.parseText(this.Text);
                }
                else
                {
                    // check to see if Text is empty. 
                    if (this.Text == String.Empty)
                    {
                        // corrects cursor position for empty Text Property when word wrapping is disabled. 
                        this._cursorPosition = this.Font.MeasureString(" ") * new Vector2(0, this.TextScale.Y);
                    }
                    else if( this.WrapText == false )
                    {
                        // scales cursor position to text scale when word wrapping is disabled. 
                        this._cursorPosition = this.Font.MeasureString(this.Text) * this.TextScale;
                    }
                }

                // draws the cursor. 
                this.SpriteBatch.Begin();
                this.SpriteBatch.DrawString(
                        this.Font,
                        this.CursorString,
                        this.CursorPosition + TextOffset + Position + CursorOffset,
                        this.Color,
                        this.Rotation,
                        this.Origin,
                        this.TextScale,
                        SpriteEffects.None,
                        0.5f);
                this.SpriteBatch.End();
            }
        }

        #endregion

        #region Helper Methods( SetTextInput, onClick, Blur, parseText )

        /// <summary>
        /// Hooks the textbox into the windows keyboard events
        /// </summary>
        private void SetTextInput()
        {
            // ensures that textbox is not already hooked before creating a new hook. 
            if (TextboxInput._input == null)
            {
                TextboxInput._input = new TextInput(TextboxInput._handle);
            }
        }

        /// <summary>
        /// Sets the focus of the textbox if the Vector2 that is passed in is contained within the textbox. 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool onClick(Vector2 point)
        {
            // checks to see if textbox already has focus. 
            if (!this.HasFocus)
            {
                // calculates the Scaled Width and Height of background texture in pixels.
                this.SetDisplayProperties();
                // creates a rectangle based on textbox position and actual pixel size on screen.
                Rectangle TextBoxArea = new Rectangle(
                    (int)this.Position.X,
                    (int)this.Position.Y,
                    (int)this.DisplayWidth,
                    (int)this.DisplayHeight);
                // sets the HasFocus property and returns true if point is contained with Textbox image. 
                return this.HasFocus = TextBoxArea.Contains(new Point((int)point.X, (int)point.Y));
            }
            return false;
        }
        /// <summary>
        /// Sets the focus of the textbox if the Point that is passed in is contained within the textbox. 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool onClick(Point point)
        {
            // checks to see if textbox already has focus.
            if (!this.HasFocus )
            {
                // calculates the Scaled Width and Height of background texture in pixels.
                this.SetDisplayProperties();
                // creates a rectangle based on textbox position and actual pixel size on screen.
                Rectangle TextBoxArea = new Rectangle(
                    (int)this.Position.X,
                    (int)this.Position.Y,
                    (int)this.DisplayWidth,
                    (int)this.DisplayHeight);
                // sets the HasFocus property and returns true if point is contained with Textbox image.
                return this.HasFocus = TextBoxArea.Contains(point);
            }
            return false;
        }

        /// <summary>
        /// Removes the Focus of the textbox and Disposes the Hook to the windows keyboard events. 
        /// </summary>
        public void Blur()
        {
            this.HasFocus = false;
            TextboxInput._input.Dispose();
            TextboxInput._input = null;
        }

        /// <summary>
        /// Parse the Text Property when WrapText us set to true, calculates position of cursor while
        /// parsing the text.
        /// </summary>
        /// <param name="text">The string that needs to parsed</param>
        /// <returns>copy of the Text Property with '\n' inserted to keep text within the text safe area.</returns>
        protected override String parseText(String text)
        {
            // used to determine if programmer explicitly set the DisplayCursor to false. 
            if (this.DisplayCursor)
            {
                // programmer set to true so set to display cursor flag to true. 
               this._displayCursor = true;
            }
            else
            {
                // programmer set to false, so do not display cursor at all. 
                this._displayCursor = false;
            }
            String line = String.Empty;
            String returnString = String.Empty;
            // breaks the text string into an array that contains each word in the string as seperate element.  
            String[] wordArray = text.Split(' ');
            // Added line to work with my Textbox implementation, instremental in my Clipping algorithim. 
            Vector2 textSafeArea = this.GetTextSafeArea();

            // KLUDGE: with out this I can not adjust for cursor position, offsets to wierd position
            //         feel free to explore and fix. 
            bool test = false;

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
                    if (((this.Font.MeasureString(returnString).Y + this.Font.LineSpacing) * this.TextScale.Y) > textSafeArea.Y
                        && this.FormFactor != ScaleType.ToTextbox)
                    {
                        // the words from this point can not be seen, so cursor is outside of text safe area, set 
                        // display cursor flag to false. 
                        this._displayCursor = false;
                        // breaks out of the loop ending the parsing.
                        break;
                    }
                    // adds the line with the '/n' added to the return string. 
                    returnString = returnString + line + '\n';

                    // KLUDGE: with out this I can not adjust for cursor position, offsets to wierd position
                    //         feel free to explore and fix. 
                    test = true;
                    // resets the line to empty so a new line can be constructed with no contamination. 
                    line = String.Empty;
                }

                // add the word to the current line as it has not exceed the width of the text safe area. 
                line = line + word + ' ';
            }
            this._cursorPosition.X = this.Font.MeasureString(line).X * this.TextScale.X;

            // KLUDGE: with out the ( test ? var : var ) I can not adjust for cursor position, offsets to wierd position
            //         feel free to explore and fix. 
            this._cursorPosition.Y = (this.Font.MeasureString(returnString).Y + (test ? 0.0f : (float)this.Font.LineSpacing)) * this.TextScale.Y;
            // parsing complete return the constructed string. 
            return returnString + line;
        }

        #endregion
    }
}