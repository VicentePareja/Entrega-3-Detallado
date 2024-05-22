using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Fire_Emblem_View;

namespace Fire_Emblem
{
    public class SetUpLogic
    {
        private View _view;
        private string _teamsFolder;
        private List<Character> _characters;
        private List<Skill> _skills;
        private Player _player1;
        private Player _player2;
        
        private bool _isPlayer1Team = true;
        private bool _team1Populated = false;
        private bool _team2Populated = false;
        private List<string> _currentTeamNames = new List<string>();

        public SetUpLogic(View view, string teamsFolder)
        {
            _view = view;
            _teamsFolder = teamsFolder;
            _characters = new List<Character>();
            _skills = new List<Skill>();
        }

        public bool LoadTeams(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
    
            ShowAvailableFiles();
            string selectedFile = SelectFile();
    
            if (selectedFile == null)
            {
                _view.WriteLine("Selección inválida.");
                return false;
            }
    
            ImportCharacters();
            ImportSkills();
    
            if (ValidTeams(selectedFile))
            {
                ChooseCharacters(selectedFile);
                return true;
            }
            else
            {
                _view.WriteLine("Archivo de equipos no válido");
                return false;
            }
        }

        private void ShowAvailableFiles()
        {
            _view.WriteLine("Elige un archivo para cargar los equipos");
            var files = Directory.GetFiles(_teamsFolder, "*.txt");
            if (files.Length == 0)
            {
                _view.WriteLine("No hay archivos disponibles.");
                throw new InvalidOperationException("No hay archivos disponibles.");
            }

            for (int i = 0; i < files.Length; i++)
            {
                _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");
            }
        }

        private string SelectFile()
        {
            string input = _view.ReadLine();
            var files = Directory.GetFiles(_teamsFolder, "*.txt");
            if (int.TryParse(input, out int fileIndex) && fileIndex >= 0 && fileIndex < files.Length)
            {
                return files[fileIndex];
            }
            else
            {
                return null;
            }
        }


        public bool ValidTeams(string selectedFile)
        {
            var lines = File.ReadAllLines(selectedFile);
            _isPlayer1Team = true;
            _team1Populated = false;
            _team2Populated = false;
            _currentTeamNames.Clear();

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

        private bool ShouldSwitchTeams(bool switchToPlayer1)
        {
            return switchToPlayer1 != _isPlayer1Team && _currentTeamNames.Any();
        }

        private bool ProcessTeamSwitch(Team team)
        {
            _team2Populated = team == _player2.Team;
            _team1Populated = team == _player1.Team;
            return FinalizeTeam(_currentTeamNames, team);
        }


        private void AddPlayerToTeam(string playerName)
        {
            _currentTeamNames.Add(playerName);
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

        private (string[] lines, bool isPlayer1, Team team1, Team team2, bool team1Populated
            , bool team2Populated, List<string> currentTeamNames) InitializeTeams(string selectedFile)
        {
            var lines = File.ReadAllLines(selectedFile);
            bool isPlayer1 = true;
            Team team1 = new Team();
            Team team2 = new Team();
            bool team1Populated = false;
            bool team2Populated = false;
            List<string> currentTeamNames = new List<string>();
            return (lines, isPlayer1, team1, team2, team1Populated, team2Populated, currentTeamNames);
        }

        private bool FinalizeTeam(List<string> currentTeamNames, Team team)
        {
            bool valid = ValidateAndClearCurrentTeam(currentTeamNames, team);
            currentTeamNames.Clear();
            return valid;
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

        private Character CreateOrCloneCharacter(string characterLine)
        {
            var parts = characterLine.Split(" (", 2);
            var characterName = parts[0];
            var skillsText = parts.Length > 1 ? parts[1].TrimEnd(')') : string.Empty;

            var newCharacter = CloneCharacter(characterName);
            if (newCharacter != null)
            {
                AssignSkillsToCharacter(newCharacter, skillsText);
            }

            return newCharacter;
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



        private void AssignCharacterToTeam(string characterLine, Team team)
        {
            var newCharacter = CreateOrCloneCharacter(characterLine);
            if (newCharacter != null)
            {
                team.Characters.Add(newCharacter);
            }
            else
            {
                _view.WriteLine($"Personaje no encontrado: {characterLine.Split(" (", 2)[0]}");
            }
        }

        public void ImportCharacters()
        {
            string jsonPath = Path.Combine(_teamsFolder, "../..", "characters.json"); 

            try
            {
                string jsonString = File.ReadAllText(jsonPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new StringToIntConverter() }
                };

                _characters = JsonSerializer.Deserialize<List<Character>>(jsonString, options);
            }
            catch (Exception ex)
            {
                _view.WriteLine($"Error al importar personajes: {ex.Message}");
            }
        }
        
        public void ImportSkills()
        {
            string jsonPath = Path.Combine(_teamsFolder, "../..", "skills.json");

            try
            {
                string jsonString = File.ReadAllText(jsonPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<Skill> loadedSkills = JsonSerializer.Deserialize<List<Skill>>(jsonString, options);
                if (loadedSkills != null) {
                    _skills = new List<Skill>();
                    var skillFactory = new SkillFactory();
                    foreach (var loadedSkill in loadedSkills) {
                        Skill skill = skillFactory.CreateSkill(loadedSkill.Name, loadedSkill.Description);
                        _skills.Add(skill);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al importar habilidades: {ex.Message}");
            }
        }
    

    }
}