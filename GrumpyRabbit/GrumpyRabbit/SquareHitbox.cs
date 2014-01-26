using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrumpyRabbit
{
    /// <summary>
    /// Provides a light box system.
    /// </summary>
    public class SquareHitbox
    {
        private int left;
        private int top;
        private int right;
        private int bottom;

        /// <summary>
        /// Main constructor. All coordinates are related to the top-left corner of
        /// the main screen.
        /// </summary>
        /// <param name="left">X coordinates</param>
        /// <param name="top">Y coordinates</param>
        /// <param name="right">X coordinates + width</param>
        /// <param name="bottom">Y coordinates + Height</param>
        public SquareHitbox(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
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

