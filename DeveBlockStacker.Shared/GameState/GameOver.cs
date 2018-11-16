using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.Drawwers;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DeveBlockStacker.Shared.GameState
{
    public class GameOver : IGameState
    {
        private readonly GameData gameData;
        private int framesDelay;

        private readonly string gameOverString = $"Youuuuu...{Environment.NewLine}SUCKKKKK{Environment.NewLine}{Environment.NewLine}      :'(";
        private Vector2 measuredString;

        public GameOver(GameData gameData)
        {
            this.gameData = gameData;

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
                measuredString = contentDistributionThing.SegoeUI70.MeasureString(gameOverString);
            }

            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);

            var pos = new Vector2(contentDistributionThing.ScreenWidth / 2, contentDistributionThing.ScreenHeight / 2 + (contentDistributionThing.ScreenHeight / 5.0f * (float)Math.Sin(framesDelay / 20.0f)));

            var scale = contentDistributionThing.ScreenWidth / (measuredString.X * 1.3f);

            spriteBatch.DrawString(contentDistributionThing.SegoeUI70, gameOverString, pos, Color.White, 0, measuredString / 2, scale + ((1.0f + (float)Math.Sin(framesDelay / 6.0f)) * 0.15f), SpriteEffects.None, 0);
        }
    }
}
