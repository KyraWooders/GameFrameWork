using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Vectoer2
    {
        public float x;
        public float y;
        public Vectoer2()
        {
            x = 0;
            y = 0;
        }
        public Vectoer2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vectoer2 operator +(Vectoer2 lhs, Vectoer2 rhs)
        {
            return new Vectoer2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vectoer2 operator -(Vectoer2 lhs, Vectoer2 rhs)
        {
            return new Vectoer2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vectoer2 operator *(Vectoer2 lhs, float rhs)
        {
            return new Vectoer2(lhs.x * rhs, lhs.y * rhs);
        }

        public static Vectoer2 operator *(float lhs, Vectoer2 rhs)
        {
            return new Vectoer2(lhs * rhs.x, lhs * rhs.y);
        }

        public static Vectoer2 operator /(Vectoer2 lhs, float rhs)
        {
            return new Vectoer2(lhs.x / rhs, lhs.y / rhs);
        }
        public static Vectoer2 operator /(float lhs, Vectoer2 rhs)
        {
            return new Vectoer2(lhs / rhs.x, lhs / rhs.y);
        }
    }
}
