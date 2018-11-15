using DeveBlockStacker.Shared.Data;
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
        private Texture2D squareImage;


        public GameData gameData;
        public IGameState currentState;

        public TheGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 720 / 2;
            graphics.PreferredBackBufferHeight = 1280 / 2;

            NewGame();
        }

        public void NewGame()
        {
            gameData = new GameData();
            currentState = new PlayingState(gameData);
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
            squareImage = Content.Load<Texture2D>("Square");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            currentState.Update(gameData);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            Random r = new Random();

            for (int y = gameData.GridHeight - 1; y >= 0; y--)
            {
                for (int x = gameData.GridWidth - 1; x >= 0; x--)
                {
                    float widthOfBlockje = graphics.PreferredBackBufferWidth / gameData.GridWidth;
                    float heightOfBlockje = graphics.PreferredBackBufferHeight / gameData.GridHeight;

                    float yyy = graphics.PreferredBackBufferHeight - ((1 + y) * heightOfBlockje);
                    var blocketje = new Blocketje(squareImage, x * widthOfBlockje, yyy, widthOfBlockje, heightOfBlockje, 3);

                    if (gameData.Gridje[x, y] == true)
                    {
                        if (y > 7)
                        {
                            blocketje.Draw(spriteBatch, Color.Red);
                        }
                        else
                        {
                            blocketje.Draw(spriteBatch, Color.Blue);
                        }
                    }
                }
            }

            //spriteBatch.Draw(squareImage, new Rectangle(0, 0, 100, 100), Color.Black);
            //spriteBatch.Draw(squareImage, new Rectangle(110, 0, 100, 100), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
