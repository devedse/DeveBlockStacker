using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DeveBlockStacker.Blazor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //tests
            var wnd = this.Window;
            var wndbounds = wnd.ClientBounds;

            return;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void Update(GameTime gameTime)
        {
            var ms = Mouse.GetState();
            var ks = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // clear
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

    }
}
