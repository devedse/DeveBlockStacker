using DeveBlockStacker.Core.HelperObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DeveBlockStacker.Core.Data
{
    public class ContentDistributionThing
    {
        public Texture2D SquareImage { get; private set; }
        public SpriteFont SecularOne20 { get; private set; }
        public SpriteFont SecularOne72 { get; private set; }

        public IntSize ScreenSizeTotal { get; private set; }
        public Rectangle ScreenSizeGame { get; private set; }


        private readonly GraphicsDeviceManager _graphics;

        public ContentDistributionThing(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            CalculateScreenSize();
        }

        public void LoadContent(ContentManager content)
        {
            SquareImage = content.Load<Texture2D>("Square");
#if NETFRAMEWORK || BLAZOR
            SecularOne20 = content.Load<SpriteFont>("SecularOne20");
            SecularOne72 = content.Load<SpriteFont>("SecularOne72");
#else
            SecularOne20 = content.Load<SpriteFont>("SecularOne20_Compressed");
            SecularOne72 = content.Load<SpriteFont>("SecularOne72_Compressed");
#endif
        }

        public void Update()
        {
            CalculateScreenSize();
        }

        private void CalculateScreenSize()
        {
            int screenWidth = _graphics.PreferredBackBufferWidth;
            int screenHeight = _graphics.PreferredBackBufferHeight;

            ScreenSizeTotal = new IntSize(screenWidth, screenHeight);

            float maxFactorWidth = 0.8f;
            float maxFactorHeight = 3f;

            int screenSizeGameWidth = (int)Math.Min(screenWidth, screenHeight * maxFactorWidth);
            int screenSizeGameHeight = (int)Math.Min(screenHeight, screenWidth * maxFactorHeight);
            ScreenSizeGame = new Rectangle((screenWidth - screenSizeGameWidth) / 2, (screenHeight - screenSizeGameHeight) / 2, screenSizeGameWidth, screenSizeGameHeight);
        }
    }
}
