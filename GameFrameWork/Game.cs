using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Game
    {
        //Whether or not the Game should finish Running and Exit
        public static bool Gameover = false;
        //the scene we are currently 
        private static Scene _currentScene;
        //creates a gmae and new scene instance as i
        public Game()
        {
            
        }
        
        public void Run()
        {
            Room startingRoom = new Room();
            Room northRoom = new Room();
            startingRoom.North = northRoom;
            northRoom.South = startingRoom;

            _currentScene.AddEntity(new Wall(1, 3));

            Player player = new Player();
            player.X = 4;
            player.Y = 3;
            Entity enemy = new Entity('*');
            enemy.X = 4;
            enemy.Y = 5;

            _currentScene.AddEntity(player);
            _currentScene.AddEntity(enemy);

            PlayerInput.AddKeyEvent(Quit, ConsoleKey.Escape);

            _currentScene.Start();

            //loop til the game is over
            while (!Gameover)
            {
                _currentScene.Update();
                _currentScene.Draw();
                PlayerInput.ReadKey();
            }

        }
        //the scene we are currently running
        public static Scene CurrentScene
        {
            set
            {
                _currentScene = value;
            }
            get
            {
                return _currentScene;
            }
        }

        public void Quit()
        {
            Gameover = true;
        }
    }
}
