using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GrumpyRabbit.Entities
{
    public abstract class Entity
    {
        protected int id;

        protected int left;
        protected int top;
        protected int right;
        protected int bottom;

        protected Rectangle representation;

        public Entity(int id, int left, int top, int right, int bottom)
        {
            this.id = id;

            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;

            representation = new Rectangle();
            representation.Width = 100;
            representation.Height = 100;
            Canvas.SetTop(representation, 100);
            Canvas.SetLeft(representation, 100);
            representation.Fill = new SolidColorBrush(Colors.Red);
        }

        public abstract void Update(int elapsedMilliseconds);
        public abstract void LookForCollisions();

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        public int Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        public Rectangle Representation
        {
            get { return representation; }
            set { representation = value; }
        }
    }
}
