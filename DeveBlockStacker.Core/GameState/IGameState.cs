using DeveBlockStacker.Core.State;
using DeveBlockStacker.Core.Data;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Core.GameState
{
    public interface IGameState
    {
        IGameState Update(InputStatifier inputStatifier);
        void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing);
    }
}
