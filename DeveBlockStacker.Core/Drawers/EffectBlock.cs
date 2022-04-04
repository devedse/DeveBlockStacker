using DeveBlockStacker.Core.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Core.Drawers
{
    public class EffectBlock
    {
        public static int BlockSize => 80;

        private readonly Color _color;
        private readonly ContentDistributionThing _contentDistributionThing;
        private float _timer;
        public bool Completed => _timer > 5;

        public bool LeftSide { get; }
        public int GridX { get; }
        public int GridY { get; }

        public EffectBlock(Color color, int gridX, int gridY, bool leftSide, ContentDistributionThing contentDistributionThing)
        {
            _color = color;

            GridX = gridX;
            GridY = gridY;
            LeftSide = leftSide;
            _contentDistributionThing = contentDistributionThing;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int xpos = BlockSize * GridX;
            if (LeftSide)
            {
                xpos = (xpos * -1) - BlockSize;
            }
            else
            {
                xpos += _contentDistributionThing.ScreenSizeGame.Width;
            }

            float maxBrightness = 0.2f;
            float brightness;

            if (_timer < 1f)
            {
                brightness = _timer / (1 / maxBrightness);
            }
            else if (_timer > 4f)
            {
                brightness = (5f - _timer) / (1 / maxBrightness);
            }
            else
            {
                brightness = maxBrightness;
            }

            var colorToDraw = Color.Lerp(_color, Color.Black, 1 - brightness);

            var pos = new Rectangle(xpos, BlockSize * GridY, BlockSize, BlockSize);
            spriteBatch.Draw(_contentDistributionThing.SquareImage, pos, colorToDraw);
        }
    }
}
