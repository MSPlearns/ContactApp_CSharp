namespace DataManagement.Services;
public interface IDataService
{
    void SaveListToFile<T>(List<T> list);
    List<T> LoadListFromFile<T>();
}