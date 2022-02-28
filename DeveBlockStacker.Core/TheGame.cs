using DeveBlockStacker.Core.State;
using DeveBlockStacker.Core.Data;
using DeveBlockStacker.Core.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DeveBlockStacker.Core.HelperObjects;

namespace DeveBlockStacker.Core
{
    public class TheGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly ContentDistributionThing _contentDistributionThing;
        private IGameState _currentState;
        private readonly InputStatifier _inputStatifier;

        private IntSize? _desiredScreenSize;

        public TheGame() : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _inputStatifier = new InputStatifier();

            _contentDistributionThing = new ContentDistributionThing(_graphics);
            _currentState = new NewGameState();
        }

        public TheGame(IntSize desiredScreenSize) : this()
        {
            _desiredScreenSize = desiredScreenSize;
        }

        protected override void Initialize()
        {
            if (_desiredScreenSize != null)
            {
                _graphics.PreferredBackBufferWidth = _desiredScreenSize.Width;
                _graphics.PreferredBackBufferHeight = _desiredScreenSize.Height;
            }

#if ANDROID
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
             //To remove the Battery bar
            graphics.IsFullScreen = true;
#endif
#if WINDOWS_UAP
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                //To remove the Battery bar
                graphics.IsFullScreen = true;
            }
#endif

            _graphics.SupportedOrientations = DisplayOrientation.Portrait | DisplayOrientation.PortraitDown;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _contentDistributionThing.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _inputStatifier.BeforeUpdate();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _currentState = _currentState.Update(_inputStatifier);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _currentState.Draw(_spriteBatch, _contentDistributionThing);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
