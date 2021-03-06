﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crushers.Objects
{
    class Player : GameObject
    {
        public string NAME { get; set; }
        public bool faceToR;
        public bool isGround = false;

        public const int MaxSpeed = 2;

        public Player()
        {
            Width = Config.PlayerConfig.Width;
            Height = Config.PlayerConfig.Height;
            Size = Config.PlayerConfig.Size;

            Texture = asd.Engine.Graphics.CreateTexture2D("baby_boy01_smile.png");
            int x = Texture.Size.X;
            int y = Texture.Size.Y;
            Scale = new asd.Vector2DF((float)Width / x, (float)Height / y);
        }

        protected override void OnUpdate()
        {
            var vel = Velocity;
            vel.X = asd.MathHelper.Clamp(vel.X, 2, -2);
            vel.Y = asd.MathHelper.Clamp(vel.Y, 2, -20);
            Velocity = vel;
        }
    }
}
