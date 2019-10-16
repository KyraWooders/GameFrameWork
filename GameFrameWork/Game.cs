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

        private Scene _currentScene;

        public Game()
        {
            _currentScene = new Scene();
        }

        public void Run()
        {
            Player player = new Player();
            player.X = 4;
            player.Y = 3;
            Entity enemy = new Entity('#');
            enemy.X = 4;
            enemy.Y = 5;

            _currentScene.AddEntity(player);
            _currentScene.AddEntity(enemy);

            _currentScene.Start();

            //loop til the game is over
            while (!Gameover)
            {
                _currentScene.Update();
                _currentScene.Draw();
                PlayerInput.ReadKey();
            }

        }

        public Scene CurrentScene
        {
            get
            {
                return _currentScene;
            }
        }
    }
}
