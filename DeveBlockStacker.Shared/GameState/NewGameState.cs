using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DeveBlockStacker.Shared.GameState
{
    public class NewGameState : IGameState
    {
        private readonly GameData gameData;
        private int framesDelay;

        private readonly string helloString = $"Hello{Environment.NewLine}Welcome to the best game ever!{Environment.NewLine}Have fun{Environment.NewLine}{Environment.NewLine}By Devedse";
        private Vector2 measuredString;

        public NewGameState()
        {
            gameData = new GameData();

            framesDelay = 300;
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            framesDelay--;
            if (framesDelay <= 0)
            {
                return new PlayingState(gameData);
            }
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            if (measuredString == Vector2.Zero)
            {
                measuredString = contentDistributionThing.SegoeUI70.MeasureString(helloString);
            }


            var pos = new Vector2(contentDistributionThing.ScreenWidth / 2, contentDistributionThing.ScreenHeight / 2 - contentDistributionThing.ScreenHeight / 10f + (contentDistributionThing.ScreenHeight / 5.0f * (float)Math.Sin(framesDelay / 20.0f)));
            var scale = contentDistributionThing.ScreenWidth / (measuredString.X * 1.3f);

            spriteBatch.DrawString(contentDistributionThing.SegoeUI70, helloString, pos, Color.White, 0, measuredString / 2, scale, SpriteEffects.None, 0);
        }
    }
}
