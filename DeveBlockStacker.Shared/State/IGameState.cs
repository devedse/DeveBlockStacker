using DeveBlockStacker.Shared.Data;

namespace DeveBlockStacker.Shared.State
{
    public interface IGameState
    {
        void Update(GameData gameData);
    }
}
