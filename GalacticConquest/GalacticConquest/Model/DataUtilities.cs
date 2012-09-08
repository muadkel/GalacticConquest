using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GalacticConquest.Model
{
    public class DataUtilities
    {
        public static string _DataPath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Data\\";

        public static string _StartingSettingsXMLPath = "StartingSettings.xml";

        public static string _CharactersXMLPath = "Characters.xml";
        public static string _FactionsXMLPath = "Factions.xml";
        public static string _FacilitiesXMLPath = "Facilities.xml";
        public static string _GalaxiesXMLPath = "Galaxies.xml";
        public static string _ShipsXMLPath = "Ships.xml";
        public static string _TroopsXMLPath = "Troops.xml";


        public static string _PlanetImagePath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Images\\Planets\\";

        public static string _ButtonsImagePath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Images\\Buttons\\";


        public static string _ControlsImagePath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Images\\Controls\\";

        public static string _GameControlsImagePath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Images\\GameControls\\";

        public static string _MsgTemplatesImagePath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Images\\MessageTemplates\\";




        public static string _FacilitiesImagePath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Images\\Facilities\\";

        public static string _ShipsImagePath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Images\\Ships\\";

        public static string _TroopsImagePath = "C:\\GameDevelopment\\GalacticConquest\\GalacticConquest\\Images\\Troops\\";

        public static List<DataCards.Galaxy> getGalaxiesFromXML()
        {
            List<DataCards.Galaxy> rtnGalaxies = new List<DataCards.Galaxy>();


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_DataPath + _GalaxiesXMLPath);

            XmlNodeList xmlTerrainNodes = xmlDoc.GetElementsByTagName("Galaxy");

            foreach (XmlNode curNode in xmlTerrainNodes)
            {
                DataCards.Galaxy curGalaxy = new DataCards.Galaxy();


                curGalaxy.Name = curNode.Attributes["Name"].InnerText;
                curGalaxy.UniverseCoordinates = new Coordinates(curNode.Attributes["UniverseCoordinates"].InnerText);
                curGalaxy.MouseImageAreaCoordinates = curNode.Attributes["MouseImageAreaCoordinates"].InnerText;





                XmlNodeList xmlGalaxyPlanets = curNode.ChildNodes;

                Random random = new Random();

                foreach (XmlNode curPlanetNode in xmlGalaxyPlanets)
                {
                    DataCards.Planet curPlanet = new DataCards.Planet();
                    curPlanet.Name = curPlanetNode.Attributes["Name"].InnerText;
                    curPlanet.HeaderText = curPlanetNode.Attributes["HeaderText"].InnerText;
                    curPlanet.GroundSpaces = new GameEngine.GalacticComponents.PlanetSpace(Convert.ToInt32(curPlanetNode.Attributes["DefaultPlanetGroundSpaces"].InnerText));

                    //Setup resources ~We will do random for now.
                    int randomNumber = random.Next(1000, 10000);
                    curPlanet.RawMaterialsPerDay = randomNumber;


                    XmlNodeList xmlPlanetAttributes = curPlanetNode.ChildNodes;

                    foreach (XmlNode curPlanetAttr in xmlPlanetAttributes)
                    {
                        //DataCards.Planet curPlanet = new DataCards.Planet();
                        //curPlanet.Name = curPlanetNode.Attributes["Name"].InnerText;
                        //curPlanet.HeaderText = curPlanetNode.Attributes["HeaderText"].InnerText;
                        //curPlanet.Spaces = new GameEngine.GalacticComponents.PlanetSpace(Convert.ToInt32(curPlanetNode.Attributes["DefaultPlanetSpaces"].InnerText));


                        if (curPlanetAttr.Name == "Description")
                        {
                            curPlanet.Description = curPlanetAttr.InnerText;
                        }
                        else
                        {
                            curPlanet.Description = "";
                        }

                    }



                    curGalaxy.addPlanet(curPlanet);
                }



                rtnGalaxies.Add(curGalaxy);


                //XmlNodeList xmlTerrainImages = curNode.ChildNodes;

                //foreach (XmlNode curImgNode in xmlTerrainImages)
                //{
                //    curWidgetData.addObjImage(new System.Drawing.Bitmap(curImgNode.Attributes["src"].InnerText));
                //}

                //_terrainObjects.Add(curWidgetData);
            }


            return rtnGalaxies;
        }


        public static List<DataCards.Faction> getFactionsFromXML()
        {
            List<DataCards.Faction> rtnFactions = new List<GalacticConquest.DataCards.Faction>();


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_DataPath + _FactionsXMLPath);

            XmlNodeList xmlTerrainNodes = xmlDoc.GetElementsByTagName("Faction");

            foreach (XmlNode curNode in xmlTerrainNodes)
            {
                DataCards.Faction curFaction = new DataCards.Faction();


                curFaction.ID = curNode.Attributes["ID"].InnerText;
                curFaction.Name = curNode.Attributes["Name"].InnerText;
                curFaction.HeaderText = curNode.Attributes["HeaderText"].InnerText;

                rtnFactions.Add(curFaction);


                //XmlNodeList xmlTerrainImages = curNode.ChildNodes;

                //foreach (XmlNode curImgNode in xmlTerrainImages)
                //{
                //    curWidgetData.addObjImage(new System.Drawing.Bitmap(curImgNode.Attributes["src"].InnerText));
                //}

                //_terrainObjects.Add(curWidgetData);
            }


            return rtnFactions;
        }


        public static List<DataCards.Facility> getFacilitiesFromXML()
        {
            List<DataCards.Facility> rtnFacilities = new List<DataCards.Facility>();

            //Pulls from the XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_DataPath + _FacilitiesXMLPath);

            XmlNodeList xmlTerrainNodes = xmlDoc.GetElementsByTagName("Facility");

            foreach (XmlNode curNode in xmlTerrainNodes)
            {
                DataCards.Facility curFacility = new DataCards.Facility();


                //curFacility.ID = curNode.Attributes["ID"].InnerText;
                curFacility.Name = curNode.Attributes["Name"].InnerText;
                curFacility.HeaderText = curNode.Attributes["HeaderText"].InnerText;
                curFacility.Type = curNode.Attributes["Type"].InnerText;

                
                //////////////////////////////////////////////////
                //Get Manufacturing details. Make this a function when you need it for the next thing like ships..
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                GameEngine.Manufacturing curManu = new GameEngine.Manufacturing();


                XmlNodeList xmlChildNodes = curNode.ChildNodes;

                foreach (XmlNode curChildNodes in xmlChildNodes)
                {
                    //curWidgetData.addObjImage(new System.Drawing.Bitmap(curImgNode.Attributes["src"].InnerText));


                    if (curChildNodes.Name == "Manufacture")
                    {

                        XmlNodeList xmlChildNodes1 = curChildNodes.ChildNodes;

                        foreach (XmlNode curChildNodes1 in xmlChildNodes1)
                        {

                            if (curChildNodes1.Name == "Resources")
                            {
                                curManu.ResourceCost = Utilities.getIntOrN(curChildNodes1.InnerText, 0);
                            }
                            else if (curChildNodes1.Name == "ConstructionTime")
                            {
                                curManu.BaseConstructionTime = Utilities.getIntOrN(curChildNodes1.InnerText, 1);
                            }
                        }
                    }
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////

                curFacility.baseManufactureCost = curManu;
                
                rtnFacilities.Add(curFacility);


            }


            return rtnFacilities;
        }


        public static List<DataCards.Ship> getShipsFromXML()
        {
            List<DataCards.Ship> rtnShips = new List<DataCards.Ship>();

            //Pulls from the XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_DataPath + _ShipsXMLPath);

            XmlNodeList xmlTerrainNodes = xmlDoc.GetElementsByTagName("Ship");

            foreach (XmlNode curNode in xmlTerrainNodes)
            {
                DataCards.Ship curShip = new DataCards.Ship();

                XmlNodeList xmlChildNodes = curNode.ChildNodes;
                //curFacility.ID = curNode.Attributes["ID"].InnerText;
                curShip.Name = curNode.Attributes["Name"].InnerText;
                curShip.HeaderText = curNode.Attributes["HeaderText"].InnerText;
                curShip.Type = curNode.Attributes["Type"].InnerText;


                foreach (XmlNode curChildNodes in xmlChildNodes)
                {
                    //curWidgetData.addObjImage(new System.Drawing.Bitmap(curImgNode.Attributes["src"].InnerText));


                    if (curChildNodes.Name == "Attributes")
                    {

                        XmlNodeList xmlChildNodes1 = curChildNodes.ChildNodes;

                        foreach (XmlNode curChildNodes1 in xmlChildNodes1)
                        {
                            if (curChildNodes1.Name == "Troops")
                                curShip.TroopSpaces = Utilities.getIntOrN(curChildNodes1.InnerText, 0);
                            //else if (curChildNodes1.Name == "ConstructionTime")
                            //{
                            //    curManu.BaseConstructionTime = Utilities.getIntOrN(curChildNodes1.InnerText, 1);
                            //}
                        }
                    }
                }

                rtnShips.Add(curShip);
            }
            return rtnShips;
        }


        public static List<DataCards.Character> getCharactersFromXML()
        {
            List<DataCards.Character> rtnCharacters = new List<DataCards.Character>();

            //Pulls from the XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_DataPath + _CharactersXMLPath);

            XmlNodeList xmlTerrainNodes = xmlDoc.GetElementsByTagName("Character");

            foreach (XmlNode curNode in xmlTerrainNodes)
            {
                DataCards.Character curCharacter = new DataCards.Character();


                //curFacility.ID = curNode.Attributes["ID"].InnerText;
                curCharacter.Name = curNode.Attributes["Name"].InnerText;
                curCharacter.HeaderText = curNode.Attributes["HeaderText"].InnerText;
                curCharacter.Species = curNode.Attributes["Species"].InnerText;


                //XmlNodeList xmlChildNodes = curNode.ChildNodes;

                //foreach (XmlNode curChildNode in xmlChildNodes)
                //{
                //    //curWidgetData.addObjImage(new System.Drawing.Bitmap(curImgNode.Attributes["src"].InnerText));



                //}


                rtnCharacters.Add(curCharacter);




                //_terrainObjects.Add(curWidgetData);
            }


            return rtnCharacters;
        }

        public static List<DataCards.Troops> getTroopsFromXML()
        {
            List<DataCards.Troops> rtnTroops = new List<DataCards.Troops>();

            //Pulls from the XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_DataPath + _TroopsXMLPath);

            XmlNodeList xmlTerrainNodes = xmlDoc.GetElementsByTagName("Troop");

            foreach (XmlNode curNode in xmlTerrainNodes)
            {
                DataCards.Troops curTroops = new DataCards.Troops();


                //curFacility.ID = curNode.Attributes["ID"].InnerText;
                curTroops.Name = curNode.Attributes["Name"].InnerText;
                curTroops.HeaderText = curNode.Attributes["HeaderText"].InnerText;
                //curTroops.Type = curNode.Attributes["Type"].InnerText;


                //XmlNodeList xmlChildNodes = curNode.ChildNodes;

                //foreach (XmlNode curChildNode in xmlChildNodes)
                //{
                //    //curWidgetData.addObjImage(new System.Drawing.Bitmap(curImgNode.Attributes["src"].InnerText));



                //}


                rtnTroops.Add(curTroops);




                //_terrainObjects.Add(curWidgetData);
            }


            return rtnTroops;
        }


        public static GameEngine.StartingGameUnits getStartingSettingsFromXML(string strFactionName, Model.StaticDataCards staticDataCards)
        {
            GameEngine.StartingGameUnits rtnStartingGameUnits = new GameEngine.StartingGameUnits();


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_DataPath + _StartingSettingsXMLPath);

            XmlNodeList xmlTerrainNodes = xmlDoc.GetElementsByTagName("StartingUnits");

            foreach (XmlNode curNode in xmlTerrainNodes)
            {
                //Get this factions starting units
                if (curNode.Attributes["FactionName"].InnerText == strFactionName)
                {
                    XmlNodeList xmlChildNodes = curNode.ChildNodes;

                    foreach (XmlNode curChildNode in xmlChildNodes)
                    {
                        //Consider a way to implement this...
                        //if (curChildNode.Attributes["Type"].InnerText == Model.DataCardType.Planet.ToString())
                        //{
                        //    GameEngine.PlayerInvObjs.InvUnit curInvUnit = new GameEngine.PlayerInvObjs.InvUnit(staticDataCards.getShipByName(curChildNode.Attributes["UnitName"].InnerText), curChildNode.Attributes["PlanetName"].InnerText);

                        //    rtnStartingGameUnits.startingUnits.Add(curInvUnit);
                        //}

                        if (curChildNode.Attributes["Type"].InnerText == "Resources")
                            rtnStartingGameUnits.startingResources = Model.Utilities.getIntOrN(curChildNode.Attributes["StartingAmount"].InnerText, -1);

                        if (curChildNode.Attributes["Type"].InnerText == Model.DataCardType.Facility.ToString())
                        {
                            GameEngine.PlayerInvObjs.InvUnit curInvUnit = new GameEngine.PlayerInvObjs.InvUnit(staticDataCards.getFacilityByName(curChildNode.Attributes["UnitName"].InnerText), curChildNode.Attributes["PlanetName"].InnerText);

                            rtnStartingGameUnits.startingUnits.Add(curInvUnit);
                        }

                        else if (curChildNode.Attributes["Type"].InnerText == Model.DataCardType.Ship.ToString())
                        {
                            GameEngine.PlayerInvObjs.InvUnit curInvUnit = new GameEngine.PlayerInvObjs.InvUnit(staticDataCards.getShipByName(curChildNode.Attributes["UnitName"].InnerText), curChildNode.Attributes["PlanetName"].InnerText);

                            rtnStartingGameUnits.startingUnits.Add(curInvUnit);
                        }

                        else if (curChildNode.Attributes["Type"].InnerText == Model.DataCardType.Troops.ToString())
                        {
                            GameEngine.PlayerInvObjs.InvUnit curInvUnit = new GameEngine.PlayerInvObjs.InvUnit(staticDataCards.getTroopsByName(curChildNode.Attributes["UnitName"].InnerText), curChildNode.Attributes["PlanetName"].InnerText);

                            rtnStartingGameUnits.startingUnits.Add(curInvUnit);
                        }

                    }


                }

            }


            return rtnStartingGameUnits;
        }



        //Dictionaries are neat-o
        public static Dictionary<string, string> getFactionsDictionaryFromXML()
        {
            Dictionary<string, string> rtnFactions = new Dictionary<string, string>();


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_DataPath + _FactionsXMLPath);

            XmlNodeList xmlTerrainNodes = xmlDoc.GetElementsByTagName("Faction");

            foreach (XmlNode curNode in xmlTerrainNodes)
            {

                rtnFactions.Add(curNode.Attributes["HeaderText"].InnerText, curNode.Attributes["Name"].InnerText);


            }


            return rtnFactions;
        }


    }
}
