using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.GameState
{
    public interface IGameState
    {
        IGameState Update(InputStatifier inputStatifier);
        void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing);
    }
}
