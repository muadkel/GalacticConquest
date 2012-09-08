using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls
{
    public class FancyDropDownBoxControl
    {
        public string ID;


        
        public DrawObj.Sprite dropDownBoxArrow;
                      
        
        public Rectangle dropDownBoxRect;

        public Color backColor;

        


        public Color borderColor;
        public int borderSize;


        public List<string> textOptions;


        public int maxItemsToShow;

        public int selectedIndex;

        public bool dropDownCollapsed;



        public bool _isEnabled;

        public FancyDropDownBoxControl()
        {
            ID = "";
            _isEnabled = false;
            dropDownBoxArrow = new DrawObj.Sprite();
            dropDownBoxRect = new Rectangle();
            backColor = Color.White;

            borderColor = Color.Black;
            borderSize = 2;

            textOptions = new List<string>();
            selectedIndex = -1;
            dropDownCollapsed = true;

            maxItemsToShow = 20;
            
        }






        public void Draw(GameDrawClassComponents curGameDrawClassComponents)
        {

            if (dropDownCollapsed)
            {
                DrawObj.DrawHelper.DrawRectangle(curGameDrawClassComponents, backColor, dropDownBoxRect, borderColor, borderSize);

                Vector2 arrowVector = new Vector2(dropDownBoxRect.X + dropDownBoxRect.Width, dropDownBoxRect.Y);
                dropDownBoxArrow.VectorPosition = arrowVector;

                dropDownBoxArrow.Draw(curGameDrawClassComponents);

                Color textColor = Color.Black;

                Vector2 textVector = new Vector2(dropDownBoxRect.X, dropDownBoxRect.Y);

                curGameDrawClassComponents._spriteBatch.DrawString(curGameDrawClassComponents._staticFonts._courierNew, textOptions[selectedIndex], textVector, textColor);

                //curGameDrawClassComponents._spriteBatch.Draw(dropDownBoxArrow.spriteTexture, Model.Utilities.getRectFromTexture2D(dropDownBoxArrow), Color.White);

            }
            else
            {
                string strTextOpts = "";
                int itemsToShow = maxItemsToShow;
                if (textOptions.Count < maxItemsToShow)
                    itemsToShow = textOptions.Count;




                for (int i = 0; i < itemsToShow; i++)
                {
                    if (i != itemsToShow - 1)
                        strTextOpts += textOptions[i] + "\n";
                    else
                        strTextOpts += textOptions[i];
                }


                //strTextOpts = textOptions[selectedIndex] + "\n" + strTextOpts;

                float textHeight = Model.Utilities.getTextHeight(strTextOpts, curGameDrawClassComponents._staticFonts);


                Rectangle expandedDropDownBoxRect = dropDownBoxRect;
                expandedDropDownBoxRect.Height = (int)textHeight;

                DrawObj.DrawHelper.DrawRectangle(curGameDrawClassComponents, backColor, expandedDropDownBoxRect, borderColor, borderSize);

                Vector2 arrowVector = new Vector2(dropDownBoxRect.X + dropDownBoxRect.Width, dropDownBoxRect.Y);
                dropDownBoxArrow.VectorPosition = arrowVector;

                dropDownBoxArrow.Draw(curGameDrawClassComponents);

                Color textColor = Color.Black;

                Vector2 textVector = new Vector2(dropDownBoxRect.X, dropDownBoxRect.Y);

                curGameDrawClassComponents._spriteBatch.DrawString(curGameDrawClassComponents._staticFonts._courierNew, strTextOpts, textVector, textColor);


            }
                  
            
        }


        //Update Methods
        // MouseClicks
        public bool mouseClicked(GameUpdateClassComponents curGameUpdateComponents)
        {
            bool blnReturn = false;


            string strTextOpts = "";
            int itemsToShow = maxItemsToShow;
            if (textOptions.Count < maxItemsToShow)
                itemsToShow = textOptions.Count;


            Vector2 startingTextOptionVector = new Vector2(dropDownBoxRect.X,dropDownBoxRect.Y);

            for (int i = 0; i < itemsToShow; i++)
            {
                float individualTextHeight = Model.Utilities.getTextHeight(textOptions[i], curGameUpdateComponents._staticFonts);

                if (curGameUpdateComponents._curMouseState.Y > startingTextOptionVector.Y && curGameUpdateComponents._curMouseState.Y < startingTextOptionVector.Y + individualTextHeight)
                {
                    if (curGameUpdateComponents._curMouseState.X > startingTextOptionVector.X && curGameUpdateComponents._curMouseState.X < startingTextOptionVector.X + dropDownBoxRect.Width)
                    {
                        selectedIndex = i;
                        dropDownCollapsed = true;
                    }
                }

                startingTextOptionVector.Y += individualTextHeight;
            }

                        
            return blnReturn;
        }

    }
}
