using DeveBlockStacker.Core.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveBlockStacker.Core.Drawers
{
    public class EffectBlockDrawer
    {
        private readonly ContentDistributionThing _contentDistributionThing;
        private Queue<EffectBlock> _blocketjes = new Queue<EffectBlock>();

        private double _timer;
        private double _lastSpawn;

        private double _timeBetweenSpawns = 0.05;

        private Random _random = new Random();

        public EffectBlockDrawer(ContentDistributionThing contentDistributionThing)
        {
            _contentDistributionThing = contentDistributionThing;
        }

        public void Update(GameTime gameTime)
        {
            var game = _contentDistributionThing.ScreenSizeGame;
            var tot = _contentDistributionThing.ScreenSizeTotal;

            _timer += gameTime.ElapsedGameTime.TotalSeconds;

            //Only do this effect when there's room to show it (widescreen displays)
            if (game.X > 0)
            {
                foreach (var block in _blocketjes)
                {
                    block.Update(gameTime);
                }

                while (_blocketjes.Count > 0 && _blocketjes.Peek().Completed)
                {
                    _blocketjes.Dequeue();
                }

                if (_timer - _lastSpawn > _timeBetweenSpawns)
                {
                    int validXNumber = game.X / EffectBlock.BlockSize;
                    int validYNumber = tot.Height / EffectBlock.BlockSize;

                    int curTry = 0;
                    //Try 10 times to not place multiple blocks on the same location
                    while (curTry < 10)
                    {
                        var gridPosX = _random.Next(validXNumber + 1);
                        var gridPosY = _random.Next(validYNumber + 1);
                        var leftSide = _random.NextDouble() > 0.5;

                        if (!_blocketjes.Any(t => t.GridX == gridPosX && t.GridY == gridPosY && t.LeftSide == leftSide))
                        {
                            var randomColor = new Color((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble());

                            _blocketjes.Enqueue(new EffectBlock(randomColor, gridPosX, gridPosY, leftSide, _contentDistributionThing));
                            _lastSpawn = _timer;
                            break;
                        }
                        curTry++;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var game = _contentDistributionThing.ScreenSizeGame;

            //Only do this effect when there's room to show it (widescreen displays)
            if (game.X > 0)
            {
                foreach (var block in _blocketjes)
                {
                    block.Draw(spriteBatch);
                }
            }
        }
    }
}
