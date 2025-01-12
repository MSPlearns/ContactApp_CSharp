﻿using System.Text.Json;

namespace DataManagement.Services;
public class DataService : IDataService
{
    private readonly string _directoryPath; // Path to the directory where the file is stored
    private readonly string _filePath; // Name of the file
    private readonly JsonSerializerOptions _jsonSerializerOptions; // Cached JsonSerializerOptions instance

    public DataService(string directoryPath = "Data", string fileName = "list.json") // Default values for directoryPath and fileName
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
        _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true }; // Initialize the cached instance
    }

    public void SaveListToFile<T>(List<T> list) //Using generics in case we want to save a list of any type in the future
    {
        //Controll if the directory exists, otherwise, create it
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            var json = JsonSerializer.Serialize(list, _jsonSerializerOptions); // Serialize the list to a JSON string
            File.WriteAllText(_filePath, json);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    public List<T> LoadListFromFile<T>()
    {
        try
        {
            if (!File.Exists(_filePath)) 
            {
                return [];
            }

            var json = File.ReadAllText(_filePath);
            var list = JsonSerializer.Deserialize<List<T>>(json, _jsonSerializerOptions);
            return list!; 

        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            return []; 
        }
    }
}