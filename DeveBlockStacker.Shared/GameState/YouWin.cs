using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.Drawwers;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DeveBlockStacker.Shared.GameState
{
    public class YouWin : IGameState
    {
        private readonly GameData gameData;
        private int framesDelay;

        private readonly string winString = $"Woooo you won, you're{Environment.NewLine}sooo greaaat!!! Yeaahhh..h.h";
        private Vector2 measuredString;

        private readonly string timeString;
        private Vector2 measuredTimeString;

        public YouWin(GameData gameData)
        {
            this.gameData = gameData;
            timeString = $"Time taken: {gameData.Stopwatch.Elapsed}";

            framesDelay = 500;
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            framesDelay--;
            if (framesDelay <= 0)
            {
                return new NewGameState();
            }
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            if (measuredString == Vector2.Zero)
            {
                measuredString = contentDistributionThing.SegoeUI70.MeasureString(winString);
            }

            if (measuredTimeString == Vector2.Zero)
            {
                measuredTimeString = contentDistributionThing.SegoeUI70.MeasureString(timeString);
            }

            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);


            var pos = new Vector2(contentDistributionThing.ScreenWidth / 2, contentDistributionThing.ScreenHeight / 2 + (contentDistributionThing.ScreenHeight / 5.0f * (float)Math.Sin(framesDelay / 50.0f)));

            var scale = contentDistributionThing.ScreenHeight / (measuredString.X * 1.9f);

            spriteBatch.DrawString(contentDistributionThing.SegoeUI70, winString, pos, Color.White, framesDelay / 50.0f, measuredString / 2, scale * (2 + (float)Math.Sin(framesDelay / 15.0f)), SpriteEffects.None, 0);


            var scaleTimeString = contentDistributionThing.ScreenWidth / (measuredTimeString.X * 1.2f);
            var posTimeString = new Vector2(contentDistributionThing.ScreenWidth / 2, contentDistributionThing.ScreenHeight - (measuredTimeString.Y * scaleTimeString / 2.0f) - 10);
            spriteBatch.DrawString(contentDistributionThing.SegoeUI70, timeString, posTimeString, Color.White, 0, measuredTimeString / 2, scaleTimeString, SpriteEffects.None, 0);
        }
    }
}
