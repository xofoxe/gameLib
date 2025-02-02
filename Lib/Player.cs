namespace ClassLibrary1
{
    public class Player : Mob
    {
        private string username;
        private int killCount;
        public Player()
        {
            Health = 100;
            X = 2;
            Y = 2;
            Angle = 0;
            Dead = false;
            Alive = true;
            Radius = 0;
        }
        public string Username
        {
            set { username = value; }
            get => username;
        }
        public int KillCount
        {
            get => killCount; set => killCount = value;
        }
    }
    public class Mover
    {
        public void MoveForward(Mob mob) => Move(mob, Direction.Forward);
        public void MoveBackward(Mob mob) => Move(mob, Direction.Backward);
        public void MoveLeft(Mob mob) => Move(mob, Direction.Left);
        public void MoveRight(Mob mob) => Move(mob, Direction.Right);

        private void Move(Mob mob, Direction direction)
        {
            float directionModifier = GetDirectionModifier(direction);
            float angleOffset = GetAngleOffset(direction);
            float speed = GetSpeed(mob, direction);

            float dirX = (float)Math.Cos(mob.Angle + angleOffset) * speed;
            float dirY = (float)Math.Sin(mob.Angle + angleOffset) * speed;

            TryMove(mob, dirX * directionModifier, dirY * directionModifier);
        }

        private float GetDirectionModifier(Direction direction)
        {
            return (direction == Direction.Forward || direction == Direction.Right) ? 1 : -1;
        }

        private float GetAngleOffset(Direction direction)
        {
            return (direction == Direction.Left || direction == Direction.Right) ? (float)(Math.PI / 2f) : 0f;
        }

        private float GetSpeed(Mob mob, Direction direction)
        {
            return (direction == Direction.Forward || direction == Direction.Backward) ? mob.SpeedX : mob.SpeedY;
        }

        private void TryMove(Mob player, float dirX, float dirY)
        {
            int tryX = (int)(player.X + dirX + player.Radius);
            int tryY = (int)(player.Y + dirY + player.Radius);

            if (IsMoveValid(tryX, tryY))
            {
                player.X += dirX;
                player.Y += dirY;
            }
        }
        private bool IsMoveValid(int x, int y)
        {
            return Map.ColapseFunc(x, y);
        }
        public enum Direction
        {
            Forward,
            Backward,
            Left,
            Right
        }
    }
}