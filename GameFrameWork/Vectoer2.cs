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

        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public float MagitudeSqr()
        {
            return (x * x + y * y);
        }
        public float Distance(Vectoer2 other)
        {
            float diffx = x - other.x;
            float diffy = y - other.y;
            return (float)Math.Sqrt(diffx * diffx + diffy * diffy);
        }

        public float Dot(Vectoer2 rhs)
        {
            return x * rhs.x + y * rhs.y;
        }

        public override string ToString()
        {
            return "{"+ x + "," + y + "}";
        }
        
        public void Normalize()
        {
            float m = Magnitude();
            x /= m;
            y /= m;
        }

        public Vectoer2 GetNormalize()
        {
            return (this / Magnitude());
        }
        public float GetAngle(Vectoer2 other)
        {
            Vectoer2 a = GetNormalize();
            Vectoer2 b = other.GetNormalize();
            return (float)Math.Acos(a.Dot(b));
        }
    }
}
