using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    static class PlayerInput
    {
        private delegate void KeyEvent(ConsoleKey key);

        private static KeyEvent OnKeyPress;

        public static void AddKeyEvent(Event action, ConsoleKey key)
        {
            void keyPressed(ConsoleKey keyPress)
            {
                if (key == keyPress)
                {
                    action();
                }
            }
            OnKeyPress += keyPressed;
        }

        public static void ReadKey()
        {
            ConsoleKey inputKey = Console.ReadKey().Key;
            OnKeyPress(inputKey);
        }
    }
}
