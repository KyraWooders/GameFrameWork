﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;
using System.IO;

namespace GameFrameWork
{
    class Game
    {
        //sprite size
        public static readonly Vector2 UnitSize = new Vector2(22, 22);
        //public static readonly int UnitSizeX = 22;
        //public static readonly int UnitSizeY = 16;

        //Whether or not the Game should finish Running and Exit
        public static bool Gameover = false;
        //the scene we are currently 
        private static Scene _currentScene = null;
        //the scene we are about to go to
        private static Scene _nextScene = null;
        //The timer for the entire game 
        private Timer _gameTimer;
        //the camera for the 3D View
        //private Camera3D _camera;

        //creates a game and new scene instance as instance as its active scene
        public Game()
        {
            //game window size
            RL.InitWindow(1280, 760, "Hello World");
            RL.SetTargetFPS(15);
            _gameTimer = new Timer();

            //Raylib.Vector3 cameraPosition = new Raylib.Vector3(-10, -10, -10);
            //Raylib.Vector3 cameraTarget = new Raylib.Vector3(0, 0, 0);
            //Raylib.Vector3 cameraUp = new Raylib.Vector3(0, 0, 1);

            //_camera = new Camera3D(cameraPosition, cameraTarget, cameraUp);
        }

        
        private void Init()
        {
            Room startingRoom = new Room();
            Room northRoom = new Room();
            Room eastRoom = new Room();
            Room southRoom = new Room();
            
            //create a player and position it
            Player player = new Player("crying.jpg");
            player.X = 4;
            player.Y = 3;
            //player.Sprite.X -= 0.5f;
            //player.Sprite.Y -= 0.5f;




            //add the player to the starting room
            startingRoom.AddEntity(player);
            

            //create an enemy
            Enemy enemy = new Enemy("cryer.jpg");

            //add enemy to the northroom
            northRoom.AddEntity(enemy);

            //reset teh enemy's poition when we enter the north room
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
            //sides
            for (int i = 1; i < startingRoom.SizeX - 1; i++)
            {
                startingRoom.AddEntity(new Wall(0, i));
                if (i != 2)
                {
                    startingRoom.AddEntity(new Wall(11, i));
                }
            }
            //top and bottom
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

            CurrentScene = startingRoom;
        }
        
        public void Run()
        {
            //bind Ecs to exit the game (no longre needed)
            //PlayerInput.AddKeyEvent(Quit, ConsoleKey.Escape);

            Init();
            Camera2D camera = new Camera2D();
            camera.zoom = 3;

            //loop til the game is over
            while (!Gameover && !RL.WindowShouldClose())
            {
                //Change the Scene if needed
                if (_currentScene != _nextScene)
                {
                    _currentScene = _nextScene;
                }

                //Update the active Scene
                _currentScene.Update(_gameTimer.GetDeltaTime());

                //start the Scene if needed
                if (_currentScene.Started == false)
                {
                    _currentScene.Start();
                }
                

                //int mouseX = (RL.GetMouseX() - 320) / 16;
                //int mouseY = (RL.GetMouseY() - 240) / 16;
                //Raylib.Vector3 cameraPosition = new Raylib.Vector3(mouseX, mouseY, -100);
                //Raylib.Vector3 cameraTarget = new Raylib.Vector3(mouseX, mouseY, 0);
                //Raylib.Vector3 cameraUp = new Raylib.Vector3(1, 1, 1);

                //_camera = new Camera3D(cameraPosition, cameraTarget, cameraUp);

                //Draw the active Scene
                RL.BeginDrawing();
                RL.BeginMode2D(camera);
                _currentScene.Draw();
                RL.EndMode2D();
                RL.EndDrawing();
            }

            RL.CloseWindow();
        }

        //the scene we are currently running and start the first scene
        public static Scene CurrentScene
        {
            set
            {
                _nextScene = value;
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

        //private Room LoadRoom(string path)
        //{
        //    StreamReader reader = new StreamReader(path);

        //    int width, height;

        //    Int32.TryParse(reader.ReadLine(), out width);
        //    Int32.TryParse(reader.ReadLine(), out height);

        //    Room room = new Room(width, height);

        //    for (int y = 0; y < height; y++)
        //    {
        //        string row = reader.ReadLine();
        //        for (int x = 0, x < width; x++)
        //        {
        //            char title = row[x];
        //            switch (title)
        //            {
        //                case '1':
        //                    room.AddEntity(new Wall(x, y));
        //                    break;
        //                case '@':
        //                    Player p = new Player();
        //                    p.X = x;
        //                    p.Y = y;
        //                    room.AddEntity(p);
        //                    break;
        //                case 'e':
        //                    Enemy e = new Enemy();
        //                    e.X = x;
        //                    e.Y = y;
        //                    room.AddEntity(e);
        //                    break;
        //            }
        //        }
        //    }
        //    return room;
        //}
    }
}
