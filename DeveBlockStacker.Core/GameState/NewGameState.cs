using DeveBlockStacker.Core.State;
using DeveBlockStacker.Core.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeveBlockStacker.Core.GameState
{
    public class NewGameState : IGameState
    {
        private int framesDelay;

        private static readonly string n = Environment.NewLine;

        private readonly string helloString = $"Hello{n}Welcome to the best game EU!{n}My friend says this game is 5 out of 7!{n}{n}I stopped playing WoW for this game{n}#Gamermagazine2019{n}{n}Haven't been outside for 2 years since the release{n}#TheRealTrump{n}{n}I kinda miss my dog :'({n}#Keanu{n}{n}Made by Devedse";
        private Vector2 measuredHelloString;

        private readonly string versionString = $"Version: {typeof(NewGameState).Assembly.GetName().Version}";
        private Vector2 measuredVersionString;

        public NewGameState()
        {
            framesDelay = 400;
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            framesDelay--;
            if (framesDelay <= 0 || inputStatifier.IsTouchTapped() || inputStatifier.KeyPressed(Keys.Space) || inputStatifier.GamepadButtonPressed(Buttons.A))
            {
                return new PlayingState(new GameData());
            }
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            if (measuredHelloString == Vector2.Zero)
            {
                measuredHelloString = contentDistributionThing.SecularOne72.MeasureString(helloString);
            }
            if (measuredVersionString == Vector2.Zero)
            {
                measuredVersionString = contentDistributionThing.SecularOne72.MeasureString(versionString);
            }

            var posHelloString = new Vector2(contentDistributionThing.ScreenWidth / 2, contentDistributionThing.ScreenHeight / 2 - contentDistributionThing.ScreenHeight / 10f + (contentDistributionThing.ScreenHeight / 5.0f * (float)Math.Sin(framesDelay / 20.0f)));
            var scaleHelloString = contentDistributionThing.ScreenWidth / (measuredHelloString.X * 1.3f);

            //Take up 50% of the screen (in width) (also we use {sideDistance} to move it away from the side)
            int sideDistance = 5;
            var scaleVersionString = (1f / ((measuredVersionString.X + sideDistance) / contentDistributionThing.ScreenWidth)) * 0.25f;
            var scaledMeasuredString = measuredVersionString * scaleVersionString;
            var posVersionString = new Vector2(contentDistributionThing.ScreenWidth - scaledMeasuredString.X - sideDistance, contentDistributionThing.ScreenHeight - scaledMeasuredString.Y - sideDistance);

            spriteBatch.DrawString(contentDistributionThing.SecularOne72, helloString, posHelloString, Color.White, 0, measuredHelloString / 2, scaleHelloString, SpriteEffects.None, 0);
            spriteBatch.DrawString(contentDistributionThing.SecularOne72, versionString, posVersionString, Color.White, 0, Vector2.Zero, scaleVersionString, SpriteEffects.None, 0);
        }
    }
}
