using DeveBlockStacker.Core.Data;
using DeveBlockStacker.Core.Drawers;
using DeveBlockStacker.Core.GameState;
using DeveBlockStacker.Core.HelperObjects;
using DeveBlockStacker.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DeveBlockStacker.Core
{
    public class TheGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly ContentDistributionThing _contentDistributionThing;
        private IGameState _currentState;
        private readonly InputStatifier _inputStatifier;
        private readonly Platform _platform;

        private IntSize? _desiredScreenSize = null;
        private IContentManagerExtension _contentManagerExtension = null;

        private EffectBlockDrawer _effectBlockDrawer;

        public TheGame() : this(Platform.Desktop)
        {

        }

        public TheGame(Platform platform) : this(null, platform)
        {

        }

        public TheGame(IntSize? desiredScreenSize, Platform platform) : this(null, desiredScreenSize, platform)
        {
        }

        public TheGame(IContentManagerExtension contentManagerExtension, IntSize? desiredScreenSize, Platform platform) : base()
        {
            _contentManagerExtension = contentManagerExtension;
            _desiredScreenSize = desiredScreenSize;
            _platform = platform;

            _graphics = new GraphicsDeviceManager(this);

            //This is required for Blazor since it loads assets in a custom way
            Content = new ExtendibleContentManager(this.Services, _contentManagerExtension);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            _inputStatifier = new InputStatifier();

            _contentDistributionThing = new ContentDistributionThing(_graphics);
            _currentState = new NewGameState();
            _platform = platform;

            _effectBlockDrawer = new EffectBlockDrawer(_contentDistributionThing);
        }

        protected override void Initialize()
        {
#if !BLAZOR
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            Window.OrientationChanged += Window_OrientationChanged;

            FixScreenSize();
#endif
            base.Initialize();
        }

        private void FixScreenSize()
        {
            if (_desiredScreenSize != null)
            {
                _graphics.PreferredBackBufferWidth = _desiredScreenSize.Value.Width;
                _graphics.PreferredBackBufferHeight = _desiredScreenSize.Value.Height;

                //Ensure this only happens once
                _desiredScreenSize = null;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            }

            if (_platform == Platform.Android || _platform == Platform.UWP) // && Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile"
            {
                //To remove the Battery bar
                _graphics.IsFullScreen = true;
            }

            Window.AllowUserResizing = true;

            _graphics.ApplyChanges();
        }

        private void Window_OrientationChanged(object sender, System.EventArgs e)
        {
            FixScreenSize();
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            FixScreenSize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _contentDistributionThing.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _inputStatifier.BeforeUpdate();
            if (_inputStatifier.CurrentGamePadState.Buttons.Back == ButtonState.Pressed || _inputStatifier.CurrentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _currentState = _currentState.Update(_inputStatifier);
            _effectBlockDrawer.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _contentDistributionThing.Update();

            var screenSizeGame = _contentDistributionThing.ScreenSizeGame;
            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(screenSizeGame.X, screenSizeGame.Y, 0));

            _effectBlockDrawer.Draw(_spriteBatch);
            _currentState.Draw(_spriteBatch, _contentDistributionThing);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
