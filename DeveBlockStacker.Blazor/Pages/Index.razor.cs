using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Microsoft.Xna.Framework;
using tainicom.Wasm.Canvas;
using tainicom.Wasm.Canvas.WebGL;
using DeveBlockStacker.Blazor;

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
                _game = new Game1();
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