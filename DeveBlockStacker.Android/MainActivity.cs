using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using DeveBlockStacker.Shared;

namespace DeveBlockStacker.Android
{
    [Activity(Label = "DeveBlockStacker.Android"
        , MainLauncher = true
        , Icon = "@drawable/iconhighres"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.SensorPortrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize )]
    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private void FixUiOptions()
        {
            int uiOptions = (int)Window.DecorView.SystemUiVisibility;

            uiOptions |= (int)SystemUiFlags.LowProfile;
            //uiOptions |= (int)SystemUiFlags.Fullscreen;
            //uiOptions |= (int)SystemUiFlags.HideNavigation;
            //uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            FixUiOptions();

            var metrics = Resources.DisplayMetrics;
            var g = new TheGame(metrics.WidthPixels, metrics.HeightPixels);

            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

