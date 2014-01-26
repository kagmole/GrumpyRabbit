using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GrumpyRabbit
{
    /// <summary>
    /// WorldGenerator is a utility static class using the Win API interface
    /// of C# to get windows as boxes for the game. GrumpyRabbit is a game
    /// based on the desktop environnement of the user.
    /// </summary>
    public static class WorldGenerator
    {
        private const int RABBIT_BOX_POOL_SIZE = 100;

        /* http://msdn.microsoft.com/en-us/library/ms632611(v=vs.85).aspx */
        private const int SW_HIDE = 0;
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;
        private const int SW_SHOW = 5;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMINNOACTIVE = 7;
        private const int SW_SHOWNA = 8;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOWNORMAL = 1;

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/ms632611(v=vs.85).aspx */
        [StructLayout(LayoutKind.Sequential)]
        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/dd162805(v=vs.85).aspx */
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/dd162897(v=vs.85).aspx */
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/ms633498(v=vs.85).aspx */
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/ms633493(v=vs.85).aspx */
        private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/ms633497(v=vs.85).aspx  */
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/ms633494(v=vs.85).aspx */
        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(EnumChildProc lpEnumFunc, IntPtr lParam);

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/ms633518(v=vs.85).aspx */
        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpWndPl);

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/ms633530(v=vs.85).aspx */
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        /* http://msdn.microsoft.com/en-us/library/windows/desktop/ms633521(v=vs.85).aspx */
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        private static EnumWindowsProc getVisiblesWindowsAsSquaresHitboxes;

        private static List<SquareHitbox> squaresHitboxesList;

        private static ObjectPool<SquareHitbox> squaresHitboxesPool;

        private static WINDOWPLACEMENT wndPl;

        /// <summary>
        /// Static constructor. It ensures than this code will run before everything else.
        /// </summary>
        static WorldGenerator()
        {
            squaresHitboxesList = new List<SquareHitbox>();
            squaresHitboxesPool = new ObjectPool<SquareHitbox>(CreateSquareHitbox, RecycleSquareHitbox, RABBIT_BOX_POOL_SIZE);

            getVisiblesWindowsAsSquaresHitboxes = new EnumWindowsProc(GetVisiblesWindowsAsSquaresHitboxes_EnumWindowsProc);
        }

        /// <summary>
        /// Win API "EnumWindowsProc" implementation to enumerate all windows of the system.
        /// A lot of windows are alive even if we see nothing on our desktop. They are usually
        /// used to provide fancy graphics on the desktop.
        /// 
        /// To filter windows that the user really see, we use the Win API function "IsWindowVisible".
        /// However, this last function doesn't work as expected with Windows 7 : some invisibles
        /// windows are still enumerate. Then, we use a "trick" to only get what we want : in the
        /// actual state of the project, we ignore windows without text.
        /// </summary>
        /// <param name="hWnd">Window handler</param>
        /// <param name="lParam">LPARAM of the window</param>
        /// <returns>false if this is the last window, true otherwise</returns>
        private static bool GetVisiblesWindowsAsSquaresHitboxes_EnumWindowsProc(IntPtr hWnd, IntPtr lParam)
        {
            /* IntPtr.Zero must be used instead of 0, because something the "null" value of IntPtr
             * is not 0. */
            if (hWnd == IntPtr.Zero)
            {
                return false;
            }

            int textLength = GetWindowTextLength(hWnd);

            /* If this is a window with the "visible" flag at 1 and with text. */
            if (IsWindowVisible(hWnd) && textLength > 0)
            {
                wndPl.length = Marshal.SizeOf(wndPl);

                GetWindowPlacement(hWnd, ref wndPl);

                /* Ignore maximized and minimized windows */
                if (wndPl.showCmd == SW_SHOWNORMAL)
                {
                    squaresHitboxesList.Add(squaresHitboxesPool.SeekObject(
                        wndPl.rcNormalPosition.left,
                        wndPl.rcNormalPosition.top,
                        wndPl.rcNormalPosition.right,
                        wndPl.rcNormalPosition.bottom));
                }
            }

            return true;
        }

        /// <summary>
        /// The create method for the SquareHitbox ObjectPool. See ObjectPool for more details.
        /// </summary>
        /// <param name="args">Boxing arguments for the SquareHitbox</param>
        /// <returns>A SquareHitbox object</returns>
        /// <see cref="ObjectPool"/>
        private static SquareHitbox CreateSquareHitbox(params object[] args)
        {
            return new SquareHitbox((int)args[0], (int)args[1], (int)args[2], (int)args[3]);
        }

        /// <summary>
        /// The recycle method for the SquareHitbox ObjectPool. See ObjectPool for more details.
        /// </summary>
        /// <param name="recycledRabbitBox">A SquareHitbox object to recycle</param>
        /// <param name="args">Boxing arguments for the SquareHitbox</param>
        /// <see cref="ObjectPool"/>
        private static void RecycleSquareHitbox(SquareHitbox recycledRabbitBox, params object[] args)
        {
            recycledRabbitBox.Left = (int)args[0];
            recycledRabbitBox.Top = (int)args[1];
            recycledRabbitBox.Right = (int)args[2];
            recycledRabbitBox.Bottom = (int)args[3];
        }

        /// <summary>
        /// Method who manage the SquareHitbox ObjectPool and the call to the EnumWindowProc.
        /// </summary>
        /// <returns>A list of SquareHitbox representing desktop windows</returns>
        public static List<SquareHitbox> GetWorldSquaresHitboxesList()
        {
            foreach (SquareHitbox squareHitbox in squaresHitboxesList)
            {
                squaresHitboxesPool.FreeObject(squareHitbox);
            }
            squaresHitboxesList.Clear();

            EnumWindows(getVisiblesWindowsAsSquaresHitboxes, IntPtr.Zero);

            return squaresHitboxesList;
        }
    }
}