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

        private void Init()
        {
            Room startingRoom = new Room();
            Room northRoom = new Room();
            Room eastRoom = new Room();
            Room southRoom = new Room();

            startingRoom.North = northRoom;
            startingRoom.East = eastRoom;
            startingRoom.South = southRoom;

            //walls for starting room
            for (int i = 1; i < startingRoom.SizeX - 1; i++)
            {
                startingRoom.AddEntity(new Wall(0, i));
                if (i != 2)
                {
                    startingRoom.AddEntity(new Wall(11, i));
                }
            }
            for (int i = 0; i < startingRoom.SizeX; i++)
            {
                if (i != 2)
                {
                    startingRoom.AddEntity(new Wall(i, 0));
                    startingRoom.AddEntity(new Wall(i, 5));
                }
            }

            //walls for northroom
            for (int i = 0; i < northRoom.SizeX; i++)
            {
                if (i != 2)
                {
                    northRoom.AddEntity(new Wall(i, 5));
                }
                northRoom.AddEntity(new Wall(i, 0));
            }
            for (int i = 1; i < northRoom.SizeX - 1; i++)
            {
                northRoom.AddEntity(new Wall(0, i));
                northRoom.AddEntity(new Wall(11, i));
            }

            //walls for south room
            for (int i = 1; i < southRoom.SizeX - 1; i++)
            {
                southRoom.AddEntity(new Wall(0, i));
                southRoom.AddEntity(new Wall(11, i));
            }
            for (int i = 0; i < southRoom.SizeX; i++)
            {
                if (i != 2)
                {
                    southRoom.AddEntity(new Wall(i, 0));
                }
                    southRoom.AddEntity(new Wall(i, 5));
            }

            //walls for east room
            for (int i = 1; i < eastRoom.SizeX - 1; i++)
            {
                if (i != 2)
                {
                    eastRoom.AddEntity(new Wall(0, i));
                }
                eastRoom.AddEntity(new Wall(11, i));
            }
            for (int i = 0; i < eastRoom.SizeX; i++)
            {
                eastRoom.AddEntity(new Wall(i, 0));
                eastRoom.AddEntity(new Wall(i, 5));
            }

            Player player = new Player();
            player.X = 4;
            player.Y = 3;
            Enemy enemy = new Enemy();
            enemy.X = 4;
            enemy.Y = 4;

            startingRoom.AddEntity(player);
            northRoom.AddEntity(enemy);

            CurrentScene = startingRoom;
        }
        
        public void Run()
        {
            PlayerInput.AddKeyEvent(Quit, ConsoleKey.Escape);

            Init();
            
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
                _currentScene.Start();
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
