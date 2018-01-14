using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crushers.Scenes
{
    class GameScene : asd.Scene
    {
        private asd.Layer2D layer;
        private asd.Layer2D bgLayer;
        private asd.Layer2D uiLayer;

        private asd.CameraObject2D camera;

        public Objects.Player player;

        private const int offset = 15;

        public GameScene()
        {
            bgLayer = new asd.Layer2D();
            layer = new asd.Layer2D();
            uiLayer = new asd.Layer2D();

            camera = new asd.CameraObject2D
            {
                Src = new asd.RectI(200, 0, 400, 600),
                Dst = new asd.RectI(200, 0, 400, 600)
            };
            player = new Objects.Player();
        }

        protected override void OnRegistered()
        {
            AddLayer(bgLayer);
            AddLayer(layer);
            AddLayer(uiLayer);

            layer.AddObject(camera);

            player.Position = new asd.Vector2DF(250, 375);
            layer.AddObject(player);

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    layer.AddObject(new Objects.Block(i / 4) { Position = new asd.Vector2DF(200 + 25 * i, 400 + 25 * j) });
                }
            }

            var ui_p1 = new asd.TextureObject2D()
            {
                Texture = asd.Engine.Graphics.CreateTexture2D("ui_window.png"),
                Position = new asd.Vector2DF(0, 0)
            };
            ui_p1.Scale = new asd.Vector2DF((float)Config.UIWindow1.Width / ui_p1.Texture.Size.X, (float)Config.UIWindow1.Height / ui_p1.Texture.Size.Y);
            uiLayer.AddObject(ui_p1);

            var ui_p2 = new asd.TextureObject2D()
            {
                Texture = asd.Engine.Graphics.CreateTexture2D("ui_window.png"),
                Position = new asd.Vector2DF(600, 0)
            };
            ui_p2.Scale = new asd.Vector2DF((float)Config.UIWindow1.Width / ui_p2.Texture.Size.X, (float)Config.UIWindow1.Height / ui_p2.Texture.Size.Y);
            uiLayer.AddObject(ui_p2);
        }

        /// <summary>
        /// if the line from p0 to p1 intersects block's Area, fix the Position p1 with the intersection.
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        private asd.Vector2DF FixedPosition(asd.Vector2DF p0, asd.Vector2DF p1, Objects.Block block)
        {
            var p = p1;
            // With Top
            if (p0.Y <= block.Area.T_y && block.Area.T_y < p1.Y)
            {
                float t = (block.Area.T_y - p0.Y) / (p1.Y - p0.Y);
                float i_x = p0.X + t * (p1.X - p0.X);
                if (block.Area.TopSide.StartingPosition.X < i_x && i_x < block.Area.TopSide.EndingPosition.X)
                {
                    p.X = i_x;
                    p.Y = block.Area.T_y;
                    player.isGround = true;
                    player.SetVelocityY(0);
                    return p;
                }
            }

            // With Bottom
            if (p0.Y >= block.Area.B_y && block.Area.B_y > p1.Y)
            {
                float t = (block.Area.B_y - p0.Y) / (p1.Y - p0.Y);
                float i_x = p0.X + t * (p1.X - p0.X);
                if (block.Area.BottomSide.StartingPosition.X < i_x && i_x < block.Area.BottomSide.EndingPosition.X)
                {
                    p.X = i_x;
                    p.Y = block.Area.B_y;
                    player.SetVelocityY(0);
                    return p;
                }
            }

            // With Left
            if (p0.X <= block.Area.L_x && block.Area.L_x < p1.X)
            {
                float t = (block.Area.L_x - p0.X) / (p1.X - p0.X);
                float i_y = p1.Y + t * (p1.Y - p0.Y);
                if (block.Area.LeftSide.StartingPosition.Y < i_y && i_y < block.Area.LeftSide.EndingPosition.Y)
                {
                    p.X = block.Area.L_x;
                    p.Y = i_y;
                    player.SetVelocityX(0);
                    return p;
                }
            }

            // With Right
            if (p0.X >= block.Area.R_x && block.Area.R_x > p1.X)
            {
                float t = (block.Area.R_x - p0.X) / (p1.X - p0.X);
                float i_y = p1.Y + t * (p1.Y - p0.Y);
                if (block.Area.RightSide.StartingPosition.Y < i_y && i_y < block.Area.RightSide.EndingPosition.Y)
                {
                    p.X = block.Area.R_x;
                    p.Y = i_y;
                    player.SetVelocityX(0);
                    return p;
                }
            }

            return p;
        }

        protected override void OnUpdated()
        {
            var playerPos = player.Position + player.Velocity;
            var key_L = asd.Engine.Keyboard.GetKeyState(asd.Keys.Left);
            var key_R = asd.Engine.Keyboard.GetKeyState(asd.Keys.Right);
            var key_U = asd.Engine.Keyboard.GetKeyState(asd.Keys.Up);
            var key_D = asd.Engine.Keyboard.GetKeyState(asd.Keys.Down);
            var key_Z = asd.Engine.Keyboard.GetKeyState(asd.Keys.Z);
            var key_Sp = asd.Engine.Keyboard.GetKeyState(asd.Keys.Space);

            if (key_L == asd.KeyState.Hold)
            {
                playerPos += new asd.Vector2DF(-2, 0);
                if (key_Z == asd.KeyState.Push)
                {
                    foreach (var obj in layer.Objects.Where(
                        o => o is Objects.Block
                        && player.Position.X - offset > o.Position.X
                        && (player.Position - o.Position).SquaredLength < 1.1f * Objects.GameObject.Width * Objects.GameObject.Width))
                    {
                        obj.Dispose();
                    }
                }
            }

            if (key_R == asd.KeyState.Hold)
            {
                playerPos += new asd.Vector2DF(2, 0);
                if (key_Z == asd.KeyState.Push)
                {
                    foreach (var obj in layer.Objects.Where(
                        o => o is Objects.Block
                        && player.Position.X + offset < o.Position.X
                        && (player.Position - o.Position).SquaredLength < 1.1f * Objects.GameObject.Width * Objects.GameObject.Width))
                    {
                        obj.Dispose();
                    }
                }
            }

            if (key_U == asd.KeyState.Hold)
            {
                playerPos += new asd.Vector2DF(0, -2);
                if (key_Z == asd.KeyState.Push)
                {
                    foreach (var obj in layer.Objects.Where(
                        o => o is Objects.Block
                        && player.Position.Y - offset > o.Position.Y
                        && (player.Position - o.Position).SquaredLength < 1.1f * Objects.GameObject.Height * Objects.GameObject.Height))
                    {
                        obj.Dispose();
                    }
                }
            }

            if (key_D == asd.KeyState.Hold)
            {
                playerPos += new asd.Vector2DF(0, 2);
                if (key_Z == asd.KeyState.Push)
                {
                    foreach (var obj in layer.Objects.Where(
                        o => o is Objects.Block
                        && player.Position.Y + offset < o.Position.Y
                        && (player.Position - o.Position).SquaredLength < 1.1f * Objects.GameObject.Height * Objects.GameObject.Height))
                    {
                        obj.Dispose();
                    }
                }
            }

            player.Gravity();
            playerPos += player.Velocity;

            if (playerPos.X < Config.GameWindow.X)
            {
                playerPos.X = Config.GameWindow.X;
            }
            else if (playerPos.X + Objects.GameObject.Width > Config.GameWindow.X + Config.GameWindow.Width)
            {
                playerPos.X = Config.GameWindow.X + Config.GameWindow.Width - Objects.GameObject.Width;
            }

            if (playerPos.Y + Objects.GameObject.Height > Config.GameWindow.Height)
            {
                playerPos.Y = Config.GameWindow.Height - Objects.GameObject.Height;
            }

            foreach (var obj in layer.Objects.Where(o => o is Objects.Block && (player.Position - o.Position).SquaredLength <= (player.Size.To2DF()).SquaredLength))
            {
                var o = obj as Objects.Block;
                playerPos = FixedPosition(player.Position, playerPos, o);
            }
            player.Position = playerPos;
        }
    }
}
