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
        private Entity _sword = new Entity('/', "cryingcat.jpg");

        public Player() : this('@')
        {

        }

        public Player(string imageName) : base('@', imageName)
        {
            //bind movement methods to the arrow keysc
            _input.AddKeyEvent(MoveRight, 100);//D
            _input.AddKeyEvent(MoveLeft, 97);//A
            _input.AddKeyEvent(MoveUp, 119);//W
            _input.AddKeyEvent(MoveDown, 115);//S
            _input.AddKeyEvent(DetachSword, 69);//Shift + E
            _input.AddKeyEvent(AttachSword, 101);//E
            //add Readkey to this Entity's onupdate
            OnUpdate += _input.ReadKey;
            OnUpdate += Orbiit;
            OnStart += AddSword;
            OnStart += AttachSword;
        }

        public Player(char icon, string imageName) : base(icon, imageName)
        {

        }

        public Player(char icon) : base(icon)
        {

        }

        //add a sword to the scene
        private void AddSword()
        {
            //_sword = new Entity('/', "cryingcat.jpg");
            CurrentScene.AddEntity(_sword);
        }
        //add sword as child
        private void AttachSword()
        {
            AddChild(_sword);
            _sword.X = 1.25f;
            _sword.Y = 0.05f;
        }

        //drop the sword
        private void DetachSword()
        {
           // _sword.X = _sword.XAbsolute;
           // _sword.Y = _sword.YAbsolute;
            RemoveChild(_sword);
        }

        private void Orbiit()
        {
            foreach (Entity child in _children)
            {
                //child.Rotate(0.5f);
            }

            Rotate(0.5f);
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
