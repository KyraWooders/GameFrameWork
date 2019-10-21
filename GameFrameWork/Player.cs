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
            if (!CScene.GetCollision(X + 1, Y))
            {
                X++;
            }
        }

        //move one space to the left
        private void MoveLeft()
        {
            if (!CScene.GetCollision(X - 1, Y))
            {
                X--;
            }
        }

        private void MoveDown()
        {
            if (!CScene.GetCollision(X, Y+ 1))
            {
                Y++;
            }
        }

        private void MoveUp()
        {

            if (!CScene.GetCollision(X, Y - 1))
            {
                Y--;
            }
        }
        private void Travel(Room destination)
        {
            if (destination == null)
            {
                return;
            }

            CScene.RemoveEntity(this);
            destination.AddEntity(this);
            Game.CurrentScene = destination;
        }
        
    }
}
