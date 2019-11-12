using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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

        protected Entity _parent = null;
        protected List<Entity> _children = new List<Entity>();

        //the location of the entity
        //private Vector2 _location = new Vector2();
        //private Vector3 _location = new Vector3(0, 0, 1);
        //the velocity of the entity
        private Vector2 _velocity = new Vector2();
        //private Matrix3 _transform = new Matrix3();
        //private Matrix3 _translation = new Matrix3();
        //private Matrix3 _rotation = new Matrix3();
        //private Matrix3 _scale = new Matrix3();
        //private float _scale = 1.0f;
        private Matrix3 _localTransform = new Matrix3();
        private Matrix3 _globalTransform = new Matrix3();

        //the character representing the entity on the screen
        public char Icon { get; set; } = ' ';
        //teh image represing the entity on the screen
        public SpriteEntity Sprite { get; set; }
        //whether or not this entity returns a collision
        public bool Solid { get; set; } = false;
        //the entity's relative origin
        public float OriginX { get; set; } = 0;
        public float OriginY { get; set; } = 0;
         //the entity's location on the axis
        public float X
        {
            get
            {
                return _localTransform.m13;
            }
            set
            {
                _localTransform.SetTranslation(value, Y, 1);
                UpdateTransform();
            }
        }

        public float XAbsolute
        {
            get { return _globalTransform.m13; }
        }

        //the entity's location on the Y axis
        public float Y
        {
            get
            {
                return _localTransform.m23;
            }
            set
            {
                _localTransform.SetTranslation(X, value, 1);
                UpdateTransform();
            }
        }

        public float YAbsolute
        {
            get { return _globalTransform.m23; }
        }
        
        //Entity's velocity on the X axis
        public float XVelocity
        {
            get
            {
                return _velocity.x;
                //return _translation.m13;
            }
            set
            {
                _velocity.x = value;
                //_translation.SetTranslation(value, YVelocity, 1);
            }
        }

        //Entity's velocity on the Y axis
        public float YVelocity
        {
            get
            {
                return _velocity.y;
                //return _translation.m23;
            }
            set
            {
                _velocity.y = value;
                //_translation.SetTranslation(XVelocity, value, 1);
            }
        }

        //thge entity's scale
        public float Size
        {
            get
            {
                //return _scal
                //return _scale;
                return 1;
            }
            //set
            //{
            //    _localTransform.SetScaled(value, value, 1);
            //    //_scale = value;
            //}
        }

        //the entity's Rotation in radians
        public float Rotation
        {
            get
            {
                return (float)Math.Atan2(_globalTransform.m21, _globalTransform.m11);
            }
            //set
            //{
            //    _localTransform.SetRotateZ(value);
            //}
        }

        //public float Width
        //{
        //    get
        //    {

        //    }

        //}

        
        private Scene _scene;
        //the scene the entity is currently in
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

        //the parent of this entity
        public Entity Parent
        {
            get
            {
                return _parent;
            }
        }

        //creates an entity
        public Entity()
        {

        }

        //creates an entity with the specified icon and default values
        public Entity(char icon) : this()
        {
            Icon = icon;
        }

        //creates an Entity with the specitfied icon and image
        public Entity(char icon, string imageName) : this(icon)
        {
            Sprite = new SpriteEntity();
            Sprite.Load(imageName);
            AddChild(Sprite);
        }

        ~Entity()
        {
            if (_parent != null)
            {
                _parent.RemoveChild(this);
            }

            foreach (Entity e in _children)
            {
                e._parent = null;
            }
        }

        public int GetChildCount()
        {
            return _children.Count;
        }

        public Entity GetChild(int index)
        {
            return _children[index];
        }

        public void AddChild(Entity child)
        {
            //make sure the child doesn't already have a parent
            if(child._parent != null)
            {
                return;
            }
            //assign this Entity as the child's parent
            child._parent = this;
            //add new child to collection
            _children.Add(child);
        }

        public void RemoveChild(Entity child)
        {
            bool isMyChild = _children.Remove(child);
            if (isMyChild)
            {
                child._parent = null;
                child._localTransform = child._globalTransform;
            }
        }

        //scale the entitu by the specified amount
        public void Scale(float width, float height)
        {
            _localTransform.Scale(width, height, 1);
            UpdateTransform();
        }

        //rotate the entity by the specifed amount
        public void Rotate(float radians)
        {
            _localTransform.RotateZ(radians);
            UpdateTransform();
        }

        protected void UpdateTransform()
        {
            if (_parent != null)
            {
                _globalTransform = _parent._globalTransform * _localTransform;
            }
            else
            {
                _globalTransform = _localTransform;
            }

            foreach (Entity child in _children)
            {
                child.UpdateTransform();
            }
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
            //Matrix3 transform = _translation * _rotation;
            //_location = transform * _location;
            X += _velocity.x;
            Y += _velocity.y;
            OnUpdate?.Invoke();
        }

        //call the entity'sw onDraw event
        public void Draw()
        {
            OnDraw?.Invoke();
        }
    }
}
