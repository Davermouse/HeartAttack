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
using HeartAttack.Oximeter;
using HeartAttack.Scenes;

namespace HeartAttack
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class HeartAttack : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public int shotsFired;
        public int bugsKilled;

        Scene m_CurrentScene = null;

        // Dirty dirty hack
        public static HeartAttack theGameInstance { get; private set; }

        public HeartAttack()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            theGameInstance = this;
            shotsFired = 0;
            bugsKilled = 0;

            graphics.SynchronizeWithVerticalRetrace = true;
           // graphics.IsFullScreen = true;
        }

        public OximeterManager Oximeter
        {
            get;
            private set;
        }

        public SpriteFont Font
        {
            get;
            private set;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Oximeter = new OximeterManager();
            Oximeter.Start();

            this.Font = this.Content.Load<SpriteFont>("MainFont");

            m_CurrentScene = new TitleScene();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Oximeter.Update(gameTime);

            m_CurrentScene = m_CurrentScene.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!Oximeter.HasBeat)
            {
                GraphicsDevice.Clear(Color.DarkSlateGray);
            }
            else
            {
                GraphicsDevice.Clear(Color.Red);
            }

            // TODO: Add your drawing code here
            m_CurrentScene.Draw(gameTime);

            Oximeter.ResetBeat();
            base.Draw(gameTime);
        }
    }
}
