using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Game
    {
        private Wall wall;
        private Player player;
        private Size canvasSize;
        private bool startGame;
        public Game()
        {
            wall = new Wall();
            player = new Player();
            startGame = true;
        }
       
        public void NewGame()
        {
            if (startGame == true)
                return;
            if (startGame == false)
            {
                startGame = true;
            }
        }
        
        public void update()
        {
            if (player.Dead == true)
            {
                return;
            }
        }
        public Size CanvasSize
        {
            get { return canvasSize; }
            set { canvasSize = value; }
        }
        
        public Wall Wall
        {
            get => wall;
            set { wall = value; }
        }
        public Player Player => player;
        public bool StartGame
        {
            set { startGame = value; }
            get { return startGame; }
        }
    }
}
