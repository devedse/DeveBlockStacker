﻿using DeveBlockStacker.Core.Data;
using DeveBlockStacker.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DeveBlockStacker.Core.GameState
{
    public class NewGameState : IGameState
    {
        private int _framesDelay;

        private static readonly string n = Environment.NewLine;

        private readonly string _helloString = $"Hello{n}Welcome to the best game EU!{n}My friend says this game is 5 out of 7!{n}{n}I stopped playing WoW for this game{n}#Gamermagazine2019{n}{n}Haven't been outside for 2 years since the release{n}#TheRealTrump{n}{n}I kinda miss my dog :'({n}#Keanu{n}{n}Made by Devedse";
        private Vector2 _measuredHelloString;

#if NETFRAMEWORK
        private readonly string _version = "_web_";
#else
        private readonly string _version = typeof(NewGameState).Assembly.GetName().Version.ToString();
#endif
        private string _versionString = "";
        private Vector2 _measuredVersionString;

        public NewGameState()
        {
            _framesDelay = 4000;
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            _framesDelay--;
            if (_framesDelay <= 0 || inputStatifier.UserDoesAction())
            {
                return new PlayingState(new GameData());
            }
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            var totSize = contentDistributionThing.ScreenSizeTotal;
            var gameSize = contentDistributionThing.ScreenSizeGame;

            if (_measuredHelloString == Vector2.Zero)
            {
                _measuredHelloString = contentDistributionThing.SecularOne72.MeasureString(_helloString);
            }

            var newVersionString = $"ScreenSize: {totSize.Width}x{totSize.Height}{n}GameSize: {gameSize.Width}x{gameSize.Height}{n}Version: {_version}";
            if (_measuredVersionString == Vector2.Zero || newVersionString != _versionString)
            {
                _versionString = newVersionString;
                _measuredVersionString = contentDistributionThing.SecularOne72.MeasureString(_versionString);
            }

            var posHelloString = new Vector2(gameSize.Width / 2, gameSize.Height / 2 - gameSize.Height / 10f + (gameSize.Height / 5.0f * (float)Math.Sin(_framesDelay / 20.0f)));
            var scaleHelloString = gameSize.Width / (_measuredHelloString.X * 1.3f);

            //Take up 50% of the screen (in width) (also we use {sideDistance} to move it away from the side)
            int sideDistance = 5;
            var scaleVersionString = (1f / ((_measuredVersionString.X + sideDistance) / gameSize.Width)) * 0.25f;
            var scaledMeasuredString = _measuredVersionString * scaleVersionString;
            var posVersionString = new Vector2(gameSize.Width - scaledMeasuredString.X - sideDistance, gameSize.Height - scaledMeasuredString.Y - sideDistance);

            spriteBatch.DrawString(contentDistributionThing.SecularOne72, _helloString, posHelloString, Color.White, 0, _measuredHelloString / 2, scaleHelloString, SpriteEffects.None, 0);
            spriteBatch.DrawString(contentDistributionThing.SecularOne72, _versionString, posVersionString, Color.White, 0, Vector2.Zero, scaleVersionString, SpriteEffects.None, 0);
        }
    }
}
