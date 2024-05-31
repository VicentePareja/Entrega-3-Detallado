using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Fire_Emblem;

public class CharacterFileImporter
{
    private readonly string _dataFolder;

    public CharacterFileImporter(string basePath)
    {
        _dataFolder = Path.Combine(basePath);
    }

    public List<Character> ImportCharacters()
    {
        string jsonPath = Path.Combine(_dataFolder, "characters.json");
        try
        {
            string jsonString = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new StringToIntConverter() }
            };

            return JsonSerializer.Deserialize<List<Character>>(jsonString, options) ?? new List<Character>();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error al importar personajes: {ex.Message}", ex);
        }
    }

}
