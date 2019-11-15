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
        public float Speed { get; set; } = 5f;

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
        private void TouchPlayer(float deltaTime)
        {
            //get the list of Entities in our space
            List<Entity> touched = CurrentScene.GetEntities(X, Y);
            
            //check if any of them are players
            foreach (Entity e in touched)
            {
                if (e is Player)
                {
                    CurrentScene.RemoveEntity(this);
                    break;
                }
            }
        }

        private void Move(float deltaTime)
        {
            //Rotate(0.09f);
            switch (_facing)
            {
                case Direction.North:
                    MoveUp(deltaTime);
                    break;
                case Direction.South:
                    MoveDown(deltaTime);
                    break;
                case Direction.East:
                    MoveRight(deltaTime);
                    break;
                case Direction.West:
                    MoveLeft(deltaTime);
                    break;
            }
        }

        private void MoveDown(float deltaTime)
        {
            //move down if the space is clear
            if (!CurrentScene.GetCollision(XAbsolute, Sprite.Bottom + Speed * deltaTime))
            {
                YVelocity = Speed * deltaTime;
            }
            //Otherwise stop and change direction
            else
            {
                YVelocity = 0f;
                _facing++;
            }
        }
        private void MoveRight(float deltaTime)
        {
            if (!CurrentScene.GetCollision(Sprite.Right + Speed * deltaTime, YAbsolute))
            {
                XVelocity = Speed * deltaTime;
            }
            else
            {
                XVelocity = 0f;
                _facing++;
            }
        }
        private void MoveLeft(float deltaTime)
        {
            if (!CurrentScene.GetCollision(Sprite.Left - Speed * deltaTime, YAbsolute))
            {
                XVelocity = -Speed * deltaTime;
            }
            else
            {
                XVelocity = 0f;
                _facing = Direction.North;
            }
        }

        //move one space up
        private void MoveUp(float deltaTime)
        {
            if (!CurrentScene.GetCollision(XAbsolute, Sprite.Top - Speed * deltaTime))
            {
                YVelocity = -Speed * deltaTime;
            }
            else
            {
                YVelocity = 0f;
                _facing++;
            }
        }
    }
}
