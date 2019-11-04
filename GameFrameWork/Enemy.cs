using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Enemy : Entity
    {
        private Direction _facing;

        public float Speed { get; set; } = 0.05f;

        //cretes a new enemy represented by the 'e' symdol and rat image
        public Enemy() : this('*')
        {

        }
        public Enemy(char icon) : base(icon)
        {
            _facing = Direction.North;
            OnUpdate += Move;
            OnUpdate += TouchPlayer;
        }

        public Enemy(string imageName) : base('*', imageName)
        {
            _facing = Direction.North;
            OnUpdate += Move;
            OnUpdate += TouchPlayer;
        }

        //Check to see if the Enemy has touched a player and remove itself if so
        private void TouchPlayer()
        {
            //get the list of Entities in our space
            List<Entity> touched = CurrentScene.GetEntities(X, Y);
            
            //check if any of them are players
            bool hit = false;
            foreach (Entity e in touched)
            {
                if (e is Player)
                {
                    hit = true;
                    break;
                }
            }

            //if we hit a player, remove this enemy from the scene
            if (hit)
            {
                CurrentScene.RemoveEntity(this);
            }
        }

        private void Move()
        {
            rotation = 5;
            switch (_facing)
            {
                case Direction.North:
                    MoveUp();
                    break;
                case Direction.South:
                    MoveDown();
                    break;
                case Direction.East:
                    MoveRight();
                    break;
                case Direction.West:
                    MoveLeft();
                    break;
            }
        }

        private void MoveDown()
        {
            //move down if the space is clear
            if (!CurrentScene.GetCollision(X, Y + 1))
            {
                YVelocity = Speed;
            }
            //Otherwise stop and change direction
            else
            {
                YVelocity = 0f;
                _facing++;
            }
        }
        private void MoveRight()
        {
            if (!CurrentScene.GetCollision(X + 1, Y))
            {
                XVelocity = Speed;
            }
            else
            {
                XVelocity = 0f;
                _facing++;
            }
        }
        private void MoveLeft()
        {
            if (!CurrentScene.GetCollision(X - 1, Y))
            {
                XVelocity = -Speed;
            }
            else
            {
                XVelocity = 0f;
                _facing = Direction.North;
            }
        }

        //move one space up
        private void MoveUp()
        {
            if (!CurrentScene.GetCollision(X, Y - 1))
            {
                YVelocity = -Speed;
            }
            else
            {
                YVelocity = 0f;
                _facing++;
            }
        }
    }
}
