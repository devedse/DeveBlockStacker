using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using DeveBlockStacker.Core;
using DeveBlockStacker.Core.HelperObjects;

namespace DeveBlockStacker.Android
{
    [Activity(Label = "DeveBlockStacker"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.SensorPortrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private void FixUiOptions()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
                Window.SetDecorFitsSystemWindows(false);
            }
            else
            {
                //int uiOptions = (int)Window.DecorView.SystemUiVisibility;

                //uiOptions |= (int)SystemUiFlags.LowProfile;
                ////uiOptions |= (int)SystemUiFlags.Fullscreen;
                ////uiOptions |= (int)SystemUiFlags.HideNavigation;
                ////uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                //Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            FixUiOptions();

            var g = new TheGame(Platform.Android);

            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

