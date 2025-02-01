using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Mob
    {
        private float x;
        private float y;
        private float dx;
        private float dy;
        private float angle;
        private float relAngle;
        private float distance;
        private float moveCount;
        private int health;
        private bool visible;
        private float radius;
        private int spriteHeight;
        private int spriteWith;
        private int drawStartY;
        private int drawStartX;
        private int frame;
        private int damage;
        private int spritePos;
        private float spriteSize;
        private int frameCount;
        private bool dead;
        private bool alive;
        private float speedY;
        private float speedX;
        public float SpeedX
        {
            set
            {
                if (value > 0.3)
                {
                    speedX = 0.3f;
                }
                else if (value < 0)
                {
                    speedX = 0;
                }
                else
                    speedX = value;
            }
            get => speedX;
        }

        public float SpeedY
        {
            set
            {
                if (value > 0.3)
                {
                    speedY = 0.3f;
                }
                else if (value < 0)
                {
                    speedY = 0;
                }
                else
                    speedY = value;
            }
            get => speedY;
        }
        public bool Dead
        {
            get => dead;
            set { dead = value; }
        }
        public bool Alive
        {
            get => alive;
            set { alive = value; }
        }
        public int FrameCount
        {
            set { frameCount = value; }
            get => frameCount;
        }
        public float SpriteSize
        {
            set { spriteSize = value; }
            get => spriteSize;
        }
        public int SpritePos
        {
            set { spritePos = value; }
            get => spritePos;
        }
        public float Radius
        {
            set { radius = value; }
            get => radius;
        }

        public int Damage
        {
            get => damage;
            set => damage = value;
        }
        public int Frame
        {
            get => frame;
            set => frame = value;
        }
        public bool Visible
        {
            get => visible;
            set => visible = value;
        }

        public int SpriteHeight
        {
            get => spriteHeight;
            set => spriteHeight = value;
        }
        public int SpriteWith
        {
            get => spriteWith;
            set => spriteWith = value;
        }
        public int DrawStartY
        {
            get => drawStartY;
            set => drawStartY = value;
        }
        public int DrawStartX
        {
            get => drawStartX;
            set => drawStartX = value;
        }


        public int Health
        {
            get => health;
            set
            {
                if (value <= 0)
                    health = 0;
                else if (value >= 100) health = 100;
                else
                    health = value;
            }
        }
        public float MoveCount
        {
            get => moveCount;
            set => moveCount = value;
        }
        private Bitmap sprite = new Bitmap("..\\..\\Resources\\ordinaryEnemy\\front\\idle\\0.png");
        public Bitmap Sprite
        {
            set { sprite = value; }
            get => sprite;
        }

        public float Distance
        {
            set { distance = value; }
            get => distance;
        }

        public float X
        {
            get => x;
            set { x = value; }
        }
        public float Y
        {
            get => y;
            set { y = value; }
        }
        public float dX
        {
            get => dx;
            set { dx = value; }
        }
        public float dY
        {
            get => dy;
            set { dy = value; }
        }
        public float Angle
        {
            get => angle;
            set { angle = value; }
        }
        public float RelAngle
        {
            get => relAngle;
            set { relAngle = value; }
        }
    }
}
