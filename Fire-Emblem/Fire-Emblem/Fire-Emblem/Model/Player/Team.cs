namespace Fire_Emblem;

public class Team
{
    public List<Character> Characters { get; set; }

    public Team()
    {
        Characters = new List<Character>();
    }
    public bool IsTeamValid()
    {
  
        if (Characters.Count < 1 || Characters.Count > 3)
        {
            return false; 
        }
        
        HashSet<string> uniqueNames = new HashSet<string>();
        foreach (var character in Characters)
        {
            
            if (!uniqueNames.Add(character.Name))
            {
                return false; 
            }
            
            if (!AreSkillsValid(character))
            {
                return false; 
            }
        }

        return true;
    }

    private bool AreSkillsValid(Character character)
    {
        
        if (character.Skills.Count > 2)
        {
            return false; 
        }
        
        HashSet<string> uniqueSkills = new HashSet<string>();
        foreach (var skill in character.Skills)
        {
            if (!uniqueSkills.Add(skill.Name))
            {
                return false; 
            }
        }

        return true;
    }

}
