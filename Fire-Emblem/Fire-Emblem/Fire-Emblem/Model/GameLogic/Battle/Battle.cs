using Fire_Emblem_View;

namespace Fire_Emblem {
    public class Battle
    {
        public Combat CurrentCombat;
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly BattleInterface _battleInterface;
        private readonly BattleController _battleController;
        private readonly AdvantageHandler _advantageHandler;
        private readonly TurnHandler _turnHandler;
        private bool _gameFinished = false;
        private int _turn = 0;
        public List<(Character Attacker, Character Defender)> CombatHistory { get; private set; }

        public int CurrentTurn => _turn;

        public Battle(Player player1, Player player2, BattleInterface battleInterface, BattleController battleController) {
            _player1 = player1;
            _player2 = player2;
            _battleInterface = battleInterface;
            _battleController = battleController;
            _advantageHandler = new AdvantageHandler();
            _turnHandler = new TurnHandler(this, battleInterface, _advantageHandler, battleController);
            CombatHistory = new List<(Character Attacker, Character Defender)>();
        }

        public void Start() {
            _gameFinished = false;
            while (!_gameFinished) {
                _turn++;
                if (_turn % 2 == 1) {
                    _turnHandler.PerformTurn(_player1, _player2);
                } else {
                    _turnHandler.PerformTurn(_player2, _player1);
                }
                _gameFinished = IsGameFinished();
            }
            AnnounceWinner();
        }

        private bool IsGameFinished() {
            return _player1.Team.Characters.Count == 0 || _player2.Team.Characters.Count == 0;
        }

        private void AnnounceWinner() {
            if (_player1.Team.Characters.Count == 0) {
                _battleInterface.PrintWinner(_player2);
            } else if (_player2.Team.Characters.Count == 0) {
                _battleInterface.PrintWinner(_player1);
            } else {
                _battleInterface.PrintTie();
            }
        }

        public void RecordCombat(Character attacker, Character defender) {
            CombatHistory.Add((Attacker: attacker, Defender: defender));
        }
    }
}
