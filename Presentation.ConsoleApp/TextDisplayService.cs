using Domain.Models;

namespace Presentation.ConsoleApp;
public class TextDisplayService : ITextDisplayService
{
    public void Header(string text)
    {
        Console.WriteLine(text.ToUpper());
        for (int i = 0; i < text.Length; i++)
        {
            Console.Write("=");
        }
        Console.WriteLine();
    }

    public string ToSentenceCase(string text)
    {
        text = text.ToLower();
        return char.ToUpper(text[0]) + text.Substring(1);
    }

    public void AwaitKeyPress()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void Selected(string option)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine("-->" + option);
        Console.ResetColor();
    }

    public void ConfirmationMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void ErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void ContactList(int index, Contact contact)
    {
        Console.WriteLine();
        Console.WriteLine($"[{index}]");
        Console.WriteLine("_____________________________________________________");
        Console.WriteLine($"{"Full name:",-13}{contact.FirstName} {contact.LastName}");
        Console.WriteLine($"{"Email:",-13}{contact.Email}");
        Console.WriteLine($"{"Phone:",-13}{contact.PhoneNumber}");
        Console.WriteLine($"{"Adress:",-13}{contact.Address}");
        Console.WriteLine($"{"",-13}{contact.Postcode} {contact.City}");
        //Console.ForegroundColor = ConsoleColor.DarkGray;
        //Console.WriteLine($"{"ID:",-13}{contact.Id}");
        //Console.ResetColor();

    }

    // Method used to clear a determined number of lines of the console (Copied from a previous assigment from gymnasiet, implemented with help of the internet back then, no exact source)
    public void ClearConsoleLines(int linesToClear)
    {
        int currentLineCursor = Console.CursorTop;

        // Set the cursor position x lines back from the current position
        Console.SetCursorPosition(0, Console.CursorTop - linesToClear);

        // Clear the content of the lines
        for (int i = 0; i < linesToClear; i++)
        {
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }

        // Restore the cursor position to the original line
        Console.SetCursorPosition(0, currentLineCursor - linesToClear);
    }
}