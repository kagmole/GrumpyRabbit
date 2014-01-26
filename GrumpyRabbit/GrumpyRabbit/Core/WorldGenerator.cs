using GrumpyRabbit.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GrumpyRabbit.Core
{
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

        private static List<Hitbox> squaresHitboxesList;

        private static ObjectPool<Hitbox> squaresHitboxesPool;

        private static WINDOWPLACEMENT wndPl;

        static WorldGenerator()
        {
            squaresHitboxesList = new List<Hitbox>();
            squaresHitboxesPool = new ObjectPool<Hitbox>(CreateSquareHitbox, RecycleSquareHitbox, RABBIT_BOX_POOL_SIZE);

            getVisiblesWindowsAsSquaresHitboxes = new EnumWindowsProc(GetVisiblesWindowsAsSquaresHitboxes_EnumWindowsProc);
        }

        private static bool GetVisiblesWindowsAsSquaresHitboxes_EnumWindowsProc(IntPtr hWnd, IntPtr lParam)
        {
            if (hWnd == IntPtr.Zero)
            {
                return false;
            }

            int textLength = GetWindowTextLength(hWnd);

            if (IsWindowVisible(hWnd) && textLength > 0)
            {
                wndPl.length = Marshal.SizeOf(wndPl);

                GetWindowPlacement(hWnd, ref wndPl);

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

        private static Hitbox CreateSquareHitbox(params object[] args)
        {
            return new Hitbox((int)args[0], (int)args[1], (int)args[2], (int)args[3]);
        }

        private static void RecycleSquareHitbox(Hitbox recycledRabbitBox, params object[] args)
        {
            recycledRabbitBox.Left = (int)args[0];
            recycledRabbitBox.Top = (int)args[1];
            recycledRabbitBox.Right = (int)args[2];
            recycledRabbitBox.Bottom = (int)args[3];
        }

        public static List<Hitbox> GetWorldSquaresHitboxesList()
        {
            foreach (Hitbox squareHitbox in squaresHitboxesList)
            {
                squaresHitboxesPool.FreeObject(squareHitbox);
            }
            squaresHitboxesList.Clear();

            EnumWindows(getVisiblesWindowsAsSquaresHitboxes, IntPtr.Zero);

            return squaresHitboxesList;
        }
    }
}