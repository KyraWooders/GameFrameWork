using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Player : Entity
    {
        private PlayerInput _input = new PlayerInput();

        public Player() : this('@')
        {

        }

        public Player(string imageName) : base('@', imageName)
        {

            _input.AddKeyEvent(MoveRight, 100);//D
            _input.AddKeyEvent(MoveLeft, 97);//A
            _input.AddKeyEvent(MoveUp, 119);//W
            _input.AddKeyEvent(MoveDown, 115);//S

            OnUpdate += _input.ReadKey;
        }

        public Player(char icon, string imageName) : base(icon, imageName)
        {
            //bind movement methods to the arrow keys
            _input.AddKeyEvent(MoveRight, 100);//D
            _input.AddKeyEvent(MoveLeft, 97);//A
            _input.AddKeyEvent(MoveUp, 119);//W
            _input.AddKeyEvent(MoveDown, 115);//S
            //add Readkey to this Entity's onupdate
            OnUpdate += _input.ReadKey;
        }

        public Player(char icon) : base(icon)
        {

        }


        //Move one space to the right
        private void MoveRight()
        {
            if (X + 1 >= CurrentScene.SizeX)
            {
                if (CurrentScene is Room)
                {
                    Room dest = (Room)CurrentScene;
                    Travel(dest.East);
                }
                X = 0;
            }
            else if (!CurrentScene.GetCollision(X + 1, Y))
            {
                X++;
            }
        }

        //move one space to the left
        private void MoveLeft()
        {
            if (X - 1 < 0)
            {
                if (CurrentScene is Room)
                {
                    Room dest = (Room)CurrentScene;
                    Travel(dest.West);
                }
                X = CurrentScene.SizeX - 1;
            }
            else if (!CurrentScene.GetCollision(X - 1, Y))
            {
                X--;
            }
        }

        private void MoveDown()
        {
            if (Y + 1 >= CurrentScene.SizeY)
            {
                if (CurrentScene is Room)
                {
                    Room dest = (Room)CurrentScene;
                    Travel(dest.South);
                }
                Y = 0;
            }
            else if (!CurrentScene.GetCollision(X, Y + 1))
            {
                Y++;
            }
        }

        private void MoveUp()
        {
            if (Y - 1 < 0)
            {
                if (CurrentScene is Room)
                {
                    Room dest = (Room)CurrentScene;
                    Travel(dest.North);
                }
                Y = CurrentScene.SizeY - 1;
            }
            else if (!CurrentScene.GetCollision(X, Y - 1))
            {
                Y--;
            }
        }

        //move the player to the destination room and change the Scene 
        private void Travel(Room destination)
        {
            //ensure destion is not null
            if (destination == null)
            {
                return;
            }
            //remove the player from its current 
            CurrentScene.RemoveEntity(this);
            destination.AddEntity(this);
            Game.CurrentScene = destination;
        }
        
    }
}
