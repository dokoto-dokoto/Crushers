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
        private List<Objects.Block> blockList;

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

            blockList = new List<Objects.Block>();
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var b = new Objects.Block(i / 4) { Position = new asd.Vector2DF(600 - 25 * i, 400 + 25 * j) };
                    blockList.Add(b);
                    layer.AddObject(b);
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

        protected override void OnUpdated()
        {
            var key_L = asd.Engine.Keyboard.GetKeyState(asd.Keys.Left);
            var key_R = asd.Engine.Keyboard.GetKeyState(asd.Keys.Right);
            var key_U = asd.Engine.Keyboard.GetKeyState(asd.Keys.Up);
            var key_D = asd.Engine.Keyboard.GetKeyState(asd.Keys.Down);
            var key_Z = asd.Engine.Keyboard.GetKeyState(asd.Keys.Z);
            var key_Sp = asd.Engine.Keyboard.GetKeyState(asd.Keys.Space);

            // 仮座標
            var playerPos = player.Position;

            // 左右移動
            if (key_L == asd.KeyState.Hold)
            {
                player.SetVelocityX(-2);
            }
            if (key_R == asd.KeyState.Hold)
            {
                player.SetVelocityX(2);
            }

            // ジャンプ
            if (key_Sp == asd.KeyState.Push)
            {
                player.SetVelocityY(-7.5f);
            }

            player.Gravity();
            playerPos += player.Velocity;

            // 掘削
            if (key_Z == asd.KeyState.Push)
            {
                foreach (var obj in blockList.Where(o=>(player.Position - o.Position).SquaredLength < Config.BlockConfig.Size.SquaredLength))
                {

                }
            }

            // 当たり判定
            foreach (var obj in blockList.Where(o => (player.Position - o.Position).SquaredLength < Config.BlockConfig.Size.SquaredLength))
            {
                playerPos = player.CollideUpdate(obj);
            }
            player.Position = playerPos;
        }
    }
}
