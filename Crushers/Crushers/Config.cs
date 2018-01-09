using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crushers
{
    static class Config
    {
        public static class Window
        {
            public const int Width = 800;
            public const int Height = 800;
            public static asd.Vector2DI Size { get; } = new asd.Vector2DI(Width, Height);

            public static string Title { get; } = "Crushers";
        }
        public static bool isQuit = false;
    }
}
