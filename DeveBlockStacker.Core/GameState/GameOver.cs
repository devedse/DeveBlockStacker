using DeveBlockStacker.Core.Data;
using DeveBlockStacker.Core.Drawwers;
using DeveBlockStacker.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading.Tasks;

namespace DeveBlockStacker.Core.GameState
{
    public class GameOver : IGameState
    {
        private readonly GameData gameData;
        private int framesDelay;

        private readonly string gameOverString = $"Youuuuu...{Environment.NewLine}SUCKKKKK{Environment.NewLine}{Environment.NewLine}      :'(";
        private Vector2 measuredString;

        private string previousHighscoreString;
        private Vector2 measuredPreviousHighScoreString;

        public GameOver(GameData gameData)
        {
            this.gameData = gameData;

            framesDelay = 500;

            Task.Run(async () =>
            {
                var saveGame = await SaveGameLoaderSaver.LoadSaveGame();
                if (saveGame == null)
                {
                    previousHighscoreString = "No high scores yet :(";
                }
                else
                {
                    previousHighscoreString = $"Current high score: {saveGame.BestTime}";
                }
            });
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            framesDelay--;
            if (framesDelay <= 0 || inputStatifier.UserDoesAction())
            {
                return new NewGameState();
            }
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            if (measuredString == Vector2.Zero)
            {
                measuredString = contentDistributionThing.SecularOne72.MeasureString(gameOverString);
            }

            if (previousHighscoreString != null && measuredPreviousHighScoreString == Vector2.Zero)
            {
                measuredPreviousHighScoreString = contentDistributionThing.SecularOne72.MeasureString(previousHighscoreString);
            }

            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);

            var pos = new Vector2(contentDistributionThing.ScreenWidth / 2, contentDistributionThing.ScreenHeight / 2 + (contentDistributionThing.ScreenHeight / 5.0f * (float)Math.Sin(framesDelay / 20.0f)));

            var scale = contentDistributionThing.ScreenWidth / (measuredString.X * 1.3f);

            spriteBatch.DrawString(contentDistributionThing.SecularOne72, gameOverString, pos, Color.White, 0, measuredString / 2, scale + ((1.0f + (float)Math.Sin(framesDelay / 6.0f)) * 0.15f), SpriteEffects.None, 0);

            if (measuredPreviousHighScoreString != Vector2.Zero)
            {
                var scalePrev = contentDistributionThing.ScreenWidth / (measuredPreviousHighScoreString.X * 1.3f);
                var posPrev = new Vector2(contentDistributionThing.ScreenWidth / 2, 5 + measuredPreviousHighScoreString.Y * scale / 2.0f);
                spriteBatch.DrawString(contentDistributionThing.SecularOne72, previousHighscoreString, posPrev, Color.White, 0, measuredPreviousHighScoreString / 2, scalePrev, SpriteEffects.None, 0);
            }
        }
    }
}
