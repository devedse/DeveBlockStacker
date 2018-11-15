using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.GameState;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeveBlockStacker.Shared
{
    public class TheGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ContentDistributionThing contentDistributionThing;
        private IGameState currentState;
        private InputStatifier inputStatifier;

        public TheGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

#if !ANDROID
            graphics.PreferredBackBufferWidth = 720 / 2;
            graphics.PreferredBackBufferHeight = 1280 / 2;
#endif

            inputStatifier = new InputStatifier();

            NewGame();
        }

        public void NewGame()
        {
            contentDistributionThing = new ContentDistributionThing(graphics);
            //var gameData = new GameData();
            currentState = new NewGameState();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            contentDistributionThing.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            inputStatifier.BeforeUpdate();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            currentState = currentState.Update(inputStatifier);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            currentState.Draw(spriteBatch, contentDistributionThing);

            //spriteBatch.Draw(squareImage, new Rectangle(0, 0, 100, 100), Color.Black);
            //spriteBatch.Draw(squareImage, new Rectangle(110, 0, 100, 100), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
