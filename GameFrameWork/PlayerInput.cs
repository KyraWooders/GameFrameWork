using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GameFrameWork
{
    static class PlayerInput
    {
        private delegate void KeyEvent(int key);

        private static KeyEvent OnKeyPress;

        //binds the specified event to the specified consolekey
        public static void AddKeyEvent(Event action, int key)
        {
            //local method that takes a consolekey and calls actiion if the specifi
            void keyPressed(int keyPress)
            {
                if (key == keyPress)
                {
                    action();
                }
            }

            //add the local method to the onkeypress keyevent
            OnKeyPress += keyPressed;
        }

        //gets input from the console 
        public static void ReadKey()
        {
            //ConsoleKey inputKey = Console.ReadKey().Key;
            int inputKey = RL.GetKeyPressed();
            OnKeyPress(inputKey);
        }
    }
}
