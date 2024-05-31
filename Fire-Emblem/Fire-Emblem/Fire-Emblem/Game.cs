using Fire_Emblem_View;

namespace Fire_Emblem
{
    public class Game
    {
        private readonly string _teamsFolder;
        private readonly SetUpInterface _setUpInterface;
        private readonly SetUpController _setUpController;
        private readonly BattleInterface _battleInterface;
        private readonly BattleController _battleController;
        private readonly Player _player1;
        private readonly Player _player2;
        
        public Game(View view, string teamsFolder)
        {
            _setUpController = new SetUpController(view);
            _setUpInterface = new SetUpInterface(view);
            _battleController = new BattleController(view);
            _battleInterface = new BattleInterface(view);
            _teamsFolder = teamsFolder;
            _player1 = new Player("Player 1");
            _player2 = new Player("Player 2");
        }

        public void Play()
        {
           
            SetUpLogic logic = new SetUpLogic(_teamsFolder, _setUpInterface, _setUpController, _player1, _player2);

            if (logic.LoadTeams(_player1, _player2))
            {
                Battle battle = new Battle(_player1, _player2, _battleInterface, _battleController);
                battle.Start();
            }
            
        }

    }
}