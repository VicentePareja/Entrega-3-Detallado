namespace Fire_Emblem;

public class TeamsValidator
{
    private readonly SetUpLogic _setUpLogic;
    private readonly Player _player1;
    private readonly Player _player2;
    private bool _isPlayer1Team = true;
    private List<string> _currentTeamNames = new List<string>();
    private bool _team1Populated = false;
    private bool _team2Populated = false;
    public TeamsValidator( Player player1, Player player2)
    {
        _player1 = player1;
        _player2 = player2;
    }
   
    public bool ValidTeams(string selectedFile)
    {
        var lines = File.ReadAllLines(selectedFile);
        ProcessTeamLines(lines);
        return CheckFinalTeamsPopulated();
    }
    
    private void ProcessTeamLines(string[] lines)
    {
        foreach (var line in lines)
        {
            if (line == "Player 1 Team" || line == "Player 2 Team")
            {
                HandleTeamSwitch(line);
            }
            else
            {
                AddPlayerToTeam(line);
            }
        }
    }
    
    private void HandleTeamSwitch(string line)
    {
        bool switchToPlayer1 = line == "Player 1 Team";
        if (ShouldSwitchTeams(switchToPlayer1))
        {
            if (switchToPlayer1)
            {
                if (!ProcessTeamSwitch(_player2.Team))
                    return;
            }
            else
            {
                if (!ProcessTeamSwitch(_player1.Team))
                    return;
            }
        }
        _isPlayer1Team = switchToPlayer1;
    }
    
    private bool CheckFinalTeamsPopulated()
    {
        if (_currentTeamNames.Any())
        {
            if (_isPlayer1Team)
            {
                _team1Populated = true;
            }
            else
            {
                _team2Populated = true;
            }
            if (!FinalizeTeam(_currentTeamNames, _isPlayer1Team ? _player1.Team : _player2.Team)) return false;
        }

        return _team1Populated && _team2Populated;
    }
    
    private bool FinalizeTeam(List<string> currentTeamNames, Team team)
    {
        bool valid = ValidateAndClearCurrentTeam(currentTeamNames, team);
        currentTeamNames.Clear();
        return valid;
    }
    
    private bool ValidateAndClearCurrentTeam(List<string> characterNames, Team team)
    {
        foreach (var characterLine in characterNames)
        {
            var character = CreateCharacterFromLine(characterLine);
            team.Characters.Add(character);
        }

        bool isValid = ValidateTeam(team);
        ClearTeamCharacters(team);

        return isValid;
    }
    
    private Character CreateCharacterFromLine(string characterLine)
    {
        var parts = characterLine.Split(" (", 2);
        var characterName = parts[0];
        var skillsText = parts.Length > 1 ? parts[1].TrimEnd(')') : string.Empty;

        var character = new Character { Name = characterName };

        if (!string.IsNullOrEmpty(skillsText))
        {
            var skillNames = skillsText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var skillName in skillNames)
            {
                var trimmedSkillName = skillName.Trim();
                character.AddSkill(new GenericSkill(trimmedSkillName, "Descripción no proporcionada")); 
            }
        }

        return character;
    }
    
    private bool ValidateTeam(Team team)
    {
        return team.IsTeamValid();
    }

    private void ClearTeamCharacters(Team team)
    {
        team.Characters.Clear();
    }
    
    private bool ProcessTeamSwitch(Team team)
    {
        _team2Populated = team == _player2.Team;
        _team1Populated = team == _player1.Team;
        return FinalizeTeam(_currentTeamNames, team);
    }
    
    private bool ShouldSwitchTeams(bool switchToPlayer1)
    {
        return switchToPlayer1 != _isPlayer1Team && _currentTeamNames.Any();
    }
        
    private void AddPlayerToTeam(string playerName)
    {
        _currentTeamNames.Add(playerName);
    }
}