using DeveBlockStacker.Core.Data;
using DeveBlockStacker.Core.Drawers;
using DeveBlockStacker.Core.State;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Core.GameState
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

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            NormalGridDrawer.DrawGrid(spriteBatch, contentDistributionThing, gameData);
        }
    }
}
