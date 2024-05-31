namespace Fire_Emblem;

public class CharacterChooser
{

    private Player _player1;
    private Player _player2;
    private bool _isPlayer1Team = true;
    private static List<Character> _characters;
    private static List<Skill> _skills;
    public CharacterChooser(Player player1, Player player2)
    {

        _player1 = player1;
        _player2 = player2;
    }
    public void ChooseCharacters(string selectedFilePath)
    {
        var lines = File.ReadAllLines(selectedFilePath);
        _isPlayer1Team = true; 

        foreach (var line in lines)
        {
            if (line == "Player 1 Team")
            {
                _isPlayer1Team = true;
            }
            else if (line == "Player 2 Team")
            {
                _isPlayer1Team = false;
            }
            else
            {
                AssignCharacterToTeam(line, _isPlayer1Team ? _player1.Team : _player2.Team);
            }
        }
    }
    
    public static void SetCharacters(List<Character> characters)
    {
        _characters = characters;
    }
    public static void SetSkills(List<Skill> skills)
    {
        _skills = skills;
    }
    
    private void AssignCharacterToTeam(string characterLine, Team team)
    {
        var newCharacter = CreateOrCloneCharacter(characterLine);
        if (newCharacter != null)
        {
            team.Characters.Add(newCharacter);
        }
    }
    private Character CreateOrCloneCharacter(string characterLine)
    {
        var parts = characterLine.Split(" (", 2);
        var characterName = parts[0];
        var skillsText = parts.Length > 1 ? parts[1].TrimEnd(')') : string.Empty;

        var newCharacter = CloneCharacter(characterName);
        AssignSkillsToCharacter(newCharacter, skillsText);
            
        return newCharacter;
    }
    
    private Character CloneCharacter(string characterName)
    {
        var originalCharacter = _characters.FirstOrDefault(c => c.Name == characterName);
        if (originalCharacter != null)
        {
            return new Character
            {
                Name = originalCharacter.Name,
                Weapon = originalCharacter.Weapon,
                Gender = originalCharacter.Gender,
                MaxHP = originalCharacter.MaxHP,
                CurrentHP = originalCharacter.MaxHP,
                Atk = originalCharacter.Atk,
                Spd = originalCharacter.Spd,
                Def = originalCharacter.Def,
                Res = originalCharacter.Res,
            };
        }

        return null;
    }
    
    private void AssignSkillsToCharacter(Character character, string skillsText) {
        if (!string.IsNullOrEmpty(skillsText)) {
            var skillNames = ExtractSkillNames(skillsText);
            var skillFactory = new SkillFactory();
            foreach (var skillName in skillNames) {
                var skill = CreateSkill(skillName, skillFactory);
                character.AddSkill(skill);
            }
        }
    }
    
    private IEnumerable<string> ExtractSkillNames(string skillsText) {
        return skillsText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(skillName => skillName.Trim());
    }
    
    private Skill CreateSkill(string skillName, SkillFactory skillFactory) {
        var skill = _skills.FirstOrDefault(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
        if (skill != null) {
            return skillFactory.CreateSkill(skill.Name, skill.Description);
        } else {
            return skillFactory.CreateSkill(skillName, "Descripción no proporcionada");
        }
    }
    

}