using Domain.Models;
namespace Presentation.ConsoleApp
{
    public interface ITextDisplayService
    {
        void Header(string text);
        string ToSentenceCase(string text);
        void AwaitKeyPress();
        void Selected(string option);
        void ConfirmationMessage(string message);
        void ErrorMessage(string message);
        void ContactList(int index, Contact contact);
        void ClearConsoleLines(int linesToClear);

    }
}