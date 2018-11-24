using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.GameState;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

            inputStatifier = new InputStatifier();

            NewGame();
        }

        public TheGame(int widthScreen, int heightScreen) : this()
        {
            graphics.PreferredBackBufferWidth = widthScreen;
            graphics.PreferredBackBufferHeight = heightScreen;
        }

        public void NewGame()
        {
            contentDistributionThing = new ContentDistributionThing(graphics);
            currentState = new NewGameState();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);



            spriteBatch.Begin();

            currentState.Draw(spriteBatch, contentDistributionThing, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
