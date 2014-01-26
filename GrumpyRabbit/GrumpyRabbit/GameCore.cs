using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GrumpyRabbit
{
    /// <summary>
    /// This is the game's core. Every "top" elements management is here.
    /// 
    /// For the moment, it manages the "world" (the transparent canvas covering the screen)
    /// and the rabbit (who is a rectangle in this example). It also catches left mouse button
    /// clicks and "drag" moves when over the rabbit.
    /// 
    /// Because we don't know if every elements in the game will need the same amount of
    /// parameters as the rabbit, we use a dictionary to register and use them.
    /// </summary>
    public class GameCore
    {
        private const double RABBIT_RECTANGLE_HEIGHT = 100;
        private const double RABBIT_RECTANGLE_WIDTH = 100;

        private bool mouseDownOverRabbit;

        private double oldMouseXPosition;
        private double oldMouseYPosition;
        private double deltaMouseXPosition;
        private double deltaMouseYPosition;

        private Canvas worldCanvas;

        private Dictionary<string, double> gameParameters;

        private List<SquareHitbox> worldSquaresHitboxesList;

        private RabbitStateManager rabbitStateManager;

        private Rectangle rabbitRectangle;

        /// <summary>
        /// Main constructor. It only needs the transparent canvas covering the main screen.
        /// </summary>
        /// <param name="canvas">World canvas</param>
        public GameCore(Canvas canvas)
        {
            worldCanvas = canvas;

            InitializeCore();
        }

        /// <summary>
        /// Initialize all attributes of the game's core. Is also adds events handler where
        /// they are needed.
        /// </summary>
        private void InitializeCore()
        {
            mouseDownOverRabbit = false;

            oldMouseXPosition = 0.0;
            oldMouseYPosition = 0.0;
            deltaMouseXPosition = 0.0;
            deltaMouseYPosition = 0.0;

            worldCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(WorldCanvas_MouseLeftButtonDown);
            worldCanvas.MouseLeftButtonUp += new MouseButtonEventHandler(WorldCanvas_MouseLeftButtonUp);
            worldCanvas.MouseMove += new MouseEventHandler(WorldCanvas_MouseMove);

            gameParameters = new Dictionary<string, double>();

            rabbitStateManager = new RabbitStateManager(this);

            rabbitRectangle = new Rectangle();

            rabbitRectangle.Width = RABBIT_RECTANGLE_WIDTH;
            rabbitRectangle.Height = RABBIT_RECTANGLE_HEIGHT;

            Canvas.SetLeft(rabbitRectangle, SystemParameters.WorkArea.Right - RABBIT_RECTANGLE_WIDTH);
            Canvas.SetTop(rabbitRectangle, SystemParameters.WorkArea.Bottom - RABBIT_RECTANGLE_HEIGHT);

            // XXX Temp code.
            rabbitRectangle.Fill = new SolidColorBrush(Colors.Red);

            worldCanvas.Children.Add(rabbitRectangle);
        }

        /// <summary>
        /// Event handler for the mouse left button down.
        /// </summary>
        /// <param name="sender">Target receiving the event</param>
        /// <param name="e">Event arguments</param>
        private void WorldCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rabbitStateManager.ChangeMoveState(RabbitStateManager.MoveStateID.GRAB);

            deltaMouseXPosition = 0.0;
            deltaMouseYPosition = 0.0;

            oldMouseXPosition = e.GetPosition(worldCanvas).X;
            oldMouseYPosition = e.GetPosition(worldCanvas).Y;

            mouseDownOverRabbit = true;

            /* A transparent UIElement doesn't catch events. Because of this, there is a chance
             * that users move their mouse too fast and exits the rabbit's rectangle, losing
             * the mouse focus. With CaptureMouse, we capture the mouse focus and ensure to
             * catch events even if we are over a transparent background. */
            worldCanvas.CaptureMouse();
        }

        /// <summary>
        /// Event handler for the mouse left button up.
        /// </summary>
        /// <param name="sender">Target receiving the event</param>
        /// <param name="e">Event arguments</param>
        private void WorldCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rabbitStateManager.ChangeMoveState(RabbitStateManager.MoveStateID.FALL);

            mouseDownOverRabbit = false;

            /* Release the mouse focus, captured above. */
            worldCanvas.ReleaseMouseCapture();
        }

        /// <summary>
        /// Event handler for the mouse move. In this case, we only use it to "drag" the
        /// rabbit.
        /// </summary>
        /// <param name="sender">Target receving the event</param>
        /// <param name="e">Event arguments</param>
        private void WorldCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownOverRabbit)
            {
                deltaMouseXPosition += e.GetPosition(worldCanvas).X - oldMouseXPosition;
                deltaMouseYPosition += e.GetPosition(worldCanvas).Y - oldMouseYPosition;

                oldMouseXPosition = e.GetPosition(worldCanvas).X;
                oldMouseYPosition = e.GetPosition(worldCanvas).Y;
            }
        }

        /// <summary>
        /// Main method of the game's core. It updats the game state.
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed milliseconds since last frame</param>
        public void UpdateGame(int elapsedMilliseconds)
        {
            /* Get system windows as boxes. */
            worldSquaresHitboxesList = WorldGenerator.GetWorldSquaresHitboxesList();

            /* Update rabbit state. */
            rabbitStateManager.UpdateRabbit(elapsedMilliseconds);
        }

        public Canvas WorldCanvas
        {
            get { return worldCanvas; }
        }

        public Dictionary<string, double> GameParameters
        {
            get { return gameParameters; }
        }

        public List<SquareHitbox> WorldSquaresHitboxesList
        {
            get { return worldSquaresHitboxesList; }
        }

        public Rectangle RabbitRectangle
        {
            get { return rabbitRectangle; }
        }

        public double DeltaMouseXPosition
        {
            get
            {
                double result = deltaMouseXPosition;
                deltaMouseXPosition = 0.0;

                return result;
            }
        }

        public double DeltaMouseYPosition
        {
            get
            {
                double result = deltaMouseYPosition;
                deltaMouseYPosition = 0.0;

                return result;
            }
        }
    }
}