using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.Drawwers;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.GameState
{
    internal class DelayState : IGameState
    {
        private readonly GameData gameData;

        private readonly IGameState stateToActivate;
        private int framesDelay;

        public DelayState(GameData gameData, IGameState stateToActivate, int framesDelay)
        {
            this.gameData = gameData;
            this.stateToActivate = stateToActivate;
            this.framesDelay = framesDelay;
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            framesDelay--;
            if (framesDelay <= 0)
            {
                return stateToActivate;
            }
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing, GameTime time)
        {
            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData, time);
        }
    }
}
