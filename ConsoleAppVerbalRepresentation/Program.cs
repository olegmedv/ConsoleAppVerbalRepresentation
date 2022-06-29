using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleAppVerbalRepresentation
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -1;     // -10...10
            string inputString = "";
            string verbalString = "";
            string[] temp;

            Console.WriteLine("Example #1");
            Console.WriteLine("1357256,32 to");
            synthesizer.Speak("Example one");
            inputString = "1357256,32";

            verbalString = "";
            temp = inputString.Split(',');
            foreach (var itemTemp in temp)
            {
                verbalString = verbalString + (verbalString == "" ? "" : "and ") + NumberToWords(Int32.Parse(itemTemp)) + (verbalString == "" ? " dollars " : " cents");

            }
            Console.WriteLine(verbalString);
            synthesizer.Speak(verbalString);

            Console.WriteLine("Example #2");
            Console.WriteLine("1123456789 to");
            synthesizer.Speak("Example two");
            inputString = "1123456789";

            verbalString = "";
            temp = inputString.Split(',');
            foreach (var itemTemp in temp)
            {
                verbalString = verbalString + (verbalString == "" ? "" : "and ") + NumberToWords(Int32.Parse(itemTemp)) + (verbalString == "" ? " dollars " : " cents");

            }
            Console.WriteLine(verbalString);
            synthesizer.Speak(verbalString);

            Console.WriteLine("Example #3");
            Console.WriteLine("0,32 to");
            synthesizer.Speak("Example three");
            inputString = "0,32";

            verbalString = "";
            temp = inputString.Split(',');
            foreach (var itemTemp in temp)
            {
                verbalString = verbalString + (verbalString == "" ? "" : "and ") + NumberToWords(Int32.Parse(itemTemp)) + (verbalString == "" ? " dollars " : " cents");

            }
            Console.WriteLine(verbalString);
            synthesizer.Speak(verbalString);


            while (!(Console.KeyAvailable))
            {
                Console.WriteLine("Write input value, please, or Press the Escape (Esc) key to quit:");
                synthesizer.Speak("Write input value, please, or Press the Escape (Esc) key to quit");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
                inputString = Console.ReadLine();
                inputString = inputString.Replace(".", ",");

                double n;
                while (!double.TryParse(inputString, out n))
                {
                    Console.WriteLine("You entered an invalid number");
                }
                verbalString = "";

                foreach (var itemTemp in temp)
                {
                    verbalString = verbalString + (verbalString == "" ? "" : "and ") + NumberToWords(Int32.Parse(itemTemp)) + (verbalString == "" ? " dollars " : " cents");

                }
                Console.WriteLine(verbalString);
                synthesizer.Speak(verbalString);
            }
        }

        static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";
            if ((number / 1000000000) > 0)
            {
                words += NumberToWords(number / 1000000000) + " billion, ";
                number %= 1000000000;
            }
            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million, ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand, ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }

            return words;
        }

    }
}
