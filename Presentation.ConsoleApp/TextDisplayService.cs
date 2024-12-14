using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ConsoleApp
{
    public class TextDisplayService
    {
        public static void Header(string text)
        {
            Console.WriteLine(text.ToUpper());
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        public static string ToSentenceCase(string text)
        {
            text = text.ToLower();
            return char.ToUpper(text[0]) + text.Substring(1);
        }

        public static void AwaitKeyPress()
        {
            Console.WriteLine("Press any key to continue...");
            Console.WriteLine();
            Console.ReadKey();
        }

        public static void Selected(string option)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("-->" + option);
            Console.ResetColor();
        }

        public static void ConfirmationMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            AwaitKeyPress();
            Console.ResetColor();
        }

        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            AwaitKeyPress();
            Console.ResetColor();
        }

        public static void ContactList(int index, Contact contact)
        {
            Console.WriteLine();
            Console.WriteLine($"[{index}]");
            Console.WriteLine("_____________________________________________________");
            Console.WriteLine($"{"Full name:",-13}{contact.FirstName} {contact.LastName}");
            Console.WriteLine($"{"Email:",-13}{contact.Email}");
            Console.WriteLine($"{"Phone:",-13}{contact.PhoneNumber}");
            Console.WriteLine($"{"Adress:",-13}{contact.Address}");
            Console.WriteLine($"{"",-13}{contact.Postcode} {contact.City}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{"ID:",-13}{contact.Id}");
            Console.ResetColor();

        }
    }
}
