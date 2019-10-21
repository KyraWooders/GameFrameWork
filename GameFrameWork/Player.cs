using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Player : Entity
    {
        public Player() : this('@')
        {

        }

        public Player(char icon) : base(icon)
        {
            PlayerInput.AddKeyEvent(MoveRight, ConsoleKey.RightArrow);
            PlayerInput.AddKeyEvent(MoveLeft, ConsoleKey.LeftArrow);
            PlayerInput.AddKeyEvent(MoveUp, ConsoleKey.UpArrow);
            PlayerInput.AddKeyEvent(MoveDown, ConsoleKey.DownArrow);
        }
        
        //Move one space to the right
        private void MoveRight()
        {
            if (X + 1 >= CScene.SizeX)
            {
                if (CScene is Room)
                {
                    Room dest = (Room)CScene;
                    Travel(dest.East);
                }
                X = 0;
            }
            else if (!CScene.GetCollision(X + 1, Y))
            {
                X++;
            }
        }

        //move one space to the left
        private void MoveLeft()
        {
            if (X - 1 < 0)
            {
                if (CScene is Room)
                {
                    Room dest = (Room)CScene;
                    Travel(dest.West);
                }
                X = CScene.SizeX - 1;
            }
            else if (!CScene.GetCollision(X - 1, Y))
            {
                X--;
            }
        }

        private void MoveDown()
        {
            if (Y + 1 >= CScene.SizeY)
            {
                if (CScene is Room)
                {
                    Room dest = (Room)CScene;
                    Travel(dest.South);
                }
                Y = 0;
            }
            else if (!CScene.GetCollision(X, Y + 1))
            {
                Y++;
            }
        }

        private void MoveUp()
        {
            if (Y - 1 < 0)
            {
                if (CScene is Room)
                {
                    Room dest = (Room)CScene;
                    Travel(dest.North);
                }
                Y = CScene.SizeY - 1;
            }
            else if (!CScene.GetCollision(X, Y - 1))
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
            CScene.RemoveEntity(this);
            destination.AddEntity(this);
            Game.CurrentScene = destination;
        }
        
    }
}
