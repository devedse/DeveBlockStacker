using DeveBlockStacker.Core;
using DeveBlockStacker.Core.HelperObjects;
using static Retyped.dom;

namespace Platformer2D
{
    public class App
    {
        private static TheGame game;

        public static void Main()
        {
            var canvasSize = new IntSize(400, 800);

            var div = new HTMLDivElement();
            div.style.width = $"{canvasSize.Width}px";
            div.style.height = $"{canvasSize.Height}px";
            document.body.appendChild(div);

            var button = new HTMLButtonElement();
            button.innerHTML = "Click on game area to start it!";
            button.style.width = "100%";
            button.style.height = "100%";
            button.style.backgroundColor = "#6495ED";
            button.style.color = "#ffffff";
            button.style.fontSize = "20px";
            div.appendChild(button);

            button.onclick = (ev) =>
            {
                div.removeChild(button);

                var canvas = new HTMLCanvasElement();
                canvas.style.width = "100%";
                canvas.style.height = "100%";
                canvas.id = "monogamecanvas";
                div.appendChild(canvas);

                game = new TheGame(canvasSize, Platform.Desktop);
                game.Run();
            };
        }
    }
}
