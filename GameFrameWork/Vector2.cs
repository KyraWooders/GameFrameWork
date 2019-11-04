using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Vector2
    {
        public float x;
        public float y;
        public Vector2()
        {
            x = 0;
            y = 0;
        }
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2 operator *(Vector2 lhs, float rhs)
        {
            return new Vector2(lhs.x * rhs, lhs.y * rhs);
        }

        public static Vector2 operator *(float lhs, Vector2 rhs)
        {
            return new Vector2(lhs * rhs.x, lhs * rhs.y);
        }

        public static Vector2 operator /(Vector2 lhs, float rhs)
        {
            return new Vector2(lhs.x / rhs, lhs.y / rhs);
        }
        public static Vector2 operator /(float lhs, Vector2 rhs)
        {
            return new Vector2(lhs / rhs.x, lhs / rhs.y);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public float MagitudeSqr()
        {
            return (x * x + y * y);
        }
        public float Distance(Vector2 other)
        {
            float diffx = x - other.x;
            float diffy = y - other.y;
            return (float)Math.Sqrt(diffx * diffx + diffy * diffy);
        }

        public float Dot(Vector2 rhs)
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

        public Vector2 GetNormalize()
        {
            return (this / Magnitude());
        }
        public float GetAngle(Vector2 other)
        {
            Vector2 a = GetNormalize();
            Vector2 b = other.GetNormalize();
            return (float)Math.Acos(a.Dot(b));
        }
    }
}
