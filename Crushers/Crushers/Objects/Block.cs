using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crushers.Objects
{
    class Block : GameObject
    {
        int dur;
        public Block(int ID)
        {
            Width = Config.BlockConfig.Width;
            Height = Config.BlockConfig.Height;
            Size = Config.BlockConfig.Size;

            dur = ID;
            string texturePath = "";
            switch (ID)
            {
                case 0:
                    texturePath = "block_200x200.png";
                    break;
                case 1:
                    texturePath = "block_200x200_blue.png";
                    break;
                case 2:
                    texturePath = "block_200x200_green.png";
                    break;
                case 3:
                    texturePath = "block_200x200_yellow.png";
                    break;
                default:
                    break;
            }
            Texture = asd.Engine.Graphics.CreateTexture2D(texturePath);
            Scale = new asd.Vector2DF((float)Width / Texture.Size.X, (float)Height / Texture.Size.Y);
        }

        public struct AreaRect
        {
            public asd.LineShape TopSide;
            public int T_y;
            public asd.LineShape BottomSide;
            public int B_y;
            public asd.LineShape LeftSide;
            public int L_x;
            public asd.LineShape RightSide;
            public int R_x;
        }
        public AreaRect Area { get; set; }

        protected override void OnAdded()
        {
            Area = new AreaRect
            {
                TopSide = new asd.LineShape
                {
                    StartingPosition = Position - Config.PlayerConfig.Size,
                    EndingPosition = new asd.Vector2DF(Position.X + Width, Position.Y - Config.PlayerConfig.Height)
                },
                BottomSide = new asd.LineShape
                {
                    StartingPosition = new asd.Vector2DF(Position.X - Config.PlayerConfig.Width, Position.Y + Height),
                    EndingPosition = Position + Size
                },
                LeftSide = new asd.LineShape(),
                RightSide = new asd.LineShape(),
                T_y = (int)Position.Y - Config.PlayerConfig.Height,
                B_y = (int)Position.Y + Height,
                L_x = (int)Position.X - Config.PlayerConfig.Width,
                R_x = (int)Position.X + Width
            };
            Area.LeftSide.StartingPosition = Area.TopSide.StartingPosition;
            Area.LeftSide.EndingPosition = Area.BottomSide.StartingPosition;
            Area.RightSide.StartingPosition = Area.TopSide.EndingPosition;
            Area.RightSide.EndingPosition = Area.BottomSide.EndingPosition;
        }

        protected override void OnUpdate()
        {

        }
    }
}
