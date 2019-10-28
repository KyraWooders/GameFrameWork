using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    class Scene
    {
        //teh list of all th eEnities in the scene
        private List<Entity> _entities = new List<Entity>();
        //the list of entities to add to the scenes
        private List<Entity> _additions = new List<Entity>();
        //the list of 
        private List<Entity> _removals = new List<Entity>();
        private int _sizeX;
        private int _sizeY;
        private bool[,] _collision;
        //the grid for Entity tracking
        private List<Entity>[,] _tracking;

        public Event OnStart;
        public Event OnUpdate;
        public Event OnDraw;

        public Scene() : this(24, 6)
        {

        }

        //creates a new scene with the specififed size
        //sizeX: the horizontal size of the scene 
        //sizeY: the vertical size of the scene
        public Scene(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;

            //create the collision grid
            _collision = new bool[_sizeX, _sizeY];

            //create the tracking grid
            _tracking = new List<Entity>[_sizeX, _sizeY];
        }

        //the horizontal size of the Scene
        public int SizeX
        {
            get
            {
                return _sizeX;
            }
        }

        //the vertical size of the scene
        public int SizeY
        {
            get
            {
                return _sizeY;
            }
        }


        //int counter = 0;
        //called in game when the scene should begin
        public void Start()
        {
            OnStart?.Invoke();

            foreach (Entity e in _entities)
            {
                //
                e.Start();
            }
        }

        //Called in Game every step to Update each entity in the scene
        public void Update()
        {
            OnUpdate?.Invoke();

            //clear the collision grid
            _collision = new bool[_sizeX, _sizeY];

            //Clear the tracking grid
            for (int i = 0; i < _sizeY; i++)
            {
                for (int t = 0; t < _sizeX; t++)
                {
                    _tracking[t, i] = new List<Entity>();
                }
            }

            //add all the entities readied for addition
            foreach (Entity e in _additions)
            {
                //add e from _entities
                _entities.Add(e);
            }
            _additions.Clear();

            //remove all the entities readied for removal
            foreach (Entity e in _removals)
            {
                //remove e from _entities
                _entities.Remove(e);
            }
            //resetthe removal list
            _removals.Clear();

            foreach (Entity e in _entities)
            {
                //set the Entity"s collision in the collision grid
                int x = (int)e.X;
                int y = (int)e.Y;

                //only update if the entity is within bounds
                if (x >= 0 && x < _sizeX && y >= 0 && y < _sizeY)
                {
                    //add the Entity to the tracking grid 
                    _tracking[x, y].Add(e);

                    //only update this point in the grid if the entity is solid
                    if (!_collision[x, y])
                    {
                        _collision[x, y] = e.Solid;
                    }
                }
            }

            foreach (Entity e in _entities)
            {
                //call the Entity's Update events
                e.Update();
            }
            //counter++;
        }

        //called in Game every step to render
        public void Draw()
        {
            OnDraw?.Invoke();

            //clear the screen
            Console.Clear();
            RL.ClearBackground(Color.DARKBLUE);
            //Console.Write(counter);

            //create the display buffer
            char[,] display = new char[_sizeX, _sizeY];

            foreach (Entity e in _entities)
            {


                //Position each Entity's icon in display
                if (e.X >= 0 && e.X < _sizeX
                    && e.Y >= 0 && e.Y < _sizeY)
                {
                    display[(int)e.X, (int)e.Y] = e.Icon;
                }

            }

            //render the display gird to the screen
            for (int y = 0; y < _sizeY; y++)
            {
                for (int x = 0; x < _sizeX; x++)
                {
                    Console.Write(display[x, y]);
                    foreach (Entity e in _tracking[x, y])
                    {
                        RL.DrawTexture(e.Sprite, (int)(e.X * Game.SizeX), (int)(e.Y * Game.SizeY), Color.WHITE);
                    }
                }
                Console.WriteLine();
            }

            foreach (Entity e in _entities)
            {
                //call the Entity' draw events
                e.Draw();
            }
        }

        //Add an Entity to the Scene and set the scene as the entity;s scene
        public void AddEntity(Entity entity)
        {
            //ready the entity for addition
            _additions.Add(entity);
            //set this scene as the entity's scene
            entity.CurrentScene = this;
        }

        //Remove an Entity from the Scene
        public void RemoveEntity(Entity entity)
        {
            //
            _removals.Add(entity);
            //
            entity.CurrentScene = null;
        }

        //clear the Scene of Entities
        public void ClearEntities()
        {
            //nullify each entity's scene
            foreach (Entity e in _entities)
            {
                RemoveEntity(e);
            }
        }

        //returns whether there is a solid entity at the point
        public bool GetCollision(float x, float y)
        {
            //ensure the point is within the scene
            if (x >= 0 && y >= 0 && x < _sizeX && y < _sizeY)
            {
                return _collision[(int)x, (int)y];
            }

            //a point outside the scene is not a collision
            else
            {
                return true;
            }
        }

        //returns the list of Entities at a specified point
        public List<Entity> GetEntities(float x, float y)
        {
            //ensure the point is within the scene
            if (x >= 0 && y >= 0 && x < _sizeX && y < _sizeY)
            {
                return _tracking[(int)x, (int)y];
            }
            //a point outside the scene is not a collision
            else
            {
                return new List<Entity>();
            }
        }
    }
}
