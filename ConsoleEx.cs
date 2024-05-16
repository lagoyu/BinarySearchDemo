using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtensions
{
    public static class ConsoleEx
    {
        public static void WriteLine(String message="", ConsoleColor color=ConsoleColor.White)
        {
            var oldColor = Console.ForegroundColor;
            if (color != ConsoleColor.White)
                Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }

        public static void Write(String message="", ConsoleColor color = ConsoleColor.White)
        {
            var oldColor = Console.ForegroundColor;
            if (color != ConsoleColor.White)
                Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = oldColor;
        }


        public static void Pause(String prompt= "\nPress Enter to Continue")
        {
            WriteLine(prompt,ConsoleColor.Yellow);
            Console.ReadLine();
        }

        public static string InputLine(string prompt = "Enter text: ", ConsoleColor color = ConsoleColor.White)
        {
            string name;
            Write(prompt, color);
            name = Console.ReadLine();
            return name;
        }

        public static int InputInt(string prompt = "Enter an integer: ",
                            int min = int.MinValue + 1, int max = int.MaxValue, ConsoleColor color = ConsoleColor.White)
        {
            // prompt for an integer between min and max inclusive
            string input;
            int val;
            bool acceptable = false;
            do
            {
                input = InputLine(prompt, color);
                // input needs to be a valid integer and within range
                acceptable = (int.TryParse(input, out val) && val >= min && val <= max);
                if (!acceptable) Console.Beep();
            } while (!acceptable);
            return val;
        }

    }

}
