using System;
using System.Collections.Generic;
using System.Text;
using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.Drawwers;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.GameState
{
    public class YouWin : IGameState
    {
        private readonly GameData gameData;
        private int framesDelay;

        public YouWin(GameData gameData)
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
            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);
            spriteBatch.DrawString(contentDistributionThing.SegoeUI20, "Wooo flashyyy screen here!!", new Vector2(30, 30), Color.White, framesDelay / 50.0f, new Vector2(60, 60), 1.0f, SpriteEffects.None, 0);
        }
    }
}
