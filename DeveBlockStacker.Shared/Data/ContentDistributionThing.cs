using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.Data
{
    public class ContentDistributionThing
    {
        public Texture2D SquareImage { get; private set; }
        public SpriteFont SegoeUI20 { get; private set; }
        public SpriteFont SegoeUI70 { get; private set; }

        public int ScreenWidth => graphics.PreferredBackBufferWidth;
        public int ScreenHeight => graphics.PreferredBackBufferHeight;

        private GraphicsDeviceManager graphics;

        public ContentDistributionThing(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }

        public void LoadContent(ContentManager content)
        {
            SquareImage = content.Load<Texture2D>("Square");
            SegoeUI20 = content.Load<SpriteFont>("SegoeUI20");
            SegoeUI70 = content.Load<SpriteFont>("SegoeUI72");
        }
    }
}
