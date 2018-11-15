using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.State;

namespace DeveBlockStacker.Shared.GameState
{
    public interface IGameState
    {
        IGameState Update(InputStatifier inputStatifier, GameData gameData);
    }
}
