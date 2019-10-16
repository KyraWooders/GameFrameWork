﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Scene
    {
        private List<Entity> _entities = new List<Entity>();
        private int _sizeX;
        private int _sizeY;

        public Scene()
        {
            _sizeX = 24;
            _sizeY = 6;
        }

        public Scene(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
        }

        public int SizeX
        {
            get
            {
                return _sizeX;
            }
        }

        public int SizeY
        {
            get
            {
                return _sizeY;
            }
        }


        //int counter = 0;

        public void Start()
        {
            foreach (Entity e in _entities)
            {
                e.Start();
            }
        }

        public void Update()
        {
            foreach (Entity e in _entities)
            {
                e.Update();
            }
            //counter++;
        }

        public void Draw()
        {
            //clear the screen
            Console.Clear();
            //Console.Write(counter);

            //create the display buffer
            char[,] display = new char[_sizeX, _sizeY];

            foreach (Entity e in _entities)
            {
                //call the Entity' draw events
                e.Draw();

                //Position each Entity's icon in display
                if (e.X >= 0 && e.X < _sizeX
                    && e.Y >= 0 && e.Y < _sizeY)
                {
                    display[e.X, e.Y] = e.Icon;
                }
                
            }

            //render the display buffer to the screen
            for (int i = 0; i < _sizeY; i++)
            {
                for (int t = 0; t < _sizeX; t++)
                {
                    Console.Write(display[t, i]);
                }
                Console.WriteLine();
            }
            
        }

        //Add an Entity to the Scene
        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
            entity.CScene = this;
        }

        //Remove an Entity from the Scene
        public void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);
            entity.CScene = null;
        }

        //clear the Scene of Entities
        public void ClearEntities()
        {
            foreach (Entity e in _entities)
            {
                e.CScene = null;
            }
            _entities.Clear();
        }
    }
}