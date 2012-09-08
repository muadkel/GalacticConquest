using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticConquest.GameEngine
{
    public class StartMenuEngine
    {
        //Game Components - Draw, Mouse, Static Content
        //public GameUpdateClassComponents curGameUpdateComponents;
        //public GameDrawClassComponents curGameDrawComponents;


        public bool _isEnabled;

        public bool startNewGame;

        public string enteredUserName;

        public string selectedFaction;

        public Controls.StartMenuControl _startMenu;


        List<Controls.FancyMenuControl> activeStartMenuMenus;


        Controls.Textbox.TextInput.TextboxInput _txtEnterUserName;


        //Control System Values

        public bool enableStartMenu;
        public bool disableStartMenu;



        public StartMenuEngine()
        {
            //curGameUpdateComponents = new GameUpdateClassComponents();
            //curGameDrawComponents = new GameDrawClassComponents();

            enableStartMenu = false;
            disableStartMenu = false;

            _isEnabled = false;

            startNewGame = false;
            selectedFaction = "";

            _startMenu = new Controls.StartMenuControl();


            activeStartMenuMenus = new List<Controls.FancyMenuControl>();


        }


        //Methods

        public void loadStartMenuContent(Model.StaticTextureImages staticTextures, Model.StaticFonts staticFonts)
        {

            //Start Menu Button List
            List<Controls.FancyButtonControl> menuButtons = new List<Controls.FancyButtonControl>();


                //            if (curMouseState.X > 172 && curMouseState.X < 308)
                //{
                //    if (curMouseState.Y > 153 && curMouseState.Y < 182)
                //    {//Clicked new game
            
                    //            if (curMouseState.Y > 279 && curMouseState.Y < 317)
                    //{//Clicked Settings
            //if (curMouseState.Y > 359 && curMouseState.Y < 388)
            //{//Clicked Exit
            //    this.Exit();
            //}

            ////Get New Game Button Settings
            Vector2 curButtonVector = new Vector2(172, 153);
            Rectangle curButtonRect = new Rectangle(172, 153, 130, 50);

            ////Add the New Game Button
            menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnNewGame", staticTextures._buttonTexture, curButtonVector, curButtonRect, "New Game"));

            //Add the Settings Button 
            curButtonVector = new Vector2(172, 279);
            curButtonRect = new Rectangle(172, 279, 130, 50);

            menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnGameSettings", staticTextures._buttonTexture, curButtonVector, curButtonRect, "Settings"));


            //Add the Exit Button 
            curButtonVector = new Vector2(172, 359);
            curButtonRect = new Rectangle(172, 359, 130, 50);

            menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnExitGame", staticTextures._buttonTexture, curButtonVector, curButtonRect, "Exit"));

            Vector2 headerVector = new Vector2(100,20);

            _startMenu = new Controls.StartMenuControl("Galactic Conquest", staticFonts._courierNew, headerVector, staticTextures._imgBackground, new Vector2(0, 0), menuButtons);


        }



        public void loadUpTheStartMenu()
        {
            _isEnabled = true;
            _startMenu.Show();
        }



        public void checkMenuStates()
        {
            if (enableStartMenu)
            {
                _startMenu._isEnabled = true;
                enableStartMenu = false;
            }

            if (disableStartMenu)
            {
                _startMenu._isEnabled = false;
                disableStartMenu = false;
            }

        }



        //Draw Methods


        //public void drawStartMenu(SpriteBatch spriteBatch, Model.StaticFonts curStaticFonts)
        public void drawStartMenu(GameDrawClassComponents curGameDrawComponents)
        {

            checkMenuStates();


            if (_startMenu._isVisible)
            {
                curGameDrawComponents._spriteBatch.Begin();

                //Display Background Image
                //spriteBatch.Draw(_startMenu.backgroundTexture, _startMenu.backgroundVector, Color.White);

                //Display Header
                //Vector2 textVector = new Vector2(curControl.vectorPos.X + 10, curControl.vectorPos.Y + 10);
                curGameDrawComponents._spriteBatch.DrawString(_startMenu.headerFont, _startMenu.strHeaderText, _startMenu.headerVector, Color.Black);

                //Display Menu Buttons
                displayButtonsInMenu(curGameDrawComponents._spriteBatch, curGameDrawComponents._staticFonts, _startMenu.menuButtons);

                //Display Meny Radio Button Controls
                //displayRadioButtonControlsInMenu(curControl.menuRadioButtonControls);


                
                //Display Active Forms Controls
                displayActiveMenuControls(curGameDrawComponents._spriteBatch, curGameDrawComponents._staticFonts);

                curGameDrawComponents._spriteBatch.End();
            }
        }
        
        public void displayActiveMenuControls(SpriteBatch spriteBatch, Model.StaticFonts staticFonts)
        {

            foreach (Controls.FancyMenuControl curControl in activeStartMenuMenus)
            {
                if (!curControl._isNull)
                {
                    //spriteBatch.Begin();

                    curControl.Draw(spriteBatch, staticFonts);

                    //Display Menu Buttons
                    displayButtonsInMenu(spriteBatch, staticFonts,curControl.menuButtons);

                    //Display Meny Radio Button Controls
                    displayRadioButtonControlsInMenu(spriteBatch, staticFonts, curControl.menuRadioButtonControls);


                   // spriteBatch.End();
                }
            }

        }

        public void displayButtonsInMenu(SpriteBatch spriteBatch, Model.StaticFonts staticFonts, List<Controls.FancyButtonControl> menuButtons)
        {
            foreach (Controls.FancyButtonControl curButton in menuButtons)
            {
                if (!curButton._isNull)
                {
                    curButton.Draw(spriteBatch,staticFonts);
                }
            }

        }

 
        public void displayRadioButtonControlsInMenu(SpriteBatch spriteBatch, Model.StaticFonts staticFonts, List<Controls.FancyRadioButtonsControl> menuRadioButtonControls)
        {
            foreach (Controls.FancyRadioButtonsControl curRadioButtonControl in menuRadioButtonControls)
            {
                if (!curRadioButtonControl._isNull)
                {
                    //Display Radio Button Control
                    int individgiButtonCount = curRadioButtonControl.strControlText.Count;

                    float buttonX = curRadioButtonControl.vectorPos.X;
                    float buttonY = curRadioButtonControl.vectorPos.Y;

                    float pixelButtonPadding = curRadioButtonControl.pxlHeightBetweenButtons;

                    int index = 0;

                    foreach (KeyValuePair<string, string> kvp in curRadioButtonControl.strControlText)
                    {
                        Vector2 curBtnVector = new Vector2(buttonX, buttonY);

                        Texture2D curBtnTexture;

                        //Display Button Image
                        if (curRadioButtonControl._selectedIndex == index)
                            curBtnTexture = curRadioButtonControl.imgControlChecked;
                        else
                            curBtnTexture = curRadioButtonControl.imgControlUnchecked;

                        //Display Button Image
                        spriteBatch.Draw(curBtnTexture, curBtnVector, Color.White);

                        //Display Button Text
                        Vector2 textVector = new Vector2(curBtnVector.X + 10, curBtnVector.Y + 10);
                        spriteBatch.DrawString(staticFonts._courierNew, kvp.Key, textVector, Color.Black);


                        //Bump Down the next button (If there is one!)
                        buttonY += pixelButtonPadding + curBtnTexture.Height;

                        index++;
                    }


                    //Display Button Image
                    //spriteBatch.Draw(curRadioButtonControl.imgControl, curRadioButtonControl.vectorPos, Color.White);

                    //Display Button Text
                    //Vector2 textVector = new Vector2(curRadioButtonControl.vectorPos.X + 10, curRadioButtonControl.vectorPos.Y + 10);
                    //spriteBatch.DrawString(courierNew, curRadioButtonControl.strControlText, textVector, Color.Black);
                }
            }

        }



        //////////////////////////////
        //Methods for Checking Input
        //////////////////////////////
        
        public void checkStartScreenMouseClick(GameUpdateClassComponents curGameUpdateComponents)
        {
            
            //Check Start Menu Menu popups first
            checkMenuControlMouseClick(curGameUpdateComponents);

            
            //Check Textboxes Second ???
            // checks the first textbox to see if it has focus already. 
            if (_txtEnterUserName != null)
            {
                if (!this._txtEnterUserName.HasFocus)
                {
                    // it does not have focus, check to see the mouse position is contained with in the textbox. 
                    if (this._txtEnterUserName.onClick(new Point(curGameUpdateComponents._curMouseState.X, curGameUpdateComponents._curMouseState.Y)))
                    {
                        // it was contained, remove focus from other textboxes. 
                        //if (this._txtEnterUserName.HasFocus)
                        //{
                        //    this._txtEnterUserName.Blur();
                        //}
                        //if (this._txtEnterUserName.HasFocus)
                        //{
                        //    this._txtEnterUserName.Blur();
                        //}
                    }
                }

            }

            //Check Start Menu Buttons
            if (_startMenu._isEnabled)
            {
                
                
                //Run through each Button
                foreach (Controls.FancyButtonControl curButton in _startMenu.menuButtons)
                {
                    if (!curButton._isNull)
                    {//Was the button clicked?
                        if(curButton.mouseClicked(curGameUpdateComponents))
                        {
                            if (curButton.ID == "btnNewGame")
                            {//Clicked New Game

                                this._txtEnterUserName = new Controls.Textbox.TextInput.TextboxInput(curGameUpdateComponents._this, "MessageBox", "Courier New");
                                this._txtEnterUserName.Position = new Vector2(125.0f, 200.0f);
                                this._txtEnterUserName.TextboxMarginX = 25;
                                this._txtEnterUserName.TextboxMarginY = 25;
                                this._txtEnterUserName.Scale = new Vector2(0.6f);
                                this._txtEnterUserName.TextScale = new Vector2(1.5f);
                                this._txtEnterUserName.CursorOffset = new Vector2(-5 * this._txtEnterUserName.TextScale.X, -25 * this._txtEnterUserName.TextScale.Y);

                                this._txtEnterUserName.MaxTextLength = 10;

                                curGameUpdateComponents._Components.Add(this._txtEnterUserName);




                                _startMenu._isEnabled = false;

                                /////////////////////////////////
                                //Create Enter your Name Menu
                                //////////////////////////////////
                                //Texture2D curTexture = Content.Load<Texture2D>("300x300_Window");
                                Vector2 curVector = new Vector2(100, 100);
                                Rectangle curRect = new Rectangle(100, 100, 300, 300);

                                Controls.FancyMenuControl curMenuControl = new Controls.FancyMenuControl();


                                //Start Menu Button List
                                List<Controls.FancyButtonControl> menuButtons = new List<Controls.FancyButtonControl>();


                                //Get Continue Button Settings
                                Vector2 curButtonVector = new Vector2(110, 340);
                                Rectangle curButtonRect = new Rectangle(110, 340, 130, 50);

                                //Add the Continue Button
                                menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnContinueToSelectFaction", curGameUpdateComponents._staticTextureImages._buttonTexture, curButtonVector, curButtonRect, "Save"));

                                //Add the Exit Button 
                                curButtonVector = new Vector2(260, 340);
                                curButtonRect = new Rectangle(260, 340, 130, 50);

                                menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnExitUserName", curGameUpdateComponents._staticTextureImages._buttonTexture, curButtonVector, curButtonRect, "Exit"));





                                //Create the Menu Control
                                curMenuControl = new GalacticConquest.Controls.FancyMenuControl("mnuEnterUsername", curGameUpdateComponents._staticTextureImages._imgMenu, curVector, curRect, "Enter your Username: ", "...Settings List Here...", menuButtons);


                                //Add menu control to list
                                activeStartMenuMenus.Add(curMenuControl);
                            }

                            if (curButton.ID == "btnGameSettings")
                            {//Clicked Settings form
                                
                                _startMenu._isEnabled = false;

                                /////////////////////////////////
                                //Create Settings Menu
                                //////////////////////////////////
                                Texture2D curTexture = curGameUpdateComponents._Content.Load<Texture2D>("300x300_Window");
                                Vector2 curVector = new Vector2(100, 100);
                                Rectangle curRect = new Rectangle(100, 100, 300, 300);

                                Controls.FancyMenuControl curMenuControl = new Controls.FancyMenuControl();


                                //Start Menu Button List
                                List<Controls.FancyButtonControl> menuButtons = new List<Controls.FancyButtonControl>();


                                //Get Continue Button Settings
                                Vector2 curButtonVector = new Vector2(110, 340);
                                Rectangle curButtonRect = new Rectangle(110, 340, 130, 50);

                                //Add the Continue Button
                                menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnSaveSettings", curGameUpdateComponents._staticTextureImages._buttonTexture, curButtonVector, curButtonRect, "Save"));

                                //Add the Exit Button 
                                curButtonVector = new Vector2(260, 340);
                                curButtonRect = new Rectangle(260, 340, 130, 50);

                                menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnExitSettings", curGameUpdateComponents._staticTextureImages._buttonTexture, curButtonVector, curButtonRect, "Exit"));





                                //Create the Menu Control
                                curMenuControl = new GalacticConquest.Controls.FancyMenuControl("mnuSettings", curTexture, curVector, curRect, "Settings: ", "...Settings List Here...", menuButtons);


                                //Add menu control to list
                                activeStartMenuMenus.Add(curMenuControl);
                            }

                            if (curButton.ID == "btnExitGame")
                            {//Clicked Exit Game
                                curGameUpdateComponents._this.Exit();
                            }
                        
                                
                            
                        }
                    }
                }   
                
                
            }
                
        }


        public void checkMenuControlMouseClick(GameUpdateClassComponents curGameUpdateComponents)
        {
            bool _exitMenuClicked = false;
            bool _continueClicked = false;
            bool _exitGalaxyMenuClicked = false;

            bool _continueToFactionClicked = false;

            //Run through each Menu
            foreach (Controls.FancyMenuControl curMenu in activeStartMenuMenus)
            {
                if (!curMenu._isNull)
                {
                    //Run through each Button
                    foreach (Controls.FancyButtonControl curButton in curMenu.menuButtons)
                    {
                        if (!curButton._isNull)
                        {
                            if(curButton.mouseClicked(curGameUpdateComponents))
                            {
                                if (curButton.ID == "btnExitFactionMenu")
                                {//Clicked Exit Faction form
                                    _exitMenuClicked = true;
                                }

                                if (curButton.ID == "btnExitSettings")
                                {//Clicked Exit Faction form
                                    _exitMenuClicked = true;
                                }

                                if (curButton.ID == "btnExitGalaxyMenu")
                                {//Clicked Exit Galaxy
                                    _exitGalaxyMenuClicked = true;
                                }

                                if (curButton.ID == "btnContinue")
                                {//Clicked Exit Faction form
                                    _continueClicked = true;
                                }

                                if (curButton.ID == "btnExitUserName")
                                {//Clicked Exit Faction form
                                    _exitMenuClicked = true;
                                }

                                if (curButton.ID == "btnContinueToSelectFaction")
                                {//Clicked Continue to Faction form

                                    _continueToFactionClicked = true;

                                    
                                }

                                
                            }
                        }
                    }

                    //Run through each Radio Button Control
                    foreach (Controls.FancyRadioButtonsControl curRadioButtonControl in curMenu.menuRadioButtonControls)
                    {
                        if (!curRadioButtonControl._isNull)
                        {

                            int buttonCount = curRadioButtonControl.strControlText.Count;

                            float buttonX = curRadioButtonControl.vectorPos.X;
                            float buttonY = curRadioButtonControl.vectorPos.Y;

                            float pixelButtonPadding = curRadioButtonControl.pxlHeightBetweenButtons;


                            int i = 0;

                            foreach (KeyValuePair<string, string> kvp in curRadioButtonControl.strControlText)
                            {

                                if (curRadioButtonControl.mouseClicked(curGameUpdateComponents, buttonY))
                                {

                                        curRadioButtonControl._selectedIndex = i;

                                        curRadioButtonControl._selectedValue = kvp.Value;


                                        selectedFaction = kvp.Value;

                                }



                                //Bump Down the next button (If there is one!)
                                buttonY += pixelButtonPadding + curRadioButtonControl.imgControlUnchecked.Height;

                                i++;
                            }
                        }
                    }

                }
            }


            if (_exitMenuClicked)
            {
                activeStartMenuMenus.Clear();
                //_isOnStartScreen = true;
                
                
                //_txtEnterUserName = null;

                curGameUpdateComponents._Components.Remove(_txtEnterUserName);
                _txtEnterUserName = null;

                //Wrongidty Wrong wrong! You check this value again after this check dufus
                //_startMenu._isEnabled = true;
                enableStartMenu = true;


            }

            if (_continueClicked)
            {
                if (selectedFaction != "")
                {
                    activeStartMenuMenus.Clear();
                    _startMenu.Hide();
                    startNewGame = true;
                }
            }

            if (_exitGalaxyMenuClicked)
            {
                activeStartMenuMenus.Clear();
            }


            if (_continueToFactionClicked)
            {

                if (_txtEnterUserName.Text != "")
                {

                    activeStartMenuMenus.Clear();

                    enteredUserName = _txtEnterUserName.Text;


                    curGameUpdateComponents._Components.Remove(_txtEnterUserName);
                    _txtEnterUserName = null;



                    _startMenu._isEnabled = false;

                    /////////////////////////////////
                    //Create Select Faction Menu
                    //////////////////////////////////
                    Texture2D curTexture = curGameUpdateComponents._Content.Load<Texture2D>("300x300_Window");
                    Vector2 curVector = new Vector2(100, 100);
                    Rectangle curRect = new Rectangle(100, 100, 300, 300);

                    Controls.FancyMenuControl curMenuControl = new Controls.FancyMenuControl();


                    //Start Menu Button List
                    List<Controls.FancyButtonControl> menuButtons = new List<Controls.FancyButtonControl>();


                    //Get Continue Button Settings
                    Vector2 curButtonVector = new Vector2(110, 340);
                    Rectangle curButtonRect = new Rectangle(110, 340, 130, 50);

                    //Add the Continue Button
                    menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnContinue", curGameUpdateComponents._staticTextureImages._buttonTexture, curButtonVector, curButtonRect, "Continue..."));

                    //Add the Exit Button 
                    curButtonVector = new Vector2(260, 340);
                    curButtonRect = new Rectangle(260, 340, 130, 50);

                    menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnExitFactionMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, curButtonVector, curButtonRect, "Exit"));


                    

                    /////////////////////////////////
                    //Create Radio Button List
                    //////////////////////////////////
                    List<Controls.FancyRadioButtonsControl> menuRadioButtons = new List<Controls.FancyRadioButtonsControl>();


                    Texture2D curRadioUncheckedTexture = curGameUpdateComponents._Content.Load<Texture2D>("205x30_RadioButton_Unchecked");
                    Texture2D curRadioCheckedTexture = curGameUpdateComponents._Content.Load<Texture2D>("205x30_RadioButton_Checked");

                    Vector2 curRadioVector = new Vector2(110, 160);
                    Rectangle curRadioRect = new Rectangle(100, 100, 205, 30);

                    Dictionary<string, string> strFactionList = new Dictionary<string, string>();

                    strFactionList = Model.DataUtilities.getFactionsDictionaryFromXML();

                    Controls.FancyRadioButtonsControl curFRBC = new Controls.FancyRadioButtonsControl();

                    curFRBC = new Controls.FancyRadioButtonsControl("rdBtnFactionList", curRadioUncheckedTexture, curRadioCheckedTexture, 10, curRadioVector, curRadioRect, strFactionList);                    



                    menuRadioButtons.Add(curFRBC);


                    //Create the Menu Control
                    curMenuControl = new GalacticConquest.Controls.FancyMenuControl("mnuSelectFaction", curTexture, curVector, curRect, "Select a Faction: ", "...Faction List Here...", menuButtons, menuRadioButtons);

                    
                    //Add menu control to list
                    activeStartMenuMenus.Add(curMenuControl);

                }
            }

        }


    }
}
