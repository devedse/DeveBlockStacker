using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using DeveBlockStacker.Shared;

namespace DeveBlockStacker.Android
{
    [Activity(Label = "DeveBlockStacker.Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var metrics = Resources.DisplayMetrics;
            var g = new TheGame(metrics.WidthPixels, metrics.HeightPixels);

            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

