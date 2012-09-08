using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls.TabPanel
{
    public class TabPanelControl
    {
        public string Name;

        
        public Texture2D TabSelectedTexture;
        public Texture2D TabUnselectedTexture;

 
        public Vector2 VectorPosition;

        public List<TabPanel> tabPanels;


        public float tabHdrPxlSpacing;
        public float tabHdrTxtPxlSpacingX;
        public float tabHdrTxtPxlSpacingY;


        public bool _isNull;

        public TabPanelControl()
        {
            _isNull = true;
            Name = "";
            tabPanels = new List<TabPanel>();
            VectorPosition = Vector2.Zero;

            tabHdrPxlSpacing = 5;
            tabHdrTxtPxlSpacingX = 10;
            tabHdrTxtPxlSpacingY = 10;

        }


        //Methods


        public void setAllTabsAsUnselected()
        {
            foreach (TabPanel curTP in tabPanels)
            {
                curTP.selected = false;
            }
        }

        public TabPanel getTabByName(string tabName)
        {
            TabPanel rtnTP = new TabPanel();

            foreach (TabPanel curTP in tabPanels)
            {
                if (curTP.Name == tabName)
                    rtnTP = curTP;
            }


            return rtnTP;
        }


        public void setSelectedTabByName(string tabName)
        {
            setAllTabsAsUnselected();
            getTabByName(tabName).selected = true;
        }


        //Update Methods

        public void CheckMouseClick(GameUpdateClassComponents curGameUpdateComponents)
        {

            float tabX = VectorPosition.X;

            foreach (TabPanel curTP in tabPanels)
            {
                if (!curTP._isNull)
                {
                    Vector2 curVectorPosition = new Vector2(tabX, VectorPosition.Y);

                    Texture2D tabTexture;

                    if (curTP.selected)
                        tabTexture = TabUnselectedTexture;
                    else
                        tabTexture = TabUnselectedTexture;

                    if (curGameUpdateComponents._curMouseState.X > curVectorPosition.X && curGameUpdateComponents._curMouseState.X < (curVectorPosition.X + tabTexture.Width))
                    {
                        if (curGameUpdateComponents._curMouseState.Y > curVectorPosition.Y && curGameUpdateComponents._curMouseState.Y < (curVectorPosition.Y + tabTexture.Height))
                        {
                            if(!curTP.selected)
                                setSelectedTabByName(curTP.Name);
                        }
                    }
                }


                tabX += TabSelectedTexture.Width + tabHdrPxlSpacing;
            }
        }

        //Draw Methods
        public void Draw(GameDrawClassComponents curGameDrawComponents)
        {


            float tabX = VectorPosition.X;

            foreach (TabPanel curTP in tabPanels)
            {
                Vector2 curVectorPosition = new Vector2(tabX, VectorPosition.Y);

                Vector2 curHdrVectorPosition = curVectorPosition;
                curHdrVectorPosition.X += tabHdrTxtPxlSpacingX;
                curHdrVectorPosition.Y += tabHdrTxtPxlSpacingY;
                

                if (curTP.selected)
                {
                    Texture2D curTexture = TabSelectedTexture;
                    curTP.panel.Draw(curGameDrawComponents);
                    curGameDrawComponents._spriteBatch.Draw(curTexture, Model.Utilities.getRectFromTexture2D(curTexture, curVectorPosition), Color.White);
                    curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, curTP.TabText, curHdrVectorPosition, Color.Black);
                }
                else
                {
                    Texture2D curTexture = TabUnselectedTexture;
                    curGameDrawComponents._spriteBatch.Draw(curTexture, Model.Utilities.getRectFromTexture2D(curTexture, curVectorPosition), Color.White);
                    curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, curTP.TabText, curHdrVectorPosition, Color.Black);
                }




                tabX += TabSelectedTexture.Width + tabHdrPxlSpacing;
            }
        }

    }
}
