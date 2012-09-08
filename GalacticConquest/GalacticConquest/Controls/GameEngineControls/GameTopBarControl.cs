using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.Controls.GameEngineControls
{
    public class GameTopBarControl
    {
        
        public Texture2D menuImageToDisplay;
        public Vector2 menuVector;


        public bool _isNull;
        public bool _isEnabled;


        public List<Controls.FancyButtonControl> menuButtons;

        public GameTopBarControl()
        {
            _isNull = true;
            _isEnabled = false;
            
            menuVector = Vector2.Zero;

            menuButtons = new List<FancyButtonControl>();

        }


        public void load(Texture2D galaxyToDisplay, Vector2 vectLoc)
        {
            _isNull = false;
            _isEnabled = true;

            menuImageToDisplay = galaxyToDisplay;

            menuVector = vectLoc;
        }


        public void Disable()
        {
            if (!_isNull)
            {
                _isEnabled = false;
            }
        }

        public void Enable()
        {
            if (!_isNull)
            {
                _isEnabled = true;
            }
        }



        //Draw Methods
        public void Draw(GameDrawClassComponents curGameDrawComponents)
        {
            if (!this._isNull)
            {
                curGameDrawComponents._spriteBatch.Draw(this.menuImageToDisplay, this.menuVector, Color.White);
                displayButtonsInMenu(curGameDrawComponents._spriteBatch, curGameDrawComponents._staticFonts, menuButtons);
            }
        }


        public void displayButtonsInMenu(SpriteBatch spriteBatch, Model.StaticFonts staticFonts, List<Controls.FancyButtonControl> curMenuButtons)
        {
            foreach (Controls.FancyButtonControl curButton in curMenuButtons)
            {
                if (!curButton._isNull)
                {
                    curButton.Draw(spriteBatch, staticFonts);
                }
            }

        }


        public void updateTopBarData(GameDrawClassComponents curGameDrawComponents, GameEngine.Player curPlayer, GameEngine.GalacticDayManager curGalacticDayMang)
        {
            int activeGameDayToShow = curGalacticDayMang.CurrentGalacticDay;
            //X:103 Y:10
            Vector2 curGameDayVector = new Vector2(103, 4);

            curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, activeGameDayToShow.ToString(), curGameDayVector, Color.Black);


            int rawMaterialsToShow = curPlayer.curGameResourceMang.invRawMaterials;

            Vector2 curRMsVect = new Vector2(400, 4);

            curGameDrawComponents._spriteBatch.DrawString(curGameDrawComponents._staticFonts._courierNew, rawMaterialsToShow.ToString(), curRMsVect, Color.Black);
        }

    }
}
