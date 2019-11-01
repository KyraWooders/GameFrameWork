using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            //Examples();
            MatrixTest();
            Console.ReadKey();
            return;
            Game game = new Game();
            game.Run();

            

        }
        static void Examples()
        {
            Vectoer2 vec2 = new Vectoer2(3f, -2f);
            Console.WriteLine(vec2.Magnitude());
            Console.WriteLine(new Vector3(-1f, 1f, -1f).Magnitude());
            Console.WriteLine(new Vector3(0.5f, -1f, 0.25f).Magnitude());


            Console.WriteLine(new Vector3(0f, 1f, 2f).Distance(new Vector3(9f, -2f, 7f)));
            Console.WriteLine(new Vectoer2(1f, 0f).Dot(new Vectoer2(0f, 1f)));
            Console.WriteLine(new Vectoer2(1f, 1f).Dot(new Vectoer2(-1f, -1f)));
            Console.WriteLine(new Vector3(2f, 3f, 1f).Dot(new Vector3(-3f, 1f, 2f)));

            Console.WriteLine(new Vector3(2f, 3f, 1f).Cross(new Vector3(-3f, 1f, 2f)));

            Console.WriteLine(new Vectoer2(1f, 3f).GetAngle(new Vectoer2(0.5f, -0.25f)));
            Console.WriteLine(new Vector3(-0.5f, 0f, 2f).GetAngle(new Vector3(-1f, 0, -1f)));

            Vector3 playerLoc = new Vector3(10f, 0f, 18f);
            Vector3 enemyLoc = new Vector3(-7.5F, 0f, 9f);
            Vector3 enemyForward = new Vector3(0.857f, 0f, -0.514f);
            Vector3 up = new Vector3(0f, 1f, 0f);

            Vector3 enemyToPlayer = playerLoc - enemyLoc;

            Console.WriteLine(enemyToPlayer);

            if (enemyForward.Dot(enemyToPlayer) > 0)
            {
                Console.WriteLine("PLayer is in front of enemy.");
            }
            else
            {
                Console.WriteLine("Player is behind enemy.");
            }

            Vector3 enemyLeft = enemyForward.Cross(up);

            if (enemyForward.Dot(enemyToPlayer) > 0)
            {
                Console.WriteLine("PLayer is in left of enemy.");
            }
            else
            {
                Console.WriteLine("Player is right of enemy.");
            }

            if (enemyForward.GetAngle(enemyToPlayer) <= Math.PI / 4 || enemyForward.GetAngle(enemyToPlayer) >= 7 * Math.PI / 4)
            {
                Console.WriteLine("I'VE GOT YOU IN MY SIGHTS");
            }
            else
            {
                Console.WriteLine("YOU CAN HID BUT YOU CAN'T RUN");
            }

            Console.ReadKey();
        }
        static void MatrixTest()
        {
            Matrix3 a = new Matrix3(1, 4, 7, 2, 5, 8, 3, 6, 9);
            Matrix3 b = new Matrix3(9, 6, 3, 8, 5, 2, 7, 4, 1);
            Matrix3 c = a * b;
            Console.WriteLine(c);
            Console.WriteLine(c * new Vector3(2, 4, 6));
        }
    }
}
