using Fire_Emblem_View;

namespace Fire_Emblem
{
    public class Game
    {
        private View _view;
        private string _teamsFolder;
        private SetUpInterface _setUpInterface;
        private CombatInterface _combatInterface;
        private BattleInterface _battleInterface;
        public Player Player1;
        public Player Player2;
        
        public Game(View view, string teamsFolder)
        {
            _view = view;
            _setUpInterface = new SetUpInterface(view);
            _combatInterface = new CombatInterface(view);
            _battleInterface = new BattleInterface(view);
            _teamsFolder = teamsFolder;
            Player1 = new Player("Player 1");
            Player2 = new Player("Player 2");
        }

        public void Play()
        {
           
            SetUpLogic logic = new SetUpLogic(_view, _teamsFolder, _setUpInterface);

            if (logic.LoadTeams(Player1, Player2))
            {
                Battle battle = new Battle(Player1, Player2, _view, _battleInterface);
                battle.Start();
            }
            
        }

    }
}