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
        public Entity Sword
        {
            get
            {
                return _sword;
            }
        }

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
            CurrentScene.AddEntity(_sword);
            _sword.X = X;
            _sword.Y = Y;
        }
        //add sword as child
        private void AttachSword()
        {
            if (!Hitbox.Overlaps(_sword.Hitbox))
            {
                return;
            }
            AddChild(_sword);
            _sword.X = 1.25f;
            _sword.Y = 0.05f;
        }

        //drop the sword
        private void DetachSword()
        {
            if (_sword.CurrentScene != CurrentScene)
            {
                return;
            }
            RemoveChild(_sword);
        }

        private void Orbiit(float deltaTime)
        {
            foreach (Entity child in _children)
            {
                child.Rotate(0.5f * deltaTime);
            }

            Rotate(0.5f * deltaTime);
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
            if (_sword.Parent == this)
            {
                //remove the sword from the 
                CurrentScene.RemoveEntity(_sword);
                //
                destination.AddEntity(_sword);
            }
            //remove the player from its current room
            CurrentScene.RemoveEntity(this);
            //add the player to the destination room
            destination.AddEntity(this);
            //change the game's active snene to the destination
            Game.CurrentScene = destination;
        }
        
    }
}
