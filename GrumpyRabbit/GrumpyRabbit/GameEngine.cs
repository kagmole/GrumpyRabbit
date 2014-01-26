using System;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;

namespace GrumpyRabbit
{
    /// <summary>
    /// The game engine ensure the render loop.
    /// 
    /// Because it is WPF who "decides" when render is done, we only care for
    /// init and update steps.
    /// </summary>
    public class GameEngine
    {
        private long deltaTime;
        private long lastFrame;

        private GameCore gameCore;

        private Stopwatch renderingStopwatch;

        /// <summary>
        /// Main constructor. Needs the transparent canvas covering the screen.
        /// </summary>
        /// <param name="canvas">World canvas</param>
        public GameEngine(Canvas canvas)
        {
            renderingStopwatch = new Stopwatch();

            gameCore = new GameCore(canvas);
        }

        /// <summary>
        /// Start the render loop and the rendering watch.
        /// </summary>
        public void LaunchEngine()
        {
            if (renderingStopwatch.IsRunning)
            {
                return;
            }

            /* Only GUI thread can alter the GUI. There is a lot of ways to work with
             * the GUI thread, but one of the more efficient ways is to add jobs
             * to the GUI thread just before the call of OnRender methods. This is
             * done by adding an event handler */
            CompositionTarget.Rendering += new EventHandler(GameEngine_Rendering);

            lastFrame = 0;

            renderingStopwatch.Start();
        }

        /// <summary>
        /// Event handler for the rendring.
        /// 
        /// It calculates elapsed milliseconds since last frame before updating the game.
        /// </summary>
        /// <param name="sender">Target of the event handler</param>
        /// <param name="e">Event arguments</param>
        private void GameEngine_Rendering(object sender, EventArgs e)
        {
            deltaTime = renderingStopwatch.ElapsedMilliseconds - lastFrame;
            lastFrame += deltaTime;

            gameCore.UpdateGame((int) deltaTime);
        }
    }
}
