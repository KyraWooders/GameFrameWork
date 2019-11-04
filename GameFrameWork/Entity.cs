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
        //private Vector2 _location = new Vector2();
        private Vector3 _location = new Vector3(0, 0, 1);
        //the velocity of the entity
        //private Vector2 _velocity = new Vector2();
        //private Matrix3 _transform = new Matrix3();
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        //private Matrix3 _scale = new Matrix3();
        private float _scale = 1.0f;

        //the character representing the entity on the screen
        public char Icon { get; set; } = ' ';
        //teh image re
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
        //the entity's location on the Y axis
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
        //Entity's velocity on the X axis
        public float XVelocity
        {
            get
            {
                //return _velocity.x;
                return _translation.m13;
            }
            set
            {
                //_velocity.x = value;
                _translation.SetTranslation(value, YVelocity, 1);
            }
        }
        //Entity's velocity on the Y axis
        public float YVelocity
        {
            get
            {
                //return _velocity.y;
                return _translation.m23;
            }
            set
            {
                //_velocity.y = value;
                _translation.SetTranslation(XVelocity, value, 1);
            }
        }
        //thge entity's scale
        public float Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
            }
        }
        //the entity's rotation in radians
        public float rotation
        {
            get
            {
                return (float)Math.Atan2(_rotation.m12, _rotation.m11);
            }
            set
            {
                _rotation.SetRotateZ(value);
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
            //_location += _velocity;
            Matrix3 transform = _translation * _rotation;
            _location = transform * _location;
            OnUpdate?.Invoke();
        }

        //call the entity'sw onDraw event
        public void Draw()
        {
            OnDraw?.Invoke();
        }
    }
}
