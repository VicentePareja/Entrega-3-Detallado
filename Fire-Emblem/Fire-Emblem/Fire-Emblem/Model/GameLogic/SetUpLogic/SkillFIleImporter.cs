using System.Text.Json;
namespace Fire_Emblem;

public class SkillFileImporter
{
    private string _basePath;
    public SkillFileImporter(string basePath)
    {
        _basePath = basePath;
    }

    public List<Skill> ImportSkills()
    {
        string jsonPath = Path.Combine(_basePath, "skills.json");
        List<Skill> skills = new List<Skill>();
        try
        {
            string jsonString = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Skill> loadedSkills = JsonSerializer.Deserialize<List<Skill>>(jsonString, options);
            if (loadedSkills != null)
            {
                var skillFactory = new SkillFactory();
                foreach (var loadedSkill in loadedSkills)
                {
                    Skill skill = skillFactory.CreateSkill(loadedSkill.Name, loadedSkill.Description);
                    skills.Add(skill);
                }
            }

            return skills;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al importar habilidades: {ex.Message}");
            return skills;
        }

    }
}