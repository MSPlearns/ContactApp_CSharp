using Domain.Models;

namespace Presentation.ConsoleApp;
public interface ITextDisplayService
{
    void AwaitKeyPress();
    void ClearConsoleLines(int linesToClear);
    void ConfirmationMessage(string message);
    void ContactList(int index, Contact contact);
    void ErrorMessage(string message);
    void Header(string text);
    void Selected(string option);
    string ToSentenceCase(string text);
}