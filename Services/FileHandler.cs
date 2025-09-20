using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace MatchInvest.Services
{
    public class FileHandler
    {
        public static List<T> ReadJsonFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }

        public static void WriteJsonFile<T>(string filePath, List<T> data)
        {
            var jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }
    }

    public class Ativo
    {
        public string Nome { get; set; }
        public string Classe { get; set; }
        public string Descricao { get; set; }
    }
}