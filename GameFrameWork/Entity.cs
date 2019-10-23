using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    delegate void Event();
    
    class Entity
    {
        //events that are called when the entity is started, updated, and draw
        public Event OnStart;
        public Event OnUpdate;
        public Event OnDraw;

        //the location of the entity
        private Vectoer2 _location = new Vectoer2();

        //the character representing the entity on the screen
        public char Icon { get; set; } = ' ';
        public Texture2D Sprite { get; set; }

        //whether or not this entity returns a collision
        public bool Solid { get; set; } = false;

        //the entity's location on the axis
        public float X
        {
            get
            {
                return _location.x;
            }
            set
            {
                _location.x = value;
            }
        }
        public float Y
        {
            get
            {
                return _location.y;
            }
            set
            {
                _location.y = value;
            }
        }
        

        private Scene _scene;
        public Scene CurrentScene
        {
            set
            {
                _scene = value;
            }
            get
            {
                return _scene;
            }
        }

        public Entity()
        {

        }

        //creates an entity with the specified icon and default values
        public Entity(char icon)
        {
            Icon = icon;
        }

        //creates an Entity with the specitfied icon and image
        public Entity(char icon, string imageName) : this(icon)
        {
            Sprite = RL.LoadTexture(imageName);
        }

        //call the entity's onstart event
        public void Start()
        {
            OnStart?.Invoke();
        }

        //call the entity's onupdate event
        public void Update()
        {
            OnUpdate?.Invoke();
        }

        //call the entity'sw onDraw event
        public void Draw()
        {
            OnDraw?.Invoke();
        }
    }
}
