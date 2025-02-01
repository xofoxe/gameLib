using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibrary1
{
    public class NPC : Mob
    {
        private int tick;
        private float accuracy;
        private bool pain;
        private bool drop;
        private bool isReloading;
        private string[] pngFiles;
        private int frameCount;
        public state CurrentState;
        private string path = "..\\..\\Resources\\ordinaryEnemy\\";
        private Animation animation;
        private Random rnd;
        public NPC(Animation animation)
        {
            rnd = new Random();
            Animation = animation;
        }
        public enum state
        {
            attack,
            walk,
            idle,
            chase,
            pain,
            death
        }
        public void GetSprite(Mob mob)
        {
            float n = (float)Math.Atan2(Y - mob.Y, X - mob.X);
            float angle = n - RelAngle;
            string direction;

            angle = (float)((angle + Math.PI) % (2 * Math.PI) - Math.PI);

            if (angle < -Math.PI)
            {
                angle += 2 * (float)Math.PI;
            }
             
            if (angle >= -Math.PI / 8 && angle < Math.PI / 8)
            {
                direction = "Back";
            }
            else if (angle >= Math.PI / 8 && angle < 3 * Math.PI / 8)
            {
                direction = "Back-left";
            }
            else if (angle >= 3 * Math.PI / 8 && angle < 5 * Math.PI / 8)
            {
                direction = "Left";
            }
            else if (angle >= 5 * Math.PI / 8 && angle < 7 * Math.PI / 8)
            {
                direction = "Front-left";
            }
            else if (angle >= -3 * Math.PI / 8 && angle < -Math.PI / 8)
            {
                direction = "Back-right";
            }
            else if (angle >= -5 * Math.PI / 8 && angle < -3 * Math.PI / 8)
            {
                direction = "Right";
            }
            else if (angle >= -7 * Math.PI / 8 && angle < -5 * Math.PI / 8)
            {
                direction = "Front-right";
            }
            else
            {
                direction = "Front";
            }
            switch (direction)
            {
                case "Front":
                    path = "..\\..\\Resources\\ordinaryEnemy\\front\\";
                    break;
                case "Back":
                    path = "..\\..\\Resources\\ordinaryEnemy\\back\\";
                    break;
                case "Front-right":
                    path = "..\\..\\Resources\\ordinaryEnemy\\front-right\\";
                    break;
                case "Front-left":
                    path = "..\\..\\Resources\\ordinaryEnemy\\front-left\\";
                    break;
                case "Back-right":
                    path = "..\\..\\Resources\\ordinaryEnemy\\back-right\\";
                    break;
                case "Back-left":
                    path = "..\\..\\Resources\\ordinaryEnemy\\back-left\\";
                    break;
                case "Right":
                    path = "..\\..\\Resources\\ordinaryEnemy\\right\\";
                    break;
                case "Left":
                    path = "..\\..\\Resources\\ordinaryEnemy\\left\\";
                    break;
            }
            switch (CurrentState)
            {
                case state.attack: path += "attack\\"; break;
                case state.idle: path += "idle\\"; break;
                case state.walk: path += "walk\\"; break;
                case state.death: path += "death\\"; break;
                case state.pain: path += "pain\\"; break;
            }
        }
        
        public void Attack(Game game)
        {
            if (Visible && !IsReloading)
            {
                IsReloading = true;
                game.Player.Health -= Damage;
            }
        }
         

        public void GetBehavior(int a)
        {
            switch (a)
            {
                case 0: CurrentState = state.attack; break;
                case 1: CurrentState = state.walk; break;
                case 2: CurrentState = state.idle; break;
            }
        }
        public void update(Game game)
        {
            Animation.checkAnimTime();
            if (!Dead)
            {
                GetSprite(game.Player);
                RunLogic(game);
            }
        }

        public void RunLogic(Game game)
        {
            if (Alive)
            {
                if (Animation.AnimationTrigger)
                {
                    tick = (tick + 1) % 10;
                    if (tick == 6 || CurrentState == state.attack || CurrentState == state.pain)
                    {
                        tick = 0;
                        int bih = rnd.Next(0, 2);
                        GetBehavior(bih);
                        MoveWay(game);
                    }
                }
                if (CurrentState != state.attack)
                {
                    IsReloading = false;
                }
                Animation.animate(this, path);
                switch (CurrentState)
                {
                    case state.attack: AnimateAttack(game); break;
                    case state.walk: MoveFront(); break;
                    case state.death: AnimateDeath(game); break;
                    case state.pain: AnimatePain(game); break;
                }
                if (pain)
                {
                    CurrentState = state.pain;
                }
                if (Health <= 0)
                {
                    CurrentState = state.death;
                    Animation.num = 0;
                }
            }
            else if (!Dead)
            {
                AnimateDeath(game);
            }
        }

        public void spawnBot()
        {
            rnd = new Random();
            CurrentState = state.walk;
            do
            {
                X = rnd.Next(25, 40);
                Y = rnd.Next(10, 15);
            }
            while (Map.map[(int)Y, (int)X] == '#');
        }
        public void AnimateAttack(Game game)
        {
            Animation.animate(this, path);
            Attack(game);
        }
        public void AnimatePain(Game game)
        {
            if (Animation.AnimationTrigger)
            {
                pain = false;
            }
        }
         
        public void AnimateDeath(Game game)
        {
            pngFiles = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
            frameCount = pngFiles.Length;
            if (Animation.AnimationTrigger && Frame < frameCount - 1)
            {
                Animation.animate(this, path);
                Frame++;
            }
            if (Frame == frameCount - 1)
            {
                Dead = true;
                path = path + $"{frameCount - 1}" + ".png";
                Sprite = new Bitmap(path);
            }

        }
        public void chase(Game game)
        {
            switch (CurrentState)
            {
                case state.attack: AnimateAttack(game); break;
                case state.idle: Animation.animate(this, path); break;
                case state.walk:
                    MoveFront();
                    Animation.animate(this, path);
                    break;
            }
        }
        public void MoveWay(Game game)
        {
            if (Animation.AnimationTrigger)
            {
                float n = (float)Math.Atan2(Y - game.Player.Y, X - game.Player.X);
                float angle = n - RelAngle;
                angle = (float)(angle + Math.PI) % (float)(2 * Math.PI) - (float)Math.PI;
                if (angle < -Math.PI)
                {
                    angle += 2 * (float)Math.PI;
                }
                switch (rnd.Next(0, 5))
                {
                    case 0: RelAngle = n - ((float)Math.PI / 4f + (float)Math.PI); break;
                    case 1: RelAngle = n + ((float)Math.PI / 4f + (float)Math.PI); break;
                    case 2: RelAngle = n + ((float)Math.PI); break;
                    case 3: RelAngle = n - ((float)Math.PI); break;
                    case 4: RelAngle = n - ((float)Math.PI / 2f + (float)Math.PI); break;
                    case 5: RelAngle = n + ((float)Math.PI / 2f + (float)Math.PI); break;

                }
            }
        }

        public void MoveFront()
        {
            float dirX = (float)Math.Cos(RelAngle) * (MoveCount);
            float dirY = (float)Math.Sin(RelAngle) * (MoveCount);
            int tryX = (int)(X + dirX + Radius);
            int tryY = (int)Y;
            if (tryY > 0 && tryY < Map.map.GetLength(0) && tryX > 0 && tryX < Map.map.GetLength(1))
                if (Map.map[tryY, tryX] != '#') X += dirX;
            tryX = (int)Math.Round(X);
            tryY = (int)Math.Round(Y + dirY + Radius);
            if (tryY > 0 && tryY < Map.map.GetLength(0) && tryX > 0 && tryX < Map.map.GetLength(1))
                if (Map.map[tryY, tryX] != '#') Y += dirY;
        }

        public bool check_hit(Game game)
        {
            if (Visible)
            {
                if ((game.CanvasSize.Width / 2 - SpriteWith / 2) < (DrawStartX + SpriteWith / 2) && (DrawStartX + SpriteWith / 2) < (game.CanvasSize.Width / 2 + SpriteWith / 2))
                {                    
                    CurrentState = state.pain;
                    return true;
                }
            }
            return false;
        }
         


        public float Acсuracy
        {
            get => accuracy;
            set { accuracy = value; }
        }
        public bool Drop
        {
            get => drop;
            set { drop = value; }
        }
        public bool IsReloading
        {
            set { isReloading = value; }
            get => isReloading;
        }
        public int Tick
        {
            get => tick;
            set { tick = value; }
        }
        public bool Pain
        {
            get => pain;
            set { pain = value; }
        }

        public Animation Animation
        {
            get => animation;
            set { animation = value; }
        }
    }
}
