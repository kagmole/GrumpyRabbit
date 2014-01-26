namespace GrumpyRabbit.Utilities
{
    public class Hitbox
    {
        public const byte LEFT_COLLISION_MASK = 1;
        public const byte TOP_COLLISION_MASK = 2;
        public const byte RIGHT_COLLISION_MASK = 4;
        public const byte BOTTOM_COLLISION_MASK = 8;

        private int left;
        private int top;
        private int right;
        private int bottom;

        public Hitbox(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public byte GetCollisionMaskWith(Hitbox other)
        {
            byte mask = 0;

            /* Check bottom collision */
            if (top < other.bottom && bottom > other.bottom)
            {
                mask++;
            }

            mask <<= 1;

            /* Check right collision */
            if (left < other.right && right > other.right)
            {
                mask++;
            }

            mask <<= 1;

            /* Check top collision */
            if (top < other.top && bottom > other.top)
            {
                mask++;
            }

            mask <<= 1;

            /* Check left collision */
            if (left < other.left && right > other.left)
            {
                mask++;
            }

            mask <<= 1;

            return mask;
        }

        public void Translate(int x, int y)
        {
            left += x;
            top += y;
            right += x;
            bottom += y;
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
    }
}

