using DeveBlockStacker.Core;
using DeveBlockStacker.Core.HelperObjects;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Microsoft.Xna.Framework;

namespace DeveBlockStacker.Blazor.Pages
{
    public partial class Index
    {
        Game _game;

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                JsRuntime.InvokeAsync<object>("initRenderJS", DotNetObjectReference.Create(this));
            }
        }

        [JSInvokable]
        public void TickDotNet()
        {
            // init game
            if (_game == null)
            {
                _game = new TheGame(new CustomEmbeddedResourceLoader(), new IntSize(400, 800), Platform.Desktop);
                _game.Run();
            }

            // run gameloop
            _game.Tick();
        }

        public void OnTouchStart(TouchEventArgs e)
        {
            //currTouchState.Position.X = (float)e.ChangedTouches[0].ClientX;
            //currTouchState.Position.Y = (float)e.ChangedTouches[0].ClientY;
            //currTouchState.IsPressed = true;
            //prevTouchState = currTouchState;
        }

        public void OnTouchMove(TouchEventArgs e)
        {
            //    currTouchState.Position.X = (float)e.ChangedTouches[0].ClientX;
            //    currTouchState.Position.Y = (float)e.ChangedTouches[0].ClientY;
        }

        public void OnTouchEnd(TouchEventArgs e)
        {
            //    currTouchState.Position.X = (float)e.ChangedTouches[0].ClientX;
            //    currTouchState.Position.Y = (float)e.ChangedTouches[0].ClientY;
            //    currTouchState.IsPressed = false;
        }


    }
}