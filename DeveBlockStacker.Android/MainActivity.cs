using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using DeveBlockStacker.Core;
using DeveBlockStacker.Core.HelperObjects;

namespace DeveBlockStacker.Android
{
    [Activity(Name = "DeveBlockStacker.Android.MainActivity"
        , Label = "DeveBlockStacker"
        , MainLauncher = true
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.FullSensor
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private void FixUiOptions()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
                Window.InsetsController.Hide(WindowInsets.Type.StatusBars());
                Window.InsetsController.Hide(WindowInsets.Type.SystemBars());
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var g = new TheGame(Platform.Android);
            SetContentView((View)g.Services.GetService(typeof(View)));

            FixUiOptions();
            g.Run();
        }
    }
}

