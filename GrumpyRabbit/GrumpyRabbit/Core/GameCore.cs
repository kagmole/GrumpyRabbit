using GrumpyRabbit.Entities;
using GrumpyRabbit.Entities.Dynamics;
using GrumpyRabbit.Utilities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GrumpyRabbit.Core
{
    public partial class GameCore
    {
        private const double RABBIT_RECTANGLE_HEIGHT = 100;
        private const double RABBIT_RECTANGLE_WIDTH = 100;

        private bool mouseDownOverRabbit;

        private double oldMouseXPosition;
        private double oldMouseYPosition;
        private double deltaMouseXPosition;
        private double deltaMouseYPosition;

        private Canvas worldCanvas;

        private List<Entity> entitiesList;
        private List<Hitbox> worldSquaresHitboxesList;

        public GameCore(Canvas worldCanvas)
        {
            this.worldCanvas = worldCanvas;

            Initialize();
        }

        private void Initialize()
        {
            mouseDownOverRabbit = false;

            oldMouseXPosition = 0.0;
            oldMouseYPosition = 0.0;
            deltaMouseXPosition = 0.0;
            deltaMouseYPosition = 0.0;

            worldCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(WorldCanvas_MouseLeftButtonDown);
            worldCanvas.MouseLeftButtonUp += new MouseButtonEventHandler(WorldCanvas_MouseLeftButtonUp);
            worldCanvas.MouseMove += new MouseEventHandler(WorldCanvas_MouseMove);

            entitiesList = new List<Entity>();
            entitiesList.Add(new RabbitCharacter(0, 0, 0, 0, 0, 0));

            worldCanvas.Children.Add(entitiesList[0].Representation);
            worldCanvas.Children.Add(
        }

        private void WorldCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            deltaMouseXPosition = 0.0;
            deltaMouseYPosition = 0.0;

            oldMouseXPosition = e.GetPosition(worldCanvas).X;
            oldMouseYPosition = e.GetPosition(worldCanvas).Y;

            mouseDownOverRabbit = true;
            worldCanvas.CaptureMouse();
        }

        private void WorldCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mouseDownOverRabbit = false;
            worldCanvas.ReleaseMouseCapture();
        }

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

        public void UpdateGame(int elapsedMilliseconds)
        {
            worldSquaresHitboxesList = WorldGenerator.GetWorldSquaresHitboxesList();

            foreach (Entity entity in entitiesList)
            {
                entity.Update(elapsedMilliseconds);
            }

            foreach (Entity entity in entitiesList)
            {
                entity.LookForCollisions();
            }
        }

        public Canvas WorldCanvas
        {
            get { return worldCanvas; }
        }

        public List<Hitbox> WorldSquaresHitboxesList
        {
            get { return worldSquaresHitboxesList; }
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