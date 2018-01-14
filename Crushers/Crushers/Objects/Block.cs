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
            }
            Texture = asd.Engine.Graphics.CreateTexture2D(texturePath);
            int x = Texture.Size.X;
            int y = Texture.Size.Y;
            Scale = new asd.Vector2DF((float)Width / x, (float)Height / y);
        }

        protected override void OnAdded()
        {
            base.OnAdded();
        }

        protected override void OnUpdate()
        {

        }
    }
}
