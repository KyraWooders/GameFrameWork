using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    class Game
    {
        public static readonly int SizeX = 16;
        public static readonly int SizeY = 16;

        //Whether or not the Game should finish Running and Exit
        public static bool Gameover = false;

        //the scene we are currently 
        private static Scene _currentScene;

        //creates a game and new scene instance as instance as its active scene
        public Game()
        {
            RL.InitWindow(640, 480, "Hello World");
            RL.SetTargetFPS(15);
        }

        
        private void Init()
        {
            Room startingRoom = new Room();
            Room northRoom = new Room();
            Room eastRoom = new Room();
            Room southRoom = new Room();

            //create an enemy
            Enemy enemy = new Enemy("Fumikage_Full_Body_Hero_Costume.png");

            void StartNorthRoom()
            {
                enemy.X = 4;
                enemy.Y = 4;
            }

            northRoom.OnStart += StartNorthRoom;

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

            //create a player and position it
            Player player = new Player("untitled.png");
            player.X = 4;
            player.Y = 3;

            //add the player to the starting room
            startingRoom.AddEntity(player);

            //add enemy to the northroom
            northRoom.AddEntity(enemy);

            CurrentScene = startingRoom;
        }
        
        public void Run()
        {
            //bind Ecs to exit the game (no longre needed)
            //PlayerInput.AddKeyEvent(Quit, ConsoleKey.Escape);

            Init();
            
            //loop til the game is over
            while (!Gameover && !RL.WindowShouldClose())
            {
                _currentScene.Update();

                RL.BeginDrawing();
                _currentScene.Draw();
                RL.EndDrawing();

                PlayerInput.ReadKey();
            }

            RL.CloseWindow();
        }

        //the scene we are currently running and start the first scene
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
