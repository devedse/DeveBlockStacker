using DeveBlockStacker.Core.Data;
using DeveBlockStacker.Core.Drawers;
using DeveBlockStacker.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;

namespace DeveBlockStacker.Core.GameState
{
    public class YouWin : IGameState
    {
        private readonly GameData gameData;
        private int framesDelay;

        private readonly string winString = $"Woooo you won, you're{Environment.NewLine}sooo greaaat!!! Yeaahhh..h.h";
        private Vector2 measuredString;

        private readonly string timeString;
        private Vector2 measuredTimeString;

        private string previousHighscoreString;
        private Vector2 measuredPreviousHighScoreString;

        public YouWin(GameData gameData)
        {
            this.gameData = gameData;
            gameData.Stopwatch.Stop();
            timeString = $"Time taken: {gameData.Stopwatch.Elapsed}";

            framesDelay = 500;

            Task.Run(async () =>
            {
                var saveGame = await SaveGameLoaderSaver.LoadSaveGame();
                if (saveGame == null || saveGame.BestTime > gameData.Stopwatch.Elapsed)
                {
                    saveGame = new SaveGame()
                    {
                        BestTime = gameData.Stopwatch.Elapsed
                    };
                    previousHighscoreString = "NEW HIGH SCORE!";
                    await SaveGameLoaderSaver.SaveSaveGame(saveGame);
                }
                else
                {
                    previousHighscoreString = $"Previous high score: {saveGame.BestTime}";
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
            var gameSize = contentDistributionThing.ScreenSizeGame;

            if (measuredString == Vector2.Zero)
            {
                measuredString = contentDistributionThing.SecularOne72.MeasureString(winString);
            }

            if (measuredTimeString == Vector2.Zero)
            {
                measuredTimeString = contentDistributionThing.SecularOne72.MeasureString(timeString);
            }

            if (previousHighscoreString != null && measuredPreviousHighScoreString == Vector2.Zero)
            {
                measuredPreviousHighScoreString = contentDistributionThing.SecularOne72.MeasureString(previousHighscoreString);
            }

            NormalGridDrawer.DrawGrid(spriteBatch, contentDistributionThing, gameData);


            var pos = new Vector2(gameSize.Width / 2, gameSize.Height / 2 + (gameSize.Height / 5.0f * (float)Math.Sin(framesDelay / 50.0f)));

            var scale = gameSize.Height / (measuredString.X * 1.9f);

            spriteBatch.DrawString(contentDistributionThing.SecularOne72, winString, pos, Color.White, framesDelay / 50.0f, measuredString / 2, scale * (2 + (float)Math.Sin(framesDelay / 15.0f)), SpriteEffects.None, 0);


            var scaleTimeString = gameSize.Width / (measuredTimeString.X * 1.4f);
            var posTimeString = new Vector2(gameSize.Width / 2, 5 + (measuredTimeString.Y * scaleTimeString / 2.0f));
            spriteBatch.DrawString(contentDistributionThing.SecularOne72, timeString, posTimeString, Color.White, 0, measuredTimeString / 2, scaleTimeString, SpriteEffects.None, 0);

            if (measuredPreviousHighScoreString != Vector2.Zero)
            {
                var posPrev = new Vector2(gameSize.Width / 2, posTimeString.Y + (measuredTimeString.Y * scaleTimeString / 2.0f) + measuredPreviousHighScoreString.Y * scale / 2.0f);
                spriteBatch.DrawString(contentDistributionThing.SecularOne72, previousHighscoreString, posPrev, Color.White, 0, measuredPreviousHighScoreString / 2, scaleTimeString, SpriteEffects.None, 0);

            }
        }
    }
}