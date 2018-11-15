using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.Drawwers;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.GameState
{
    internal class DelayState : IGameState
    {
        private readonly IGameState stateToActivate;
        private int framesDelay;

        public DelayState(IGameState stateToActivate, int framesDelay)
        {
            this.stateToActivate = stateToActivate;
            this.framesDelay = framesDelay;
        }

        public IGameState Update(InputStatifier inputStatifier, GameData gameData)
        {
            framesDelay--;
            if (framesDelay <= 0)
            {
                return stateToActivate;
            }
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing, GameData gameData)
        {
            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);
        }
    }
}
