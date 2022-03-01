using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Core
{
    public class Blocketje
    {
        private readonly Texture2D squareTexture;

        public float Width { get; set; }
        public float Height { get; set; }

        public float X { get; set; }
        public float Y { get; set; }

        public float Gap { get; set; }

        //public float XMiddle
        //{
        //    get => X + Width / 2;
        //    set => X = value - Width / 2;
        //}

        //public float YBottom
        //{
        //    get => Y + Height;
        //    set => Y = value - Height;
        //}

        public Blocketje(Texture2D squareTexture, float x, float y, float width, float height, float gap)
        {
            this.squareTexture = squareTexture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Gap = gap;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            float realWidth = Width / 2.0f - Gap / 2.0f;
            float realHeight = Height / 2.0f - Gap / 2.0f;

            int drawX = (int)(X + Gap / 2.0f);
            int drawY = (int)(Y + Gap / 2.0f);
            int drawWidth = (int)(realWidth - Gap / 2.0f);
            int drawHeight = (int)(realHeight - Gap / 2.0f);

            int drawXRight = (int)(X + Width / 2.0f + Gap / 2.0f);
            int drawYBottom = (int)(Y + Height / 2.0f + Gap / 2.0f);

            spriteBatch.Draw(squareTexture, new Rectangle(drawX, drawY, drawWidth, drawHeight), color);
            spriteBatch.Draw(squareTexture, new Rectangle(drawXRight, drawY, drawWidth, drawHeight), color);
            spriteBatch.Draw(squareTexture, new Rectangle(drawX, drawYBottom, drawWidth, drawHeight), color);
            spriteBatch.Draw(squareTexture, new Rectangle(drawXRight, drawYBottom, drawWidth, drawHeight), color);
        }
    }
}
