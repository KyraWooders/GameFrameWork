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
            if (X < CScene.SizeX-1)
            {
                X++;
            }
        }

        //move one space to the left
        private void MoveLeft()
        {
            if (X > 0)
            {
                X--;
            }
        }

        private void MoveDown()
        {
            if (Y < CScene.SizeY- 1)
            {
                Y++;
            }
        }

        private void MoveUp()
        {
            if (Y > 0)
            {
                Y--;
            }
        }
    }
}
