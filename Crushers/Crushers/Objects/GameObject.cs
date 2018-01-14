using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crushers.Objects
{
    public class GameObject : asd.TextureObject2D
    {
        public const int Width = 25;
        public const int Height = 25;
        public asd.Vector2DI Size { get; } = new asd.Vector2DI(Width, Height);

        public asd.Vector2DF Acceleration { get; set; } = new asd.Vector2DF(0, 0);
        public asd.Vector2DF Velocity { get; set; } = new asd.Vector2DF(0, 0);

        public void SetForce(asd.Vector2DF dir, float power)
        {
            Acceleration = dir.Normal * power;
        }
        public void SetVelocityX(float x)
        {
            var vel = Velocity;
            vel.X = x;
            Velocity = vel;
        }
        public void SetVelocityY(float y)
        {
            var vel = Velocity;
            vel.Y = y;
            Velocity = vel;
        }

        public void AddForce()
        {
            Velocity += Acceleration;
        }
        public void Gravity()
        {
            Velocity += new asd.Vector2DF(0, 0.5f);
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
                    StartingPosition = Position - Size.To2DF(),
                    EndingPosition = new asd.Vector2DF(Position.X + Width, Position.Y - Height)
                },
                BottomSide = new asd.LineShape
                {
                    StartingPosition = new asd.Vector2DF(Position.X - Width, Position.Y + Height),
                    EndingPosition = Position + Size.To2DF()
                },
                LeftSide = new asd.LineShape(),
                RightSide = new asd.LineShape(),
                T_y = (int)Position.Y - Height,
                B_y = (int)Position.Y + Height,
                L_x = (int)Position.X - Width,
                R_x = (int)Position.X + Width
            };
            Area.LeftSide.StartingPosition = Area.TopSide.StartingPosition;            
            Area.LeftSide.EndingPosition = Area.BottomSide.StartingPosition;
            Area.RightSide.StartingPosition = Area.TopSide.EndingPosition;
            Area.RightSide.EndingPosition = Area.BottomSide.EndingPosition;
        }    

        // GameObject同士の衝突を想定
        public bool Inside(asd.Vector2DF point)
        {
            if (point.X > this.Position.X - Width && point.X < this.Position.X + Width
                && point.Y > this.Position.Y - Height && point.Y < this.Position.Y + Height)
            {
                return true;
            }

            return false;
        }
    }
}
