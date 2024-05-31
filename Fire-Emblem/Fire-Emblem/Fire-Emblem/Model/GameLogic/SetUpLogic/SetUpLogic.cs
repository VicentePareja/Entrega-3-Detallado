using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Fire_Emblem_View;

namespace Fire_Emblem
{
    public class SetUpLogic
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly string _teamsFolder;
        private List<Character> _characters;
        private List<Skill> _skills;
        private readonly SetUpInterface _setUpInterface;
        private readonly SetUpController _setUpController;
        private bool _isPlayer1Team = true;
        private readonly CharacterFileImporter _characterFileImporter;
        private readonly SkillFileImporter _skillFileImporter;
        private readonly TeamsValidator _teamsValidator;
        

        public SetUpLogic(string teamsFolder, SetUpInterface setUpInterface, SetUpController setUpController, Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
            _setUpInterface = setUpInterface;
            _setUpController = setUpController;
            _teamsFolder = teamsFolder;
            _characters = new List<Character>();
            _skills = new List<Skill>();
            _characterFileImporter = new CharacterFileImporter(Path.Combine(_teamsFolder, "../.."));
            _skillFileImporter = new SkillFileImporter(Path.Combine(_teamsFolder, "../.."));
            _teamsValidator = new TeamsValidator(this, _player1, _player2);
        }

        public bool LoadTeams(Player player1, Player player2)
        {

            ShowAvailableFiles();
            string selectedFile = SelectFile();
            ImportFiles();
    
            if (ValidTeams(selectedFile))
            {
                ChooseCharacters(selectedFile);
                return true;
            }
            else
            {
                _setUpInterface.PrintTeamsNotValid();
                return false;
            }
        }

        private void ShowAvailableFiles()
        {
            _setUpInterface.PrintGetTeamsFolder();
            var files = Directory.GetFiles(_teamsFolder, "*.txt");
            if (files.Length == 0)
            {
                _setUpInterface.PrintNotFilesInFolder();
                throw new InvalidOperationException("No hay archivos disponibles.");
            }

            for (int i = 0; i < files.Length; i++)
            {
                _setUpInterface.PrintFile(i, Path.GetFileName(files[i]));
            }
        }

        private string SelectFile()
        {
            string input = _setUpController.GetTeamsFolder();
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
            AssignSkillsToCharacter(newCharacter, skillsText);
            
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
                _setUpInterface.PrintCharacterNotFound(characterLine.Split(" (", 2)[0]);
                
            }
        }
        
        public void ImportFiles()
        {
            _characters = _characterFileImporter.ImportCharacters();
            _skills = _skillFileImporter.ImportSkills();
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
        
        public bool ValidTeams(string selectedFile)
        {
            return _teamsValidator.ValidTeams(selectedFile);
        }
        
    }
}