using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crushers.Objects
{
    public class GameObject : asd.TextureObject2D
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public asd.Vector2DF Size { get; set; }

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

        // GameObject同士の衝突時処理
        public asd.Vector2DF CollideUpdate(GameObject gameObj)
        {
            // 衝突する範囲
            var colArea_U = gameObj.Position.Y - this.Height;
            var colArea_L = gameObj.Position.X - this.Width;
            var colArea_R = gameObj.Position.X + gameObj.Width;
            var colArea_D = gameObj.Position.Y + gameObj.Height;

            // 次フレームのオブジェクトの位置
            var nextPos = this.Position + this.Velocity;

            // 衝突処理
            // 上辺との衝突処理
            if (this.Position.Y <= colArea_U && colArea_U < nextPos.Y)
            {
                var t = (colArea_U - this.Position.Y) / (nextPos.Y - this.Position.Y);
                var i_x = this.Position.X + t * (nextPos.X - this.Position.X);
                if (colArea_L <= i_x && i_x <= colArea_R)
                {
                    nextPos.Y = colArea_U;
                    SetVelocityY(0);
                    return nextPos;
                }
            }
            // 左辺との衝突処理
            if (this.Position.X <= colArea_L && colArea_L <= nextPos.X)
            {
                var t = (colArea_L - this.Position.X) / (nextPos.X - this.Position.X);
                var i_y = this.Position.Y + t * (nextPos.Y - this.Position.Y);
                if (colArea_U <= i_y && i_y <= colArea_D)
                {
                    nextPos.X = colArea_L;
                    SetVelocityX(0);
                    return nextPos;
                }
            }
            // 右辺との衝突処理
            if (this.Position.X >= colArea_R && colArea_R >= nextPos.X)
            {
                var t = (colArea_R - this.Position.X) / (nextPos.X - this.Position.X);
                var i_y = this.Position.Y + t * (nextPos.Y - this.Position.Y);
                if (colArea_U <= i_y && i_y <= colArea_D)
                {
                    nextPos.X = colArea_R;
                    SetVelocityX(0);
                    return nextPos;
                }
            }
            // 下辺との衝突処理
            if (this.Position.Y >= colArea_D && colArea_D >= nextPos.Y)
            {
                var t = (colArea_D - this.Position.Y) / (nextPos.Y - this.Position.Y);
                var i_x = this.Position.X + t * (nextPos.X - this.Position.X);
                if (colArea_L <= i_x && i_x <= colArea_R)
                {
                    nextPos.Y = colArea_D;
                    SetVelocityY(0);
                }
            }
            return nextPos;
        }
    }
}
