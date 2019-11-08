using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    class SpriteEntity : Entity
    {
        private Texture2D _texture = new Texture2D();
        private Image _image = new Image();

        public float Width
        {
            get
            {
                return _texture.width;
            }

        }
        public float Height
        {
            get
            {
                return _texture.height;
            }

        }

        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
        }

        public SpriteEntity()
        {

        }

        public void Load(string path)
        {
            _image = RL.LoadImage(path);
            _texture = RL.LoadTextureFromImage(_image);
        }
    }
}
