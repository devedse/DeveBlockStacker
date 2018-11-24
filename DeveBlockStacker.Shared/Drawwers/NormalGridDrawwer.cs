using DeveBlockStacker.Shared.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DeveBlockStacker.Shared.Drawwers
{
    public static class NormalGridDrawwer
    {

        public class Shake {

            public static Random RANDOM = new Random();

            // Shakes camera
            public float duration = 2000;
            public float frequency = 40;

            public int sampleCount;
            public float[] noiseSamples;

            public float shakeTime = 0;
            public bool isShaking = false;

            public Shake(float duration, float frequency, float amplitude) {
                this.duration = duration;
                this.frequency = frequency;

                sampleCount = (int)Math.Round(duration / frequency);

                // Populate the samples array with randomized values between -1.0 and 1.0
                noiseSamples = new float[sampleCount];
                for (var i = 0; i < sampleCount; i++){
                    noiseSamples[i] = (float)(RANDOM.NextDouble() * 2 - 1) * amplitude;
                }
            }

            public void Update(float timeInMs){
                if (!isShaking) return;
                if (shakeTime >= duration) {
                    // Shake runs after update, so we need one frame with the final "shake"
                    isShaking = false;
                } else {
                    shakeTime = Math.Min(duration, shakeTime + timeInMs);
                }
            }
            
            public float Amplitude()
            {
                // Get the previous and next sample
                var s = shakeTime / 1000 * frequency;
                int s0 = (int) Math.Floor(s);
                int s1 = s0 + 1;
                                
                // Return the current amplitude
                return (Noise(s0) + (s - s0) * Noise(s1) - Noise(s0)) * Decay();
            }

            private float Decay()
            {
                if (shakeTime >= duration) return 0;
                return (duration - shakeTime) / duration;
            }

            private float Noise(int s)            {
                // Retrieve the randomized value from the samples
                return s >= noiseSamples.Length ? 0 : noiseSamples[s];
            }

            public void Start(){
                isShaking = true;
                shakeTime = 0;
            }

        }

        /// <summary>        /// Simple class to combine the two shakes to produce a Vector because I'm a lazy boy        /// </summary>
        public class ShakeVector2 {
            
            public bool IsShaking{ get { return X.isShaking | Y.isShaking; } }

            public Shake X, Y;

            public ShakeVector2(float duration, float frequency, float amplitude){
                X = new Shake(duration, frequency, amplitude);
                Y = new Shake(duration, frequency, amplitude);
            }

            public void Update(float timeInMs)
            {
                X.Update(timeInMs);
                Y.Update(timeInMs);
            }

            public Vector2 Amplitude(){
                return new Vector2(X.Amplitude(), Y.Amplitude());
            }

            public void Start()
            {
                X.Start();
                Y.Start();
            }

        }

        public static float GLOBAL_OFFSET = 0, LOCAL_OFFSET = 0;
        public static float GLOBAL_FREQUENCY = 50, LOCAL_FREQUENCY = 20;

        public static ShakeVector2 GLOBAL_CAMERA_SHAKE = new ShakeVector2(1000, GLOBAL_FREQUENCY, GLOBAL_OFFSET);
        public static ShakeVector2[,] LOCAL_BLOCK_SHAKE;

        public static void DrawGrid(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing, GameData gameData, GameTime time)
        {

            float ms = (float) time.ElapsedGameTime.TotalMilliseconds;

            GLOBAL_CAMERA_SHAKE.Update(ms);

            if (LOCAL_BLOCK_SHAKE == null){
                LOCAL_BLOCK_SHAKE = new ShakeVector2[gameData.GridWidth, gameData.GridHeight];

                for (int y = gameData.GridHeight - 1; y >= 0; y--) {
                    for (int x = gameData.GridWidth - 1; x >= 0; x--) {
                        LOCAL_BLOCK_SHAKE[x, y] = new ShakeVector2(2500, LOCAL_FREQUENCY, LOCAL_OFFSET);
                    }
                }
            }

            Vector2 globalShake = GLOBAL_CAMERA_SHAKE.Amplitude();
            
            for (int y = gameData.GridHeight - 1; y >= 0; y--)
            {
                for (int x = gameData.GridWidth - 1; x >= 0; x--)
                {
                    float widthOfBlockje = contentDistributionThing.ScreenWidth / gameData.GridWidth;
                    float heightOfBlockje = contentDistributionThing.ScreenHeight / gameData.GridHeight;

                    float yyy = contentDistributionThing.ScreenHeight - ((1 + y) * heightOfBlockje);
                    var blocketje = new Blocketje(contentDistributionThing.SquareImage, x * widthOfBlockje, yyy, widthOfBlockje, heightOfBlockje, 3);

                    if (gameData.Gridje[x, y] == true)
                    {
                        Vector2 localShake = Vector2.Zero;
                        //if (GLOBAL_CAMERA_SHAKE.IsShaking){
                            LOCAL_BLOCK_SHAKE[x, y].Update(ms);
                            localShake = LOCAL_BLOCK_SHAKE[x, y].Amplitude();
                        //}

                        blocketje.Draw(spriteBatch, y > 7 ? Color.Red : Color.Blue, globalShake + localShake);
                    }
                }
            }
        }
    }
}
