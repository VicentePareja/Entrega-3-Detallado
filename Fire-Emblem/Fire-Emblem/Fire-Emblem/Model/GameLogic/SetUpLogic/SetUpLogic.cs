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
        private readonly SetUpInterface _setUpInterface;
        private readonly SetUpController _setUpController;
        private readonly CharacterFileImporter _characterFileImporter;
        private readonly SkillFileImporter _skillFileImporter;
        private readonly TeamsValidator _teamsValidator;
        private readonly CharacterChooser _characterChooser;
        public SetUpLogic(string teamsFolder, SetUpInterface setUpInterface, SetUpController setUpController, Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
            _setUpInterface = setUpInterface;
            _setUpController = setUpController;
            _teamsFolder = teamsFolder;
            _characterFileImporter = new CharacterFileImporter(Path.Combine(_teamsFolder, "../.."));
            _skillFileImporter = new SkillFileImporter(Path.Combine(_teamsFolder, "../.."));
            _teamsValidator = new TeamsValidator(_player1, _player2);
            _characterChooser = new CharacterChooser(_player1, _player2);
        }

        public bool LoadTeams(Player player1, Player player2)
        {

            ShowAvailableFiles();
            string selectedFile = SelectFile();
            ImportFiles();
    
            if (_teamsValidator.ValidTeams(selectedFile))
            {
                _characterChooser.ChooseCharacters(selectedFile);
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
        
        private void ImportFiles()
        {
            CharacterChooser.SetCharacters(_characterFileImporter.ImportCharacters());
            CharacterChooser.SetSkills(_skillFileImporter.ImportSkills());
        }
        
    }
}