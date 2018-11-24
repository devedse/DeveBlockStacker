using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeveBlockStacker.Shared.GameState
{
    public class NewGameState : IGameState
    {
        private int framesDelay;

        private static readonly string n = Environment.NewLine;

        private readonly string helloString = $"Hello{n}Welcome to the best game EU!{n}My friend says this game is 5 out of 7!{n}{n}I stopped playing WoW for this game{n}#Gamermagazine2019{n}{n}Haven't been outside for 2 years since the release{n}#TheRealTrump{n}{n}I kinda miss my dog :'({n}#Keanu{n}{n}Made by Devedse";
        private Vector2 measuredString;

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

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing, GameTime time)
        {
            if (measuredString == Vector2.Zero)
            {
                measuredString = contentDistributionThing.SegoeUI70.MeasureString(helloString);
            }


            var pos = new Vector2(contentDistributionThing.ScreenWidth / 2, contentDistributionThing.ScreenHeight / 2 - contentDistributionThing.ScreenHeight / 10f + (contentDistributionThing.ScreenHeight / 5.0f * (float)Math.Sin(framesDelay / 20.0f)));
            var scale = contentDistributionThing.ScreenWidth / (measuredString.X * 1.3f);

            spriteBatch.DrawString(contentDistributionThing.SegoeUI70, helloString, pos, Color.White, 0, measuredString / 2, scale, SpriteEffects.None, 0);
        }
    }
}
