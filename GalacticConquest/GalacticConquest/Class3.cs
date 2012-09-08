using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using System.IO;
using TankEngine2D.Helpers;
using SmartTank.Helpers;


namespace SmartTank.Helpers
{
    public static class Directories
    {
        #region Game base directory
        public static readonly string GameBaseDirectory =
            StorageContainer.TitleLocation;

        #endregion

        #region Directories

        public static string ContentDirectory
        {
            get { return Path.Combine(GameBaseDirectory, "Content"); }
        }

        public static string BasicGraphicsContent
        {
            get { return Path.Combine(ContentDirectory, "BasicGraphics"); }
        }

        public static string FontContent
        {
            get { return Path.Combine(ContentDirectory, "Font"); }
        }

        public static string UIContent
        {
            get { return Path.Combine(ContentDirectory, "UI"); }
        }

        public static string TankTexture
        {
            get { return Path.Combine(ContentDirectory, "Tanks"); }
        }

        public static string SoundDirectory
        {
            get { return Path.Combine(GameBaseDirectory, "Content\\Sounds"); }
        }

        public static string AIDirectory
        {
            get { return Path.Combine(GameBaseDirectory, "AI"); }
        }

        public static string MapDirectory
        {
            get { return Path.Combine(GameBaseDirectory, "Map"); }
        }

        public static string GameObjsDirectory
        {
            get { return Path.Combine(GameBaseDirectory, "GameObjs"); }
        }

        #endregion

        #region FilePath

        public static string AIListFilePath
        {
            get { return Path.Combine(AIDirectory, "AIList.txt"); }
        }

        #endregion
    }
}



namespace GalacticConquest
{
        public class Button : Control
        {
            #region Constants
            #endregion

            #region Variables
            Texture2D texture;

            public Color Color;
            Color currentColor;

            MouseState ms;

            public Point size;
            public string textureName;

            public ButtonState state = ButtonState.MouseOut;
            public enum ButtonState
            {
                MouseOver,
                MouseOut,
                Pressed,
                Released
            }

            #endregion

            #region Events
            public event EventHandler OnMouseOver;

            public event EventHandler OnMouseOut;

            public event EventHandler OnMousePress;

            public event EventHandler OnMouseRelease;

            #endregion

            #region Construction
            public Button(string name, string textureName, Vector2 position, Color color)//, /*Style style,*/ bool bUseStyle )
            {
                this.Type = ControlType.Button;
                this.name = name;
                this.position = position;
                this.Color = color;
                this.currentColor = color;
                this.textureName = textureName;
                this.text = text;


                #region Old Code
                //if (File.Exists(filePath + style.ToString() + "\\" + textureName + ".xnb"))
                //    texture = Game1.content.Load<Texture2D>(filePath + this.style.ToString() + "\\" + textureName);
                //else if (File.Exists(filePath + textureName + ".xnb"))
                //    texture = Game1.content.Load<Texture2D>(filePath + textureName); 
                #endregion

                if (File.Exists(Directories.UIContent + "\\" + textureName + ".xnb"))
                {
                    texture = BaseGame.ContentMgr.Load<Texture2D>(Path.Combine(Directories.UIContent, textureName));
                }
                else
                    throw new Exception("Btn TextureFile don't exist!");


                this.size = new Point(texture.Width, texture.Height);

                //OnMouseOver += new EventHandler( buttonMouseOver );
                //OnMouseOut += new EventHandler( buttonMouseOut );
                //OnMousePress += new EventHandler( buttonMousePress );
                //OnMouseRelease += new EventHandler( buttonMouseRelease );
            }
            #endregion

            #region Update

            void HandleMouseOver(Object obj, EventArgs e)
            {
                state = ButtonState.MouseOver;
                if (OnMouseOver != null)
                    OnMouseOver(obj, e);
                currentColor = ColorHelper.InterpolateColor(Color, Color.White, 0.3f);
            }

            void HandleMouseOut(Object obj, EventArgs e)
            {
                state = ButtonState.MouseOut;
                if (OnMouseOut != null)
                    OnMouseOut(obj, e);
                currentColor = Color;
            }

            void HandleMousePress(Object obj, EventArgs e)
            {
                state = ButtonState.Pressed;
                if (OnMousePress != null)
                    OnMousePress(obj, e);
                currentColor = ColorHelper.InterpolateColor(Color, Color.Black, 0.3f);
            }

            void HandleMouseRelease(Object obj, EventArgs e)
            {
                state = ButtonState.Released;
                if (OnMouseRelease != null)
                    OnMouseRelease(obj, e);
                currentColor = ColorHelper.InterpolateColor(Color, Color.White, 0.3f);
            }


            public override void Update()
            {
                if (benable)
                    UpdateButtonState();
            }

            //public override void Update ( Vector2 formPosition, Vector2 formSize )
            //{
            //    position = origin + formPosition;

            //    if (benable)
            //        UpdateButtonState();

            //    CheckVisibility( formPosition, formSize );
            //}

            private void UpdateButtonState()
            {
                ms = Mouse.GetState();

                Rectangle Border = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                if (Border.Contains(ms.X, ms.Y))
                {
                    if (state == ButtonState.MouseOut)
                    {
                        HandleMouseOver(this, EventArgs.Empty);
                    }

                    if (ms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        if (state != ButtonState.Pressed)
                        {
                            HandleMousePress(this, EventArgs.Empty);
                        }
                    }

                    if (ms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    {
                        if (state == ButtonState.Pressed)
                        {
                            HandleMouseRelease(this, EventArgs.Empty);
                        }
                    }

                }
                else
                {
                    if (state != ButtonState.MouseOut)
                    {
                        HandleMouseOut(this, EventArgs.Empty);
                    }
                }
            }

            private void CheckVisibility(Vector2 formPosition, Vector2 formSize)
            {
                if (position.X + size.X > formPosition.X + formSize.X - 15f)
                    bVisible = false;
                else if (position.Y + size.Y > formPosition.Y + formSize.Y - 25f)
                    bVisible = false;
                else
                    bVisible = true;
            }
            #endregion

            #region Public Functions
            public void MoveTo(Vector2 newPosition)
            {
                position = newPosition;
            }
            #endregion

            #region Draw
            #region Old Code
            //public void Draw ( SpriteBatch spriteBatch, Vector2 formPosition, float alpha )
            //{
            //    Color dynamicColor = new Color( new Vector4( currentColor.ToVector3(), alpha ) );
            //    position = formPosition + origin;
            //    spriteBatch.Draw( texture, position, dynamicColor );
            //} 
            #endregion

            public override void Draw(SpriteBatch spriteBatch, float alpha)
            {
                Color dynamicColor = new Color(new Vector4(currentColor.ToVector3(), alpha));
                spriteBatch.Draw(texture, position, dynamicColor);
            }
            #endregion
        }
    }

