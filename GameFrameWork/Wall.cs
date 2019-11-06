using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Wall : Entity
    {
        public Wall(int x, int y) : base('█', "cry.jpg")
        {
            X = x;
            Y = y;
            Solid = true;
            OnUpdate += Spin;
        }

        public Wall(int x, int y, string imageName) : base('█', imageName)
        {
            X = x;
            Y = y;
            Solid = true;
        }

        void Spin()
        {
            Rotate(10f);
            //Rotation = 0.01f;
        }
    }
}
