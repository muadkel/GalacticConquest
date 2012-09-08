using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GalacticConquest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int screenWidth = 1024;
        public const int screenHeight = 768;




        GameEngine.StartMenuEngine currentStartMenu;
        GameEngine.GalacticGameEngine currentGalaxyGame;



        ////////////////////////////////
        //Static Content Variables
        //-Loaded in the LoadContent to be used throughout
        Model.StaticTextureImages staticTextureImages;

        Model.StaticFonts staticFonts;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            loadSystemSettings();
            clearAllProgramSettings();

            base.Initialize();
        }


        /// <summary>
        /// Attempt to set the display mode to the desired resolution.  Itterates through the display
        /// capabilities of the default graphics adapter to determine if the graphics adapter supports the
        /// requested resolution.  If so, the resolution is set and the function returns true.  If not,
        /// no change is made and the function returns false.
        /// </summary>
        /// <param name="iWidth">Desired screen width.</param>
        /// <param name="iHeight">Desired screen height.</param>
        /// <param name="bFullScreen">True if you wish to go to Full Screen, false for Windowed Mode.</param>
        private bool InitGraphicsMode(int iWidth, int iHeight, bool bFullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (bFullScreen == false)
            {
                if ((iWidth <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (iHeight <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = iWidth;
                    graphics.PreferredBackBufferHeight = iHeight;
                    graphics.IsFullScreen = bFullScreen;
                    graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == iWidth) && (dm.Height == iHeight))
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        graphics.PreferredBackBufferWidth = iWidth;
                        graphics.PreferredBackBufferHeight = iHeight;
                        graphics.IsFullScreen = bFullScreen;
                        graphics.ApplyChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public void loadSystemSettings()
        {
            this.IsMouseVisible = true;

            this.Window.Title = "Galactic Conquest";

            InitGraphicsMode(screenWidth, screenHeight, false);

        }

        
        public void clearAllProgramSettings()
        {
            currentStartMenu = new GameEngine.StartMenuEngine();
            currentGalaxyGame = new GameEngine.GalacticGameEngine();

        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            /////////////////////////////
            //Load all Content Images and Fonts
            /////////////////////////////
            staticTextureImages = new Model.StaticTextureImages(Content,GraphicsDevice);
            staticFonts = new Model.StaticFonts(Content);



            currentStartMenu.loadStartMenuContent(staticTextureImages, staticFonts);

            currentStartMenu.loadUpTheStartMenu();


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        MouseState mouseStateCurrent, mouseStatePrevious;

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit on XBox
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Keyboard exit
            exitGameOnEsc();

            // TODO: Add your update logic here

            
            ///////////////////////
            //Start a new game
            //////////////////////

            if (currentStartMenu.startNewGame)
            {
                //Get all the Update and system values to send to the checks in the objects.
                GameUpdateClassComponents curGameUpdateComponents = new GameUpdateClassComponents();
                curGameUpdateComponents = new GameUpdateClassComponents(Mouse.GetState(), Content, staticTextureImages, staticFonts, this, Components, GraphicsDevice, screenWidth, screenHeight);

                //currentGalaxyGame 
                currentGalaxyGame.startNewGame("NewGame", "MyNewGame", currentStartMenu.enteredUserName, currentStartMenu.selectedFaction, curGameUpdateComponents);
                currentStartMenu.startNewGame = false;
            }



            /////////////////////////
            //Mouse Click Event
            /////////////////////////
            mouseStateCurrent = Mouse.GetState();

          

            //Check if Left mouse click is over the button as the user lets go of the button//
            if (mouseStatePrevious.LeftButton == ButtonState.Pressed && mouseStateCurrent.LeftButton == ButtonState.Released)
            {
                if (currentStartMenu._isEnabled)
                {
                    //Get all the Update and system values to send to the checks in the objects.
                    GameUpdateClassComponents curGameUpdateComponents = new GameUpdateClassComponents();
                    curGameUpdateComponents = new GameUpdateClassComponents(Mouse.GetState(), Content, staticTextureImages, staticFonts, this, Components, GraphicsDevice, screenWidth, screenHeight);

                    currentStartMenu.checkStartScreenMouseClick(curGameUpdateComponents);
                }

                if (currentGalaxyGame._gameIsRunning)
                {
                    //Get all the Update and system values to send to the checks in the objects.
                    GameUpdateClassComponents curGameUpdateComponents = new GameUpdateClassComponents();
                    curGameUpdateComponents = new GameUpdateClassComponents(Mouse.GetState(), Content, staticTextureImages, staticFonts, this, Components, GraphicsDevice, screenWidth, screenHeight);

                    currentGalaxyGame.checkGameRunningMouseClick(curGameUpdateComponents);
                }

            }

            mouseStatePrevious = mouseStateCurrent;
            

            base.Update(gameTime);
        }

        public void exitGameOnEsc()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //Start Menu controls
            if (currentStartMenu._isEnabled)
            {
                //Prepare the Drawing components to send to the objects
                GameDrawClassComponents curDrawComponents = new GameDrawClassComponents();
                curDrawComponents = new GameDrawClassComponents(Content, staticTextureImages, staticFonts, this, spriteBatch, GraphicsDevice, screenWidth, screenHeight);
                
                currentStartMenu.drawStartMenu(curDrawComponents);
            }

            //Check if a game is running
            if (currentGalaxyGame._gameIsRunning)
            {   
                //Prepare the Drawing components to send to the objects
                GameDrawClassComponents curDrawComponents = new GameDrawClassComponents();
                curDrawComponents = new GameDrawClassComponents(Content, staticTextureImages, staticFonts, this, spriteBatch, GraphicsDevice, screenWidth, screenHeight);

                currentGalaxyGame.displayGameScreen(curDrawComponents);
            }

            

            base.Draw(gameTime);
        }




    }
}
