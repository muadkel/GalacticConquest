using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class PanelControl : IDrawable
    {
        public Rectangle panelRect;
        public Color backColor;

        public Color borderColor;
        public int borderSize;


        public bool _isNull;
        public bool _isEnabled;


        public List<Controls.FancyButtonControl> menuButtons;

        public List<Controls.FancyRadioButtonsControl> menuRadioButtonControls;


        public List<Controls.DataCardIconControl> dataCards;
        public float paddingForDataCards;


        public PanelControl()
        {
            _isEnabled = false;
            _isNull = true;

            backColor = Color.White;
            borderColor = Color.Black;
            borderSize = 2;

            panelRect = new Rectangle();


            menuButtons = new List<FancyButtonControl>();
            menuRadioButtonControls = new List<FancyRadioButtonsControl>();
            dataCards = new List<DataCardIconControl>();


            paddingForDataCards = 15;
        }




        //Draw Commands

        public void Draw(GameDrawClassComponents curGameDrawClassComponents)
        {

            if (!_isNull)
            {
                Texture2D t = new Texture2D(curGameDrawClassComponents._graphicsDevice, 1, 1);
                t.SetData(new[] { backColor });


                curGameDrawClassComponents._spriteBatch.Draw(t, panelRect, backColor);


                //Draw Border
                curGameDrawClassComponents._spriteBatch.Draw(t, new Rectangle(panelRect.Left, panelRect.Top, borderSize, panelRect.Height), borderColor); // Left
                curGameDrawClassComponents._spriteBatch.Draw(t, new Rectangle(panelRect.Right, panelRect.Top, borderSize, panelRect.Height), borderColor); // Right
                curGameDrawClassComponents._spriteBatch.Draw(t, new Rectangle(panelRect.Left, panelRect.Top, panelRect.Width, borderSize), borderColor); // Top
                curGameDrawClassComponents._spriteBatch.Draw(t, new Rectangle(panelRect.Left, panelRect.Bottom, panelRect.Width, borderSize), borderColor); // Bottom



                DrawDataCardIcons(curGameDrawClassComponents);


                displayButtonsInMenu(curGameDrawClassComponents);

            }

            
        }


        public void DrawDataCardIcons(GameDrawClassComponents curGameDrawClassComponents)
        {
            Vector2 originalStartingVector = new Vector2(panelRect.X+paddingForDataCards, panelRect.Y+paddingForDataCards);
            Vector2 startingVector = new Vector2(panelRect.X+paddingForDataCards, panelRect.Y+paddingForDataCards);
            Rectangle curRect = new Rectangle();

            int counter = 1;

            foreach (Controls.DataCardIconControl curDataCard in dataCards)
            {
                curRect = curDataCard.rectControl;

                curRect.X = (int)startingVector.X;
                curRect.Y = (int)startingVector.Y;
                

                
                curGameDrawClassComponents._spriteBatch.Draw(curDataCard.imgControl, curRect, Color.White);

                Vector2 curVector = startingVector;
                curVector.Y += curDataCard.imgControl.Height + paddingForDataCards;

                /////////////////////
                //Make text drop-down based on image width//
                string displayText = Model.Utilities.parseText(curDataCard.strControlText,curRect.Width,curGameDrawClassComponents._staticFonts);

                curGameDrawClassComponents._spriteBatch.DrawString(curGameDrawClassComponents._staticFonts._courierNew, displayText, curVector, Color.White);

                ////////////////////////////
                //Loop back around with updated x coords :D
                startingVector.X += curDataCard.imgControl.Width + paddingForDataCards;
                
                if(counter == 5)
                {
                    //Reset counter
                    counter = 0;

                    //Move Y
                    startingVector.Y += curDataCard.imgControl.Height + 100;
                    //Reset X
                    startingVector.X = originalStartingVector.X;
                }




                counter++;
            }

        }




        public void displayButtonsInMenu(GameDrawClassComponents curGameDrawClassComponents)
        {
            foreach (Controls.FancyButtonControl curButton in menuButtons)
            {
                if (!curButton._isNull)
                {
                    curButton.Draw(curGameDrawClassComponents._spriteBatch, curGameDrawClassComponents._staticFonts);
                }
            }

        }



    }
}
