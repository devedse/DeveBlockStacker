using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.GameState
{
    public class NewGameState : IGameState
    {
        private readonly GameData gameData;
        private int framesDelay;

        public NewGameState()
        {
            this.gameData = new GameData();

            framesDelay = 30;
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
            spriteBatch.DrawString(contentDistributionThing.SegoeUI70, "Helloooo!!! :)", new Vector2(60, framesDelay), Color.White);
        }
    }
}
