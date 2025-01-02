using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataManagement.Services
{
    public class DataService
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
            // Load userlist from file
            try
            {
                if (!File.Exists(_filePath)) // Check if the file doesnt exists return an empty list
                {
                    return [];
                }

                var json = File.ReadAllText(_filePath);
                var list = JsonSerializer.Deserialize<List<T>>(json, _jsonSerializerOptions);
                return list; // Return null list if the deserialization fails
                             // maybe should be an empty list???

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return []; // Return an empty list if an error occurs
                           // maybe it should be null??
            }
        }
    }
}
