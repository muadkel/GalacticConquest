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
    public class GalacticGameEngine
    {
        //Basic Game information
        public string ID;
        public string Name;
        public bool _gameIsRunning;

        public GameEngine.Player Player1;


        public GalacticDayManager curGalacticDayMang;

        public GameUniverse curGameUniverse;

        public Model.StaticDataCards staticDataCards; //The data cards represent almost every unit or just anything with basic information


        //////////////////////////////////////////////////////////////////
        //In-Game Menus///////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////

        public Controls.GameEngineControls.GalacticMapControl MainMapControl;
        public Controls.GameEngineControls.GameTopBarControl TopBarControl;
        public Controls.GameEngineControls.GameRightBarControl RightBarControl;



        //////////////////////////////////////////////////////////////////
        //Pop up Menus
        public Controls.PlanetsInGalaxyMenu LeftPlanetMenu;
        public Controls.PlanetsInGalaxyMenu RightPlanetMenu;

        public Controls.PlanetOrbitControl LeftPlanetOrbitMenu;
        public Controls.PlanetOrbitControl RightPlanetOrbitMenu;

        public Controls.GameInformationMenu InfoMenu;

        public Controls.PlanetCommandControl PlanetCommandMenu;

        public Controls.GameEngineControls.ConstructionMenuControl ConstructionMenu;
        //////////////////////////////////////////////////////////////////


        //Constructors
        public GalacticGameEngine()
        {
            ID = "";
            Name = "";
            Player1 = new Player();

            _gameIsRunning = false;

            curGalacticDayMang = new GalacticDayManager();

            curGameUniverse = new GameUniverse();


            MainMapControl = new Controls.GameEngineControls.GalacticMapControl();
            TopBarControl = new Controls.GameEngineControls.GameTopBarControl();
            RightBarControl = new Controls.GameEngineControls.GameRightBarControl();
            
            LeftPlanetMenu = new Controls.PlanetsInGalaxyMenu();
            RightPlanetMenu = new Controls.PlanetsInGalaxyMenu();
            LeftPlanetOrbitMenu = new Controls.PlanetOrbitControl();
            
            InfoMenu = new Controls.GameInformationMenu();
            PlanetCommandMenu = new Controls.PlanetCommandControl();
            
            ConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl();
        }




        //Methods
        public void startNewGame(string gameID, string gameName, string userName, string factionName, GameUpdateClassComponents curGameUpdateClassComponents)
        {
            ID = gameID;
            Name = gameName;
            _gameIsRunning = true;

            //Start at galactic day 1
            curGalacticDayMang.CurrentGalacticDay = 1;

            //Create player 1. Give him 5 resources for the hell of it...
            Player1 = new Player(userName, factionName, 5);

            //Build the Galaxies and planets from the XML file
            curGameUniverse.TheUniverse = Model.DataUtilities.getGalaxiesFromXML();



            buildGameControls(curGameUpdateClassComponents);
            
            
            //Get all the units from XML files
            staticDataCards = new Model.StaticDataCards();



            GameEngine.StartingGameUnits startingUnits = Model.DataUtilities.getStartingSettingsFromXML(factionName, staticDataCards);
            Player1.curGameResourceMang.invRawMaterials = startingUnits.startingResources;


            curGameUniverse.getPlanetByName("Coruscant").Owner = new GameEngine.GalacticComponents.Owner(Player1);

            foreach (GameEngine.PlayerInvObjs.InvUnit curInvObj in startingUnits.startingUnits)
            {
                curInvObj.FacilityOwner = new GameEngine.GalacticComponents.Owner(Player1);
                
                //curInvObj.iuFacility._underConstruction = false;
                    //.player = Player1;

                if (curInvObj.invUnitType == Model.DataCardType.Facility)
                {//Add Facilities to the ground
                    curGameUniverse.getPlanetByName(curInvObj.startingLocation).GroundSpaces.Facilities.Add(curInvObj);
                }
                else if (curInvObj.invUnitType == Model.DataCardType.Ship)
                {//Add Ships to the orbit
                    curGameUniverse.getPlanetByName(curInvObj.startingLocation).Orbit.StarshipFleetsInOrbit.Add(curInvObj);
                }
                else if (curInvObj.invUnitType == Model.DataCardType.Troops)
                {//Add Troops to the ground
                    curGameUniverse.getPlanetByName(curInvObj.startingLocation).GroundSpaces.Troops.Add(curInvObj);
                }
                //Player1.playerUnits.Add(new GalacticConquest.GameEngine.PlayerInvObjs.InvUnit(curFacil));

            }

            //Player1.playerUnits

            //DataCards.GameUnit curUnit = new GalacticConquest.DataCards.Ship();

            

        }


        //Setup Game Controls
        public void buildGameControls(GameUpdateClassComponents curGameUpdateClassComponents)
        {
            //Build Galaxy Map
            Texture2D curTexture = curGameUpdateClassComponents._staticTextureImages._galacticMap;

            float middleWidth = curGameUpdateClassComponents._screenWidth / 2;
            float middleHeight = curGameUpdateClassComponents._screenHeight / 2;

            float middleBoxX = middleWidth - (curTexture.Width / 2);
            float middleBoxY = middleHeight - (curTexture.Height / 2);

            Vector2 curVector = new Vector2(middleBoxX, middleBoxY);

            MainMapControl.loadMap(curTexture, curVector);

            
            Texture2D curTopBar = curGameUpdateClassComponents._staticTextureImages._gameTopBar;

            Vector2 curTopBarVector = new Vector2(0, 0);

            TopBarControl.load(curTopBar, curTopBarVector);


            Vector2 btnDayAdvVector = new Vector2(170, 3);
            Rectangle rectDayAdv = new Rectangle((int)btnDayAdvVector.X, (int)btnDayAdvVector.Y, 30, 30);

            Controls.FancyButtonControl btnDayAdvance = new Controls.FancyButtonControl();
            btnDayAdvance = new Controls.FancyButtonControl("btnAdvanceDay", curGameUpdateClassComponents._staticTextureImages._gameTopBarDayAdvance, btnDayAdvVector, rectDayAdv, "");

            TopBarControl.menuButtons.Add(btnDayAdvance);


            Texture2D curRightBar = curGameUpdateClassComponents._staticTextureImages._gameRightBar;

            Vector2 curRightBarVector = new Vector2(862, 65);

            RightBarControl.load(curRightBar, curRightBarVector);


            //curGameDrawComponents._spriteBatch.Begin();

            //curGameDrawComponents._spriteBatch.Draw(curTopBar, curTopBarVector, Color.White);
            //curGameDrawComponents._spriteBatch.Draw(curRightBar, curRightBarVector, Color.White);
            //curGameDrawComponents._spriteBatch.Draw(curTexture, curVector, Color.White);


            //updateTopBarData(curGameDrawComponents._spriteBatch, curGameDrawComponents._staticFonts);


        }



        //Disable All Menus
        public void disableAllControls()
        {
            MainMapControl.Disable();
            //Disable these?
            //TopBarControl.Disable();
            RightBarControl.Disable();

            LeftPlanetMenu.Disable();
            RightPlanetMenu.Disable();
            LeftPlanetOrbitMenu.Disable();
                
            InfoMenu.Disable();
            PlanetCommandMenu.Disable();
        }



        //////////////////////
        //Draw Methods
        public void displayGameScreen(GameDrawClassComponents curGameDrawComponents)
        {
           
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            //Start the Sprite Batch drawing// All draw methods rely on this Begin process. 
            curGameDrawComponents._spriteBatch.Begin();


            //Draw Galactic Map
            MainMapControl.Draw(curGameDrawComponents);

            TopBarControl.Draw(curGameDrawComponents);
            TopBarControl.updateTopBarData(curGameDrawComponents, Player1, curGalacticDayMang);

            RightBarControl.Draw(curGameDrawComponents);


            updateGalaxyPlanetForms(curGameDrawComponents);
            

            displayInfoMenu(curGameDrawComponents);

            
            displayPlanetCommandMenu(curGameDrawComponents);


            displayConstructionMenu(curGameDrawComponents);
            
            
            curGameDrawComponents._spriteBatch.End();
            //End the Sprite Batch drawing// All draw methods rely on the Begin process and this End cleanup.
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            


        }

        public void displayConstructionMenu(GameDrawClassComponents curGameDrawComponents)
        {
            curGameDrawComponents._staticDataCards = staticDataCards;
            ConstructionMenu.Draw(curGameDrawComponents);
        }


        public void displayInfoMenu(GameDrawClassComponents curGameDrawComponents)
        {

            Controls.GameInformationMenu curInfoMenu = this.InfoMenu;

            if (!curInfoMenu._isNull)
            {
                curInfoMenu.Draw(curGameDrawComponents);
            }


        }


        public void displayPlanetCommandMenu(GameDrawClassComponents curGameDrawComponents)
        {

            Controls.PlanetCommandControl curPlanetCommandMenu = this.PlanetCommandMenu;

            if (!curPlanetCommandMenu._isNull)
            {
                curPlanetCommandMenu.Draw(curGameDrawComponents);
                                
            }


        }

        public void updateGalaxyPlanetForms(GameDrawClassComponents curGameDrawComponents)
        {

            Controls.PlanetsInGalaxyMenu LeftGPMenu = this.LeftPlanetMenu;

            if (!LeftGPMenu._isNull)
            {
                LeftGPMenu.Draw(curGameDrawComponents);
            }



            Controls.PlanetOrbitControl LeftOrbitMenu = this.LeftPlanetOrbitMenu;

            if (!LeftOrbitMenu._isNull)
            {

                LeftOrbitMenu.Draw(curGameDrawComponents, curGameUniverse.getAllPlanetsList());
            }


        }


        public void displayButtonsInMenu(SpriteBatch spriteBatch, Model.StaticFonts staticFonts, List<Controls.FancyButtonControl> menuButtons)
        {
            foreach (Controls.FancyButtonControl curButton in menuButtons)
            {
                if (!curButton._isNull)
                {
                    curButton.Draw(spriteBatch, staticFonts);
                }
            }

        }




        public void CreatePlanetInfoMenu(GameUpdateClassComponents curGameUpdateComponents, DataCards.Planet curPlanet)
        {
            //Create a Info Menu

            //Disable other controls
            this.disableAllControls();


            Controls.GameInformationMenu newGameInfoMenu = new Controls.GameInformationMenu();

            newGameInfoMenu.dataCardType = Model.DataCardType.Planet;

            newGameInfoMenu.imageToDisplay = curGameUpdateComponents._staticTextureImages._infoMenu;
            newGameInfoMenu.menuVector = new Vector2(100, 100);
            newGameInfoMenu.HeaderText = curPlanet.HeaderText;
            newGameInfoMenu.Description = curPlanet.Description;
            newGameInfoMenu.tag = curPlanet;
            
            newGameInfoMenu.Name = "menuInfo";
            newGameInfoMenu._isEnabled = true;
            newGameInfoMenu._isNull = false;



            //Add the Scroll Panel
            Vector2 scrollPanelVector = new Vector2(550, 175);

            int spWidth = 320;
            int spHeight = 480;
            Rectangle scrollPanelRect = new Rectangle(Convert.ToInt32(scrollPanelVector.X), Convert.ToInt32(scrollPanelVector.Y), spWidth, spHeight);

            Controls.ScrollPanel.ScrollPanelTextControl scrlPnlDescription = new Controls.ScrollPanel.ScrollPanelTextControl(curGameUpdateComponents._staticTextureImages._scrollingPanel, scrollPanelVector, scrollPanelRect);
            newGameInfoMenu.scrlPnlDescriptionText = scrlPnlDescription;




            //Add the Exit Button 
            newGameInfoMenu.menuButtons.Add(new Controls.FancyButtonControl("btnExitInfoMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, new Vector2(365, 600), new Rectangle(365, 650, 130, 50), "Exit"));


            this.InfoMenu = newGameInfoMenu;

        }

        public void CreateOrbitCommandMenu(GameUpdateClassComponents curGameUpdateComponents, DataCards.Planet curPlanet)
        {
            //Create a Orbit Command Menu

            //Disable other controls
            this.disableAllControls();


            Rectangle myAvgRect = new Rectangle(300, 200, 600, 400);

            List<GameEngine.PlayerInvObjs.InvUnit> shipInvUnits = new List<GameEngine.PlayerInvObjs.InvUnit>();


            shipInvUnits = curGameUniverse.getPlanetByName(curPlanet.Name).Orbit.StarshipFleetsInOrbit;

            List<Controls.DataCardIconControl> dataCards = new List<GalacticConquest.Controls.DataCardIconControl>();




            foreach (GameEngine.PlayerInvObjs.InvUnit curIU in shipInvUnits)
            {
                Controls.DataCardIconControl curDataCard = new GalacticConquest.Controls.DataCardIconControl();


                //If Under construction@!!/////
                if (curIU._underConstruction)
                {
                    curDataCard.imgControl = curGameUpdateComponents._staticTextureImages._underConstructionFacility;
                    curDataCard.strControlText = curIU.iuShip.HeaderText + " (U/C " + curIU._remainingConstructionDays.ToString() + " days left)";
                }
                else
                {
                    curDataCard.imgControl = Texture2D.FromFile(curGameUpdateComponents._graphicsDevice, Model.DataUtilities._ShipsImagePath + curIU.iuShip.Name + ".jpg");
                    curDataCard.strControlText = curIU.iuShip.HeaderText;
                }
                Rectangle curDataCardRect = new Rectangle(myAvgRect.X + 10, myAvgRect.Y + 10, curDataCard.imgControl.Width, curDataCard.imgControl.Height);

                curDataCard.rectControl = curDataCardRect;

                curDataCard._isNull = false;


                dataCards.Add(curDataCard);
            }


            /////////////////////////////////
            //Create Orbit Planet Menu
            //////////////////////////////////

            Texture2D curTexture = curGameUpdateComponents._staticTextureImages._orbitCommandMenu;
            //Vector2 curVector = new Vector2(100, 100);
            //Rectangle curRect = new Rectangle(100, 100, 300, 300);

            Controls.PlanetOrbitControl curMenuControl = new Controls.PlanetOrbitControl();


            //Start Menu Button List
            List<Controls.FancyButtonControl> menuButtons = new List<Controls.FancyButtonControl>();


            //Get Continue Button Settings
            Texture2D curButtonTexture = curGameUpdateComponents._staticTextureImages._buttonTexture;
            Vector2 curButtonVector = new Vector2(110, 340);
            Rectangle curButtonRect = new Rectangle(110, 340, 130, 50);

            //Add the Continue Button
            //menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnSaveSettings", curButtonTexture, curButtonVector, curButtonRect, "Save"));

            //Add the Exit Button 
            curButtonVector = new Vector2(365, 650);
            curButtonRect = new Rectangle(365, 650, 130, 50);

            menuButtons.Add(new Controls.FancyButtonControl("btnExitOrbitMenu", curButtonTexture, curButtonVector, curButtonRect, "Exit"));

            
                        
            
            //Create the Menu Control
            curMenuControl = new Controls.PlanetOrbitControl("mnuOrbit", curPlanet.Name, curTexture, Model.OrientationType.Left, curPlanet.Name, "...Settings List Here...", menuButtons, curGameUniverse.getAllPlanetsList(), curGameUpdateComponents, this.curGameUniverse.getPlanetByName(curPlanet.Name).Orbit.StarshipFleetsInOrbit);

            


            this.LeftPlanetOrbitMenu = curMenuControl;

            //Add menu control to list
            //activeImgMenus.Add(curMenuControl);



        }

        public void CreatePlanetCommandMenu(GameUpdateClassComponents curGameUpdateComponents, DataCards.Planet curPlanet)
        {
            //Create a Planet Command Menu

            //Disable other controls
            this.disableAllControls();





            Rectangle myAvgRect = new Rectangle(300, 200, 600, 400);

            List<GameEngine.PlayerInvObjs.InvUnit> facilityInvUnits = new List<GameEngine.PlayerInvObjs.InvUnit>();
            facilityInvUnits = curGameUniverse.getPlanetByName(curPlanet.Name).GroundSpaces.Facilities;

            List<GameEngine.PlayerInvObjs.InvUnit> troopsInvUnits = new List<GameEngine.PlayerInvObjs.InvUnit>();
            troopsInvUnits = curGameUniverse.getPlanetByName(curPlanet.Name).GroundSpaces.Troops;



            List<Controls.DataCardIconControl> facilityDataCards = new List<GalacticConquest.Controls.DataCardIconControl>();
            List<Controls.DataCardIconControl> troopsDataCards = new List<GalacticConquest.Controls.DataCardIconControl>();


            foreach (GameEngine.PlayerInvObjs.InvUnit curIU in facilityInvUnits)
            {
                Controls.DataCardIconControl curDataCard = new GalacticConquest.Controls.DataCardIconControl();

                //If Under construction@!!/////
                if (curIU._underConstruction)
                {
                    curDataCard.imgControl = curGameUpdateComponents._staticTextureImages._underConstructionFacility;
                    curDataCard.strControlText = curIU.iuFacility.HeaderText + " (U/C " + curIU._remainingConstructionDays.ToString() + " days left)";
                }
                else
                {
                    curDataCard.imgControl = Texture2D.FromFile(curGameUpdateComponents._graphicsDevice, Model.DataUtilities._FacilitiesImagePath + curIU.iuFacility.Name + ".jpg");
                    curDataCard.strControlText = curIU.iuFacility.HeaderText;
                }
                Rectangle curDataCardRect = new Rectangle(myAvgRect.X + 10, myAvgRect.Y + 10, curDataCard.imgControl.Width, curDataCard.imgControl.Height);
                curDataCard.rectControl = curDataCardRect;
                curDataCard._isNull = false;

                facilityDataCards.Add(curDataCard);
            }

            foreach (GameEngine.PlayerInvObjs.InvUnit curIU in troopsInvUnits)
            {
                Controls.DataCardIconControl curDataCard = new GalacticConquest.Controls.DataCardIconControl();

                //If Under construction@!!/////
                if (curIU._underConstruction)
                {
                    curDataCard.imgControl = curGameUpdateComponents._staticTextureImages._underConstructionFacility; // Use a troops Under Construction image when you get one...
                    curDataCard.strControlText = curIU.iuTroops.HeaderText + " (U/C " + curIU._remainingConstructionDays.ToString() + " days left)";
                }
                else
                {
                    curDataCard.imgControl = Texture2D.FromFile(curGameUpdateComponents._graphicsDevice, Model.DataUtilities._TroopsImagePath + curIU.iuTroops.Name + ".jpg");
                    curDataCard.strControlText = curIU.iuTroops.HeaderText;
                }
                Rectangle curDataCardRect = new Rectangle(myAvgRect.X + 10, myAvgRect.Y + 10, curDataCard.imgControl.Width, curDataCard.imgControl.Height);
                curDataCard.rectControl = curDataCardRect;
                curDataCard._isNull = false;

                troopsDataCards.Add(curDataCard);
            }

            Controls.PlanetCommandControl newPlanetCmdMenu = new Controls.PlanetCommandControl();


            newPlanetCmdMenu.menuImageToDisplay = curGameUpdateComponents._staticTextureImages._planetCommandMenu;

            newPlanetCmdMenu.menuVector = new Vector2(50, 100);
            newPlanetCmdMenu.HeaderText = curPlanet.HeaderText;
            newPlanetCmdMenu.Description = curPlanet.Description;
            newPlanetCmdMenu.Name = "plntCmdMenu";
            newPlanetCmdMenu.tag = curPlanet;
            newPlanetCmdMenu.SelectedPlanetName = curPlanet.Name;
            newPlanetCmdMenu._isEnabled = true;
            newPlanetCmdMenu._isNull = false;



            
            ///////////////////////////////////////////////////////////////////
            //Create Tab Control
            Controls.TabPanel.TabPanelControl tabControl = new Controls.TabPanel.TabPanelControl();


            tabControl.TabSelectedTexture = curGameUpdateComponents._staticTextureImages._tabSelected;
            tabControl.TabUnselectedTexture = curGameUpdateComponents._staticTextureImages._tabUnselected;
            tabControl.VectorPosition = new Vector2(300,157);



            Controls.TabPanel.TabPanel tabPanelCmd = new Controls.TabPanel.TabPanel();
            Controls.PanelControl btnPanel = new Controls.PanelControl();

            btnPanel._isEnabled = true;
            btnPanel._isNull = false;
            btnPanel.backColor = Color.LightSalmon;
            btnPanel.borderColor = Color.Black;
            btnPanel.borderSize = 2;
            btnPanel.panelRect = myAvgRect;


            if (curPlanet.GroundSpaces.hasFacilityMFR()) //If the planet has a manufactoring facility then let them build
            {
                Controls.FancyButtonControl btnFacilityMFR = new Controls.FancyButtonControl();

                Vector2 btnFacilVector = new Vector2(350,270);

                Rectangle btnFacilRect = new Rectangle((int)btnFacilVector.X, (int)btnFacilVector.Y, curGameUpdateComponents._staticTextureImages._buttonTexture.Width, curGameUpdateComponents._staticTextureImages._buttonTexture.Height);

                btnFacilityMFR = new Controls.FancyButtonControl("btnFacilityMFR", curGameUpdateComponents._staticTextureImages._buttonTexture, btnFacilVector, btnFacilRect, "Construct Buildings");

                btnPanel.menuButtons.Add(btnFacilityMFR);
            }
            if (curPlanet.GroundSpaces.hasShipMFR())//If the planet has a ship building facility then let them build
            {
                Controls.FancyButtonControl btnShipMFR = new Controls.FancyButtonControl();

                Vector2 btnFacilVector = new Vector2(curGameUpdateComponents._staticTextureImages._buttonTexture.Width + 350 + 75, 270);

                Rectangle rectShipMFR = new Rectangle((int)btnFacilVector.X, (int)btnFacilVector.Y, curGameUpdateComponents._staticTextureImages._buttonTexture.Width, curGameUpdateComponents._staticTextureImages._buttonTexture.Height);

                btnShipMFR = new Controls.FancyButtonControl("btnShipMFR", curGameUpdateComponents._staticTextureImages._buttonTexture, btnFacilVector, rectShipMFR, "Construct Ships");

                btnPanel.menuButtons.Add(btnShipMFR);
            }
            if (curPlanet.GroundSpaces.hasTroopsMFR())//If the planet has a troops building facility then let them build
            {
                Controls.FancyButtonControl btnTroopsMFR = new Controls.FancyButtonControl();

                Vector2 btnFacilVector = new Vector2(350, curGameUpdateComponents._staticTextureImages._buttonTexture.Height + 270 + 50);

                Rectangle rectTroopsMFR = new Rectangle((int)btnFacilVector.X, (int)btnFacilVector.Y, curGameUpdateComponents._staticTextureImages._buttonTexture.Width, curGameUpdateComponents._staticTextureImages._buttonTexture.Height);

                btnTroopsMFR = new Controls.FancyButtonControl("btnTroopsMFR", curGameUpdateComponents._staticTextureImages._buttonTexture, btnFacilVector, rectTroopsMFR, "Construct Troops");

                btnPanel.menuButtons.Add(btnTroopsMFR);
            }
            if (curPlanet.GroundSpaces.hasTroops() && curPlanet.Orbit.hasShipsInOrbit())//If the planet has troops and ship then let them move them
            {
                Controls.FancyButtonControl btnTroops = new Controls.FancyButtonControl();

                Vector2 btnFacilVector = new Vector2(curGameUpdateComponents._staticTextureImages._buttonTexture.Width + 350 + 75, curGameUpdateComponents._staticTextureImages._buttonTexture.Height + 270 + 50);

                Rectangle rectTroopsMFR = new Rectangle((int)btnFacilVector.X, (int)btnFacilVector.Y, curGameUpdateComponents._staticTextureImages._buttonTexture.Width, curGameUpdateComponents._staticTextureImages._buttonTexture.Height);

                btnTroops = new Controls.FancyButtonControl("btnTroops", curGameUpdateComponents._staticTextureImages._buttonTexture, btnFacilVector, rectTroopsMFR, "Move Troops");

                btnPanel.menuButtons.Add(btnTroops);
            }
            //btnPanel.

            tabPanelCmd.Name = "Command";
            tabPanelCmd.TabText = "Command";
            tabPanelCmd.selected = true;

            tabPanelCmd.panel = btnPanel;
            tabPanelCmd._isNull = false;

            Controls.TabPanel.TabPanel tabPanelFacility = new Controls.TabPanel.TabPanel();
            Controls.PanelControl facilityPanel = new Controls.PanelControl();

            facilityPanel._isEnabled = true;
            facilityPanel._isNull = false;
            facilityPanel.backColor = Color.LightGreen;
            facilityPanel.borderColor = Color.Black;
            facilityPanel.borderSize = 2;
            facilityPanel.panelRect = myAvgRect;

            //Add Facility DataCards
            //facilityPanel.


            facilityPanel.dataCards = facilityDataCards;
        
            tabPanelFacility.Name = "Facilities";
            tabPanelFacility.TabText = "Facilities";

                       


            tabPanelFacility.panel = facilityPanel;
            tabPanelFacility._isNull = false;




            Controls.TabPanel.TabPanel tabPanelTroops = new Controls.TabPanel.TabPanel();
            Controls.PanelControl troopsPanel = new Controls.PanelControl();

            troopsPanel._isEnabled = true;
            troopsPanel._isNull = false;
            troopsPanel.backColor = Color.Gray;
            troopsPanel.borderColor = Color.Red;
            troopsPanel.borderSize = 2;
            troopsPanel.panelRect = myAvgRect;

            troopsPanel.dataCards = troopsDataCards;

            tabPanelTroops.Name = "Troops";
            tabPanelTroops.TabText = "Troops";

            tabPanelTroops.panel = troopsPanel;
            tabPanelTroops._isNull = false;


            //Insert all panels intoTab control
            tabControl.tabPanels.Add(tabPanelCmd);
            tabControl.tabPanels.Add(tabPanelFacility);
            tabControl.tabPanels.Add(tabPanelTroops);


            //Add the Tab Control
            newPlanetCmdMenu.tabControl = tabControl;

            //Add the Exit Button 
            newPlanetCmdMenu.menuButtons.Add(new Controls.FancyButtonControl("btnExitPlanetCmdMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, new Vector2(800, 680), new Rectangle(800, 680, 130, 50), "Exit"));


            this.PlanetCommandMenu = newPlanetCmdMenu;
        }



        ////////////////////////////////////////////
        //Check Mouse Clicks
        public void checkGameRunningMouseClick(GameUpdateClassComponents curGameUpdateComponents)
        {

            if (this._gameIsRunning)
            {                
                ///////////////////////////////////////
                //Check Orbit Planet Menus
                //////////////////////////////////////

                //Check Orbit Menu
                if (!this.LeftPlanetOrbitMenu._isNull && this.LeftPlanetOrbitMenu._isEnabled)
                {
                    bool blnExit = false;



                    //foreach(Controls.FancyDropDownBoxControl curDDB in this.LeftPlanetOrbitMenu.menuDropDownBoxControls)
                    //{
                        for (int i = 0; i < this.LeftPlanetOrbitMenu.menuDropDownBoxControls.Count; i++)
                        {

                            if (LeftPlanetOrbitMenu.menuDropDownBoxControls[i]._isEnabled)
                            {
                                if (LeftPlanetOrbitMenu.menuDropDownBoxControls[i].dropDownBoxArrow.mouseClicked(curGameUpdateComponents))
                                {
                                    if (LeftPlanetOrbitMenu.menuDropDownBoxControls[i].dropDownCollapsed)
                                        LeftPlanetOrbitMenu.menuDropDownBoxControls[i].dropDownCollapsed = false;
                                    else
                                        LeftPlanetOrbitMenu.menuDropDownBoxControls[i].dropDownCollapsed = true;
                                }

                                if (!LeftPlanetOrbitMenu.menuDropDownBoxControls[i].dropDownCollapsed)
                                {
                                    LeftPlanetOrbitMenu.menuDropDownBoxControls[i].mouseClicked(curGameUpdateComponents);
                                }
                            }
                        }
                    //}

                        bool blnOrbitLeftClose = false;
                        int menuInc = 0;
                    //Run through each Button
                    foreach (Controls.FancyButtonControl curButton in this.LeftPlanetOrbitMenu.menuButtons)
                    {
                        if (!curButton._isNull)
                        {
                            if (curButton.mouseClicked(curGameUpdateComponents))
                            {
                                if (curButton.ID == "btnExitOrbitMenu")
                                {//Clicked Exit Galaxy
                                    blnOrbitLeftClose = true;
                                }

                                if (curButton.ID.Length > "btnSendShip".Length)
                                {
                                    if (curButton.ID.Substring(0, "btnSendShip".Length) == "btnSendShip")
                                    {
                                        int invUnitID = Convert.ToInt32(curButton.ID.Replace("btnSendShip", ""));

                                        string originalPlanetName = LeftPlanetOrbitMenu.strPlanetName;

                                        GameEngine.PlayerInvObjs.InvUnit myInv = curGameUniverse.getInvUnitByID(invUnitID, Model.DataCardType.Ship);

                                        string desitiantionPlanetName = LeftPlanetOrbitMenu.getDestinationPlanetNameByID(invUnitID);

                                        DataCards.Planet oriPlnt = curGameUniverse.getPlanetByName(originalPlanetName);

                                        DataCards.Planet destPlnt = curGameUniverse.getPlanetByName(desitiantionPlanetName);

                                        


                                        oriPlnt.Orbit.removeShipFromOrbitByID(myInv.id);

                                        //Takes 5 days to get anywhere!
                                        myInv.transitObj.setTravelTime(5);

                                        destPlnt.Orbit.StarshipFleetsInOrbit.Add(myInv);

                                        LeftPlanetOrbitMenu.menuButtons.RemoveAt(menuInc);

                                        blnOrbitLeftClose = true;

                                        break;
                                        //myInv.
                                    }
                                }
                                //if (curButton.ID == "btnExitPlanetCmdMenu")
                                //{//Clicked Exit Planet Command
                                //    exitPlntCmdMenu = true;
                                //}

                                //if (curButton.ID == "btnContinue")
                                //{//Clicked Exit Faction form
                                //    _continueClicked = true;
                                //}



                            }
                        }
                        menuInc++;
                    }



                       



                    if (blnOrbitLeftClose)
                    {
                        this.LeftPlanetOrbitMenu = new Controls.PlanetOrbitControl();
                        this.LeftPlanetMenu.Enable();
                        this.RightPlanetMenu.Enable();
                    }
                }




                ///////////////////////////////////////
                //Check Galaxy Planet Menus
                //////////////////////////////////////
                //Check Left Menu
                if (!this.LeftPlanetMenu._isNull && this.LeftPlanetMenu._isEnabled)
                {
                    bool exitLeftGPMenu = false;
                    bool exitRightGPMenu = false;

                    //Run through each Button
                    foreach (Controls.FancyButtonControl curButton in this.LeftPlanetMenu.menuButtons)
                    {
                        if (!curButton._isNull)
                        {
                            if (curButton.mouseClicked(curGameUpdateComponents))
                            {
                                if (curButton.ID == "btnExitGalaxyMenu")
                                {//Clicked Exit Galaxy
                                    exitLeftGPMenu = true;
                                }

                                //if (curButton.ID == "btnContinue")
                                //{//Clicked Exit Faction form
                                //    _continueClicked = true;
                                //}


                                
                            }
                        }
                    }

                    float startingPlanetY = this.LeftPlanetMenu.vectorPos.Y + 50;

                    float infoButtonX = Model.StaticValues.xPaddingForInfoBuutton + this.LeftPlanetMenu.vectorPos.X;
                    float commandButtonX = Model.StaticValues.xPaddingForCommandButton + this.LeftPlanetMenu.vectorPos.X;
                    float orbitButtonX = Model.StaticValues.xPaddingForOrbitButton + this.LeftPlanetMenu.vectorPos.X;


                    foreach (DataCards.Planet curPlanet in this.LeftPlanetMenu.menuPlanetControls)
                    {
                        //Check for Info Button
                        if (curGameUpdateComponents._curMouseState.X > infoButtonX && curGameUpdateComponents._curMouseState.X < (infoButtonX + 30))
                        {
                            if (curGameUpdateComponents._curMouseState.Y > startingPlanetY && curGameUpdateComponents._curMouseState.Y < (startingPlanetY + 30))
                            {
                                CreatePlanetInfoMenu(curGameUpdateComponents, curPlanet);
                            }
                        }

                        //Check for Command Button
                        if (curGameUpdateComponents._curMouseState.X > commandButtonX && curGameUpdateComponents._curMouseState.X < (commandButtonX + 30))
                        {
                            if (curGameUpdateComponents._curMouseState.Y > startingPlanetY && curGameUpdateComponents._curMouseState.Y < (startingPlanetY + 30))
                            {
                                CreatePlanetCommandMenu(curGameUpdateComponents, curPlanet);
                            }
                        }

                        //Check for Orbit Button
                        if (curGameUpdateComponents._curMouseState.X > orbitButtonX && curGameUpdateComponents._curMouseState.X < (orbitButtonX + 30))
                        {
                            if (curGameUpdateComponents._curMouseState.Y > startingPlanetY && curGameUpdateComponents._curMouseState.Y < (startingPlanetY + 30))
                            {
                                //CreatePlanetCommandMenu(curGameUpdateComponents, curPlanet);
                                CreateOrbitCommandMenu(curGameUpdateComponents, curPlanet);
                            }
                        }


                        startingPlanetY += 60;

                    }



                    if (exitLeftGPMenu)
                    {
                        this.LeftPlanetMenu = new Controls.PlanetsInGalaxyMenu();
                    }

                    if (exitRightGPMenu)
                    {
                        this.RightPlanetMenu = new Controls.PlanetsInGalaxyMenu();
                    }

                }


                



                //Check Info Menu
                if (!this.InfoMenu._isNull && this.InfoMenu._isEnabled)
                {
                    bool exitInfoMenu = false;


                    //Run through each Button
                    foreach (Controls.FancyButtonControl curButton in this.InfoMenu.menuButtons)
                    {
                        if (!curButton._isNull)
                        {
                            if (curButton.mouseClicked(curGameUpdateComponents))
                            {
                                if (curButton.ID == "btnExitInfoMenu")
                                {//Clicked Exit Galaxy
                                    exitInfoMenu = true;
                                }

                                //if (curButton.ID == "btnExitPlanetCmdMenu")
                                //{//Clicked Exit Planet Command
                                //    exitPlntCmdMenu = true;
                                //}

                                //if (curButton.ID == "btnContinue")
                                //{//Clicked Exit Faction form
                                //    _continueClicked = true;
                                //}


                                
                            }
                        }
                    }
                    


                    if (exitInfoMenu)
                    {
                        this.InfoMenu = new Controls.GameInformationMenu();

                        this.LeftPlanetMenu.Enable();
                        this.RightPlanetMenu.Enable();
                    }

                    //if (exitPlntCmdMenu)
                    //{
                    //    this.PlanetCommandMenu = new Controls.PlanetCommandControl();

                    //    this.LeftPlanetMenu.Enable();
                    //    this.RightPlanetMenu.Enable();
                    //}


                }
                

                //Check Planet Control Menu
                if (!this.PlanetCommandMenu._isNull && this.PlanetCommandMenu._isEnabled)
                {
                    
                    bool exitPlntCmdMenu = false;


                    //Run through each Button
                    foreach (Controls.FancyButtonControl curButton in this.PlanetCommandMenu.menuButtons)
                    {
                        if (!curButton._isNull)
                        {
                            if (curButton.mouseClicked(curGameUpdateComponents))
                            {
                            //    if (curButton.ID == "btnExitInfoMenu")
                            //    {//Clicked Exit Galaxy
                            //        exitInfoMenu = true;
                            //    }

                                if (curButton.ID == "btnExitPlanetCmdMenu")
                                {//Clicked Exit Planet Command
                                    exitPlntCmdMenu = true;
                                }

                                //if (curButton.ID == "btnContinue")
                                //{//Clicked Exit Faction form
                                //    _continueClicked = true;
                                //}



                            }
                        }
                    }

                    foreach (Controls.TabPanel.TabPanel curTP in PlanetCommandMenu.tabControl.tabPanels)
                    {
                        Controls.PanelControl curPnl = curTP.panel;

                        //Prolly dont need a loop here but whatevs///

                        if (curTP.selected)
                        {
                            foreach (Controls.FancyButtonControl curBtn in curPnl.menuButtons)
                            {
                                if (curBtn.mouseClicked(curGameUpdateComponents))
                                {
                                    if (curBtn.ID == "btnFacilityMFR")
                                    {
                                        buildConstructionMenu(curGameUpdateComponents, curBtn.ID);
                                    }

                                    if (curBtn.ID == "btnShipMFR")
                                    {
                                        buildConstructionMenu(curGameUpdateComponents, curBtn.ID);
                                        //buildShipMenu(curGameUpdateComponents, curBtn.ID);
                                    }

                                    if (curBtn.ID == "btnTroopsMFR")
                                    {
                                        buildConstructionMenu(curGameUpdateComponents, curBtn.ID);
                                        //buildShipMenu(curGameUpdateComponents, curBtn.ID);
                                    }

                                    if (curBtn.ID == "btnTroops")
                                    {
                                        loadAsManyTroopsOnShipsAsPossible(PlanetCommandMenu.SelectedPlanetName);
                                    }
                                }
                            }
                        }

                    }

                    PlanetCommandMenu.CheckMouseClick(curGameUpdateComponents);


                    if (exitPlntCmdMenu)
                    {
                        this.PlanetCommandMenu = new Controls.PlanetCommandControl();

                        this.LeftPlanetMenu.Enable();
                        this.RightPlanetMenu.Enable();
                    }


                }


                //Check Construction Menu
                if (!ConstructionMenu._isNull)
                {

                    if (ConstructionMenu.destinationDropDown._isEnabled)
                    {
                        //Collapse or close menu if arrow clicked
                        if (ConstructionMenu.destinationDropDown.dropDownBoxArrow.mouseClicked(curGameUpdateComponents))
                        {
                            if (ConstructionMenu.destinationDropDown.dropDownCollapsed)
                                ConstructionMenu.destinationDropDown.dropDownCollapsed = false;
                            else
                                ConstructionMenu.destinationDropDown.dropDownCollapsed = true;
                        }

                        //Select a new test option as "selected"
                        if (!ConstructionMenu.destinationDropDown.dropDownCollapsed)
                        {
                            ConstructionMenu.destinationDropDown.mouseClicked(curGameUpdateComponents);
                        }
                    }


                    if (ConstructionMenu.facilityTypeDropDown._isEnabled)
                    {
                        //Collapse or close menu if arrow clicked
                        if (ConstructionMenu.facilityTypeDropDown.dropDownBoxArrow.mouseClicked(curGameUpdateComponents))
                        {
                            if (ConstructionMenu.facilityTypeDropDown.dropDownCollapsed)
                                ConstructionMenu.facilityTypeDropDown.dropDownCollapsed = false;
                            else
                                ConstructionMenu.facilityTypeDropDown.dropDownCollapsed = true;
                        }

                        //Select a new test option as "selected"
                        if (!ConstructionMenu.facilityTypeDropDown.dropDownCollapsed)
                        {
                            ConstructionMenu.facilityTypeDropDown.mouseClicked(curGameUpdateComponents);
                        }
                    }





                    //Check buttons
                    foreach (Controls.FancyButtonControl curBtn in ConstructionMenu.menuButtons)
                    {
                        if (curBtn.mouseClicked(curGameUpdateComponents))
                        {
                            //Exit
                            if (curBtn.ID == "btnExitManuFacilityMenu")
                            {
                                ConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl();
                            }


                            //Build new facility
                            if (curBtn.ID == "btnContructManuFacilityMenu")
                            {/////////Under construction is going on Type not Entity!!!

                                string strFacilityTypeName = ConstructionMenu.facilityTypeDropDown.textOptions[ConstructionMenu.facilityTypeDropDown.selectedIndex];
                                
                                string strPlanetName = ConstructionMenu.destinationDropDown.textOptions[ConstructionMenu.destinationDropDown.selectedIndex];

                                DataCards.Facility facilCheck = staticDataCards.getFacilityByName(strFacilityTypeName);
                                

                                //InvUnit will be like... An Entity. Maybe I will rename it that later when its a pain in the ass to change it :P
                                GameEngine.PlayerInvObjs.InvUnit newUnit = new GameEngine.PlayerInvObjs.InvUnit();
                                newUnit._underConstruction = true;
                                newUnit.startEntityConstruction(facilCheck, strPlanetName);

                                newUnit.FacilityOwner = new GameEngine.GalacticComponents.Owner(Player1);

                                //Add the Inventory Object to the GroundSpaces->Facilites array
                                curGameUniverse.getPlanetByName(strPlanetName).GroundSpaces.Facilities.Add(newUnit);

                                Player1.curGameResourceMang.invRawMaterials -= facilCheck.baseManufactureCost.ResourceCost;

                                ConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl();
                            }


                            //Build new Ship
                            if (curBtn.ID == "btnContructManuShipMenu")
                            {/////////Under construction is going on Type not Entity!!!

                                string strFacilityTypeShipName = ConstructionMenu.facilityTypeDropDown.textOptions[ConstructionMenu.facilityTypeDropDown.selectedIndex];

                                string strPlanetName = ConstructionMenu.destinationDropDown.textOptions[ConstructionMenu.destinationDropDown.selectedIndex];

                                DataCards.Ship shipCheck = staticDataCards.getShipByName(strFacilityTypeShipName);


                                //InvUnit will be like... An Entity. Maybe I will rename it that later when its a pain in the ass to change it :P
                                GameEngine.PlayerInvObjs.InvUnit newUnit = new GameEngine.PlayerInvObjs.InvUnit();
                                newUnit._underConstruction = true;
                                newUnit.startEntityConstruction(shipCheck, strPlanetName);

                                newUnit.FacilityOwner = new GameEngine.GalacticComponents.Owner(Player1);

                                //Add ship to orbit
                                curGameUniverse.getPlanetByName(strPlanetName).Orbit.StarshipFleetsInOrbit.Add(newUnit);

                                Player1.curGameResourceMang.invRawMaterials -= 100;//Charging 100 by default

                                ConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl();
                            }

                            //Build new Troop
                            if (curBtn.ID == "btnContructManuTroopsMenu")
                            {/////////Under construction is going on Type not Entity!!!

                                string strFacilityTypeTroopsName = ConstructionMenu.facilityTypeDropDown.textOptions[ConstructionMenu.facilityTypeDropDown.selectedIndex];

                                string strPlanetName = ConstructionMenu.destinationDropDown.textOptions[ConstructionMenu.destinationDropDown.selectedIndex];

                                DataCards.Troops TroopsCheck = staticDataCards.getTroopsByName(strFacilityTypeTroopsName);


                                //InvUnit will be like... An Entity. Maybe I will rename it that later when its a pain in the ass to change it :P
                                GameEngine.PlayerInvObjs.InvUnit newUnit = new GameEngine.PlayerInvObjs.InvUnit();
                                newUnit._underConstruction = true;
                                newUnit.startEntityConstruction(TroopsCheck, strPlanetName);

                                newUnit.FacilityOwner = new GameEngine.GalacticComponents.Owner(Player1);

                                //Add troops to planet
                                curGameUniverse.getPlanetByName(strPlanetName).GroundSpaces.Troops.Add(newUnit);

                                Player1.curGameResourceMang.invRawMaterials -= 10;//Charging 10 by default

                                ConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl();
                            }
                        }

                    }




                }




                //Only Check if GP menus are not displayed
                if (this.LeftPlanetMenu._isNull && this.RightPlanetMenu._isNull)
                {
                    //Check Galaxy Clicks
                    foreach (DataCards.Galaxy curGalaxy in this.curGameUniverse.TheUniverse)
                    {
                        string[] strArryXYBounds = curGalaxy.MouseImageAreaCoordinates.Split(';');

                        Model.XYStringToInt topLeft = new Model.XYStringToInt(strArryXYBounds[0]);

                        Model.XYStringToInt bottomRight = new Model.XYStringToInt(strArryXYBounds[1]);




                        if (curGameUpdateComponents._curMouseState.X > topLeft.X && curGameUpdateComponents._curMouseState.X < bottomRight.X)
                        {
                            if (curGameUpdateComponents._curMouseState.Y > topLeft.Y && curGameUpdateComponents._curMouseState.Y < bottomRight.Y)
                            {

                                

                                /////////////////////////////////
                                //Create Galaxy Planets Menu
                                //////////////////////////////////

                                Texture2D curTexture = curGameUpdateComponents._staticTextureImages._galacticPlanetView;
                                //Vector2 curVector = new Vector2(100, 100);
                                //Rectangle curRect = new Rectangle(100, 100, 300, 300);

                                Controls.PlanetsInGalaxyMenu curMenuControl = new Controls.PlanetsInGalaxyMenu();


                                //Start Menu Button List
                                List<Controls.FancyButtonControl> menuButtons = new List<Controls.FancyButtonControl>();


                                //Get Continue Button Settings
                                Texture2D curButtonTexture = curGameUpdateComponents._staticTextureImages._buttonTexture;
                                Vector2 curButtonVector = new Vector2(110, 340);
                                Rectangle curButtonRect = new Rectangle(110, 340, 130, 50);

                                //Add the Continue Button
                                //menuButtons.Add(new GalacticConquest.Controls.FancyButtonControl("btnSaveSettings", curButtonTexture, curButtonVector, curButtonRect, "Save"));

                                //Add the Exit Button 
                                curButtonVector = new Vector2(365, 650);
                                curButtonRect = new Rectangle(365, 650, 130, 50);

                                menuButtons.Add(new Controls.FancyButtonControl("btnExitGalaxyMenu", curButtonTexture, curButtonVector, curButtonRect, "Exit"));





                                //Create the Menu Control
                                curMenuControl = new Controls.PlanetsInGalaxyMenu("mnuGalaxy", curTexture, Model.OrientationType.Left, curGalaxy.Name, "...Settings List Here...", menuButtons);

                                curMenuControl.menuPlanetControls = this.curGameUniverse.getPlanetListByGalaxyName(curGalaxy.Name);

                                this.LeftPlanetMenu = curMenuControl;

                                //Add menu control to list
                                //activeImgMenus.Add(curMenuControl);




                            }
                        }

                    }

                }




                checkTopBarMouseClick(curGameUpdateComponents);
                

                
            }


        }

        public void loadAsManyTroopsOnShipsAsPossible(string planetName)
        {

        }

        public void buildConstructionMenu(GameUpdateClassComponents curGameUpdateComponents, string btnID)
        {//GameEngine.Manufacturing.FacilityMFR

            if (btnID == "btnFacilityMFR")
            {
                Controls.GameEngineControls.ConstructionMenuControl buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl();

                DrawObj.Sprite menuBackImg = new DrawObj.Sprite();
                Vector2 curVector = new Vector2(100, 100);
                menuBackImg = new DrawObj.Sprite("ConstructionMenuBackground", curVector, curGameUpdateComponents._staticTextureImages._constructionMenu);



                Controls.FancyDropDownBoxControl ddlPlanet = new Controls.FancyDropDownBoxControl();
                ddlPlanet._isEnabled = true;
                ddlPlanet.backColor = Color.LightBlue;
                ddlPlanet.borderColor = Color.Black;

                Vector2 ddlPlanetsVector = new Vector2(370, 150);



                ddlPlanet.ID = "ddlPlanets";
                ddlPlanet.selectedIndex = 0;
                ddlPlanet.borderSize = 2;
                ddlPlanet.dropDownBoxRect = new Rectangle((int)ddlPlanetsVector.X, (int)ddlPlanetsVector.Y, 100, 25);

                List<DataCards.Planet> lstPlanets = new List<DataCards.Planet>();

                //lstPlanet.Add("Coruscant");
                //lstPlanet.Add("Corfai");
                //lstPlanet.Add("Corellia");

                lstPlanets = curGameUniverse.getAllPlanetsList();

                List<string> lstStrPlanetNames = new List<string>();

                foreach (DataCards.Planet curPlanet in lstPlanets)
                    lstStrPlanetNames.Add(curPlanet.Name);

                ddlPlanet.textOptions = lstStrPlanetNames;

                Vector2 ddlPlanetsArrowBoxVector = new Vector2(370, 150);
                ddlPlanet.dropDownBoxArrow = new DrawObj.Sprite("ddlPlanetsArrowbox", ddlPlanetsArrowBoxVector, curGameUpdateComponents._staticTextureImages._dropDownArrowTexture);

                buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl("facilContructMenu", "", menuBackImg);

                buildConstructionMenu.destinationDropDown = ddlPlanet;


                //Build facility type list
                Controls.FancyDropDownBoxControl ddlFacilityTypes = new Controls.FancyDropDownBoxControl();
                ddlFacilityTypes._isEnabled = true;
                ddlFacilityTypes.backColor = Color.LightSeaGreen;
                ddlFacilityTypes.borderColor = Color.Black;

                Vector2 ddlFacilityTypesVector = new Vector2(125, 200);



                ddlFacilityTypes.ID = "ddlFacilityTypes";
                ddlFacilityTypes.selectedIndex = 0;
                ddlFacilityTypes.borderSize = 2;
                ddlFacilityTypes.dropDownBoxRect = new Rectangle((int)ddlFacilityTypesVector.X, (int)ddlFacilityTypesVector.Y, 175, 25);

                List<string> lstFacilityTypes = new List<string>();

                lstFacilityTypes.Add("Starport");
                lstFacilityTypes.Add("Barracks");
                lstFacilityTypes.Add("ConstructionYard");

                ddlFacilityTypes.textOptions = lstFacilityTypes;

                Vector2 ddlFacilityTypesArrowBoxVector = new Vector2(125, 200);
                ddlFacilityTypes.dropDownBoxArrow = new DrawObj.Sprite("ddlFacilityTypesArrowbox", ddlFacilityTypesArrowBoxVector, curGameUpdateComponents._staticTextureImages._dropDownArrowTexture);

                //buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl("facilContructMenu", "", menuBackImg);

                buildConstructionMenu.facilityTypeDropDown = ddlFacilityTypes;

                //Button Time!!!

                //Add the Exit Button 
                Vector2 vectExitBtn = new Vector2(300, 375);
                buildConstructionMenu.menuButtons.Add(new Controls.FancyButtonControl("btnExitManuFacilityMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, vectExitBtn, new Rectangle((int)vectExitBtn.X, (int)vectExitBtn.Y, 130, 50), "Exit"));

                Vector2 vectConstructBtn = new Vector2(100, 375);
                buildConstructionMenu.menuButtons.Add(new Controls.FancyButtonControl("btnContructManuFacilityMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, vectConstructBtn, new Rectangle((int)vectConstructBtn.X, (int)vectConstructBtn.Y, 130, 50), "Build"));

                ConstructionMenu = buildConstructionMenu;
            }//End btnFacilityMFR

            if (btnID == "btnShipMFR")
            {
                Controls.GameEngineControls.ConstructionMenuControl buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl();

                DrawObj.Sprite menuBackImg = new DrawObj.Sprite();
                Vector2 curVector = new Vector2(100, 100);
                menuBackImg = new DrawObj.Sprite("ConstructionMenuBackground", curVector, curGameUpdateComponents._staticTextureImages._constructionMenu);

                Controls.FancyDropDownBoxControl ddlPlanet = new Controls.FancyDropDownBoxControl();
                ddlPlanet._isEnabled = true;
                ddlPlanet.backColor = Color.LightBlue;
                ddlPlanet.borderColor = Color.Black;

                Vector2 ddlPlanetsVector = new Vector2(370, 150);

                ddlPlanet.ID = "ddlPlanets";
                ddlPlanet.selectedIndex = 0;
                ddlPlanet.borderSize = 2;
                ddlPlanet.dropDownBoxRect = new Rectangle((int)ddlPlanetsVector.X, (int)ddlPlanetsVector.Y, 100, 25);

                List<DataCards.Planet> lstPlanets = new List<DataCards.Planet>();

                lstPlanets = curGameUniverse.getAllPlanetsList();

                List<string> lstStrPlanetNames = new List<string>();
                
                foreach (DataCards.Planet curPlanet in lstPlanets)
                    lstStrPlanetNames.Add(curPlanet.Name);

                ddlPlanet.textOptions = lstStrPlanetNames;

                Vector2 ddlPlanetsArrowBoxVector = new Vector2(370, 150);
                ddlPlanet.dropDownBoxArrow = new DrawObj.Sprite("ddlPlanetsArrowbox", ddlPlanetsArrowBoxVector, curGameUpdateComponents._staticTextureImages._dropDownArrowTexture);

                buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl("shipContructMenu", "", menuBackImg);

                buildConstructionMenu.destinationDropDown = ddlPlanet;

                //Build facility type list
                Controls.FancyDropDownBoxControl ddlShipTypes = new Controls.FancyDropDownBoxControl();
                ddlShipTypes._isEnabled = true;
                ddlShipTypes.backColor = Color.LightSeaGreen;
                ddlShipTypes.borderColor = Color.Black;

                Vector2 ddlShipTypesVector = new Vector2(125, 200);

                ddlShipTypes.ID = "ddlShipTypes";
                ddlShipTypes.selectedIndex = 0;
                ddlShipTypes.borderSize = 2;
                ddlShipTypes.dropDownBoxRect = new Rectangle((int)ddlShipTypesVector.X, (int)ddlShipTypesVector.Y, 175, 25);

                List<string> lstShipTypes = new List<string>();

                lstShipTypes.Add("NebulonBFrigate");
                lstShipTypes.Add("ActionVITransport");
                lstShipTypes.Add("AurekTacticalStrikefighter");

                ddlShipTypes.textOptions = lstShipTypes;

                Vector2 ddlShipTypesArrowBoxVector = new Vector2(125, 200);
                ddlShipTypes.dropDownBoxArrow = new DrawObj.Sprite("ddlFacilityTypesArrowbox", ddlShipTypesArrowBoxVector, curGameUpdateComponents._staticTextureImages._dropDownArrowTexture);

                //buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl("facilContructMenu", "", menuBackImg);

                buildConstructionMenu.facilityTypeDropDown = ddlShipTypes;


                //Button Time!!!

                //Add the Exit Button 
                Vector2 vectExitBtn = new Vector2(300, 375);
                buildConstructionMenu.menuButtons.Add(new Controls.FancyButtonControl("btnExitManuFacilityMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, vectExitBtn, new Rectangle((int)vectExitBtn.X, (int)vectExitBtn.Y, 130, 50), "Exit"));

                Vector2 vectConstructBtn = new Vector2(100, 375);
                buildConstructionMenu.menuButtons.Add(new Controls.FancyButtonControl("btnContructManuShipMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, vectConstructBtn, new Rectangle((int)vectConstructBtn.X, (int)vectConstructBtn.Y, 130, 50), "Build"));

                ConstructionMenu = buildConstructionMenu;
            }//End btnShipsMFR


            if (btnID == "btnTroopsMFR")
            {
                Controls.GameEngineControls.ConstructionMenuControl buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl();

                DrawObj.Sprite menuBackImg = new DrawObj.Sprite();
                Vector2 curVector = new Vector2(100, 100);
                menuBackImg = new DrawObj.Sprite("ConstructionMenuBackground", curVector, curGameUpdateComponents._staticTextureImages._constructionMenu);

                Controls.FancyDropDownBoxControl ddlPlanet = new Controls.FancyDropDownBoxControl();
                ddlPlanet._isEnabled = true;
                ddlPlanet.backColor = Color.LightBlue;
                ddlPlanet.borderColor = Color.Black;

                Vector2 ddlPlanetsVector = new Vector2(370, 150);

                ddlPlanet.ID = "ddlPlanets";
                ddlPlanet.selectedIndex = 0;
                ddlPlanet.borderSize = 2;
                ddlPlanet.dropDownBoxRect = new Rectangle((int)ddlPlanetsVector.X, (int)ddlPlanetsVector.Y, 100, 25);

                List<DataCards.Planet> lstPlanets = new List<DataCards.Planet>();

                lstPlanets = curGameUniverse.getAllPlanetsList();

                List<string> lstStrPlanetNames = new List<string>();

                foreach (DataCards.Planet curPlanet in lstPlanets)
                    lstStrPlanetNames.Add(curPlanet.Name);

                ddlPlanet.textOptions = lstStrPlanetNames;

                Vector2 ddlPlanetsArrowBoxVector = new Vector2(370, 150);
                ddlPlanet.dropDownBoxArrow = new DrawObj.Sprite("ddlPlanetsArrowbox", ddlPlanetsArrowBoxVector, curGameUpdateComponents._staticTextureImages._dropDownArrowTexture);

                buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl("troopsContructMenu", "", menuBackImg);

                buildConstructionMenu.destinationDropDown = ddlPlanet;

                //Build facility type list
                Controls.FancyDropDownBoxControl ddlTroopsTypes = new Controls.FancyDropDownBoxControl();
                ddlTroopsTypes._isEnabled = true;
                ddlTroopsTypes.backColor = Color.LightSeaGreen;
                ddlTroopsTypes.borderColor = Color.Black;

                Vector2 ddlTroopsTypesVector = new Vector2(125, 200);

                ddlTroopsTypes.ID = "ddlTroopsTypes";
                ddlTroopsTypes.selectedIndex = 0;
                ddlTroopsTypes.borderSize = 2;
                ddlTroopsTypes.dropDownBoxRect = new Rectangle((int)ddlTroopsTypesVector.X, (int)ddlTroopsTypesVector.Y, 175, 25);

                List<string> lstTroopsTypes = new List<string>();

                lstTroopsTypes.Add("ImperialGarrison");
                lstTroopsTypes.Add("StormTroopers");

                ddlTroopsTypes.textOptions = lstTroopsTypes;

                Vector2 ddlShipTypesArrowBoxVector = new Vector2(125, 200);
                ddlTroopsTypes.dropDownBoxArrow = new DrawObj.Sprite("ddlFacilityTypesArrowbox", ddlShipTypesArrowBoxVector, curGameUpdateComponents._staticTextureImages._dropDownArrowTexture);

                //buildConstructionMenu = new Controls.GameEngineControls.ConstructionMenuControl("facilContructMenu", "", menuBackImg);

                buildConstructionMenu.facilityTypeDropDown = ddlTroopsTypes;


                //Button Time!!!

                //Add the Exit Button 
                Vector2 vectExitBtn = new Vector2(300, 375);
                buildConstructionMenu.menuButtons.Add(new Controls.FancyButtonControl("btnExitManuFacilityMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, vectExitBtn, new Rectangle((int)vectExitBtn.X, (int)vectExitBtn.Y, 130, 50), "Exit"));

                Vector2 vectConstructBtn = new Vector2(100, 375);
                buildConstructionMenu.menuButtons.Add(new Controls.FancyButtonControl("btnContructManuTroopsMenu", curGameUpdateComponents._staticTextureImages._buttonTexture, vectConstructBtn, new Rectangle((int)vectConstructBtn.X, (int)vectConstructBtn.Y, 130, 50), "Build"));

                ConstructionMenu = buildConstructionMenu;
            }//End btnTroopsMFR


            if (btnID == "btnTroops")
            {

            }
        }


        public void checkTopBarMouseClick(GameUpdateClassComponents curGameUpdateComponents)
        {
            
            //Check Top Bar Menu
            if (!this.TopBarControl._isNull && this.TopBarControl._isEnabled)
            {
                //bool exitInfoMenu = false;


                //Run through each Button
                foreach (Controls.FancyButtonControl curButton in this.TopBarControl.menuButtons)
                {
                    if (!curButton._isNull)
                    {
                        if (curButton.mouseClicked(curGameUpdateComponents))
                        {
                            if (curButton.ID == "btnAdvanceDay")
                            {
                                //Advance a day
                                curGalacticDayMang.advanceADay();
                                
                                curGameUniverse.advanceGalacticDay(Player1);

                                //Update construction stuff

                                List<DataCards.Planet> allPlanets = curGameUniverse.getAllPlanetsList();


                                foreach (DataCards.Planet curPlanet in allPlanets)
                                {

                                    foreach (GameEngine.PlayerInvObjs.InvUnit curIU in curPlanet.GroundSpaces.Facilities)
                                    {
                                        if (curIU._underConstruction)
                                        {
                                            curIU._remainingConstructionDays--;

                                            if (curIU._remainingConstructionDays <= 0)
                                            {//Finished constructing
                                                curIU._underConstruction = false;
                                                curIU._remainingConstructionDays = 0;
                                            }
                                        }
                                    }

                                    foreach (GameEngine.PlayerInvObjs.InvUnit curIU in curPlanet.GroundSpaces.Troops)
                                    {
                                        if (curIU._underConstruction)
                                        {
                                            curIU._remainingConstructionDays--;

                                            if (curIU._remainingConstructionDays <= 0)
                                            {//Finished constructing
                                                curIU._underConstruction = false;
                                                curIU._remainingConstructionDays = 0;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }


    }
}
