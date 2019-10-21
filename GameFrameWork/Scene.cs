using System;
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
        private bool[,] _collision;
        //the grid for Entity tracking
        private List<Entity>[,] _tracking;

        public Event OnStart;
        public Event OnUpdate;
        public Event OnDraw;

        public Scene() : this(12, 6)
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
            OnStart?.Invoke();

            foreach (Entity e in _entities)
            {
                e.Start();
            }
        }

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

            foreach (Entity e in _entities)
            {
                //call the Entity's Update events
                e.Update();

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
            //counter++;
        }

        //called in Game every step to render
        public void Draw()
        {
            OnDraw?.Invoke();

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
                    display[(int)e.X, (int)e.Y] = e.Icon;
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

        public bool GetCollision(float x, float y)
        {
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
