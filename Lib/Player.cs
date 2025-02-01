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

        private void TryMove(float dirX, float dirY)
        {
            int tryX = (int)Math.Round(X + dirX + Radius);
            int tryY = (int)Math.Round(Y + dirY + Radius);

            if (tryY > 0 && tryY < Map.map.GetLength(0) && tryX > 0 && tryX < Map.map.GetLength(1) &&
                Map.map[tryY, tryX] != '#')
            {
                X += dirX;
                Y += dirY;
            }
        }
        public void MoveForward()
        {
            float dirX = (float)Math.Cos(Angle) * SpeedX;
            float dirY = (float)Math.Sin(Angle) * SpeedX;
            TryMove(dirX, dirY);
        }

        public void MoveBackward()
        {
            float dirX = (float)Math.Cos(Angle) * SpeedX;
            float dirY = (float)Math.Sin(Angle) * SpeedX;
            TryMove(-dirX, -dirY);
        }

        public void MoveLeft()
        {
            float rdirX = (float)Math.Cos(Angle + Math.PI / 2f) * SpeedY;
            float rdirY = (float)Math.Sin(Angle + Math.PI / 2f) * SpeedY;
            TryMove(-rdirX, -rdirY);
        }

        public void MoveRight()
        {
            float rdirX = (float)Math.Cos(Angle + Math.PI / 2f) * SpeedY;
            float rdirY = (float)Math.Sin(Angle + Math.PI / 2f) * SpeedY;
            TryMove(rdirX, rdirY);
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
}
