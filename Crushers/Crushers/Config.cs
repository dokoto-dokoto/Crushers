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
            public const int Height = 600;
            public static asd.Vector2DI Size { get; } = new asd.Vector2DI(Width, Height);

            public static string Title { get; } = "Crushers";
        }

        public static class GameWindow
        {
            public const int Width = 400;
            public const int Height = 600;
            public static asd.Vector2DI Size { get; } = new asd.Vector2DI(Width, Height);
            public const int X = 200;
            public const int Y = 0;
            public static asd.Vector2DI Dest { get; } = new asd.Vector2DI(X, Y);
        }

        public static class UIWindow1
        {
            public const int Width = 200;
            public const int Height = 600;
            public static asd.Vector2DI Size { get; } = new asd.Vector2DI(Width, Height);
        }

        public static class UIWindow2
        {
            public const int Width = 200;
            public const int Height = 300;
            public static asd.Vector2DI Size { get; } = new asd.Vector2DI(Width, Height);
        }

        public static class PlayerConfig
        {
            public const int Width = 24;
            public const int Height = 24;
            public static asd.Vector2DF Size { get; } = new asd.Vector2DF(Width, Height);
        }

        public static class BlockConfig
        {
            public const int Width = 25;
            public const int Height = 25;
            public static asd.Vector2DF Size { get; } = new asd.Vector2DF(Width, Height);
        }

        public static bool isQuit = false;
    }
}
