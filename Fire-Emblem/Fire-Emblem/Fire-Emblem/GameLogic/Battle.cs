using Fire_Emblem_View;
namespace Fire_Emblem
{
    public class Battle
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly Dictionary<string, string> _advantages;
        private readonly View _view;
        private bool _gameFinished = false;
        private int _turn = 0;
        private Character _attackerUnit;
        private Character _defenderUnit;
        private string _currentAdvantage;
        public Combat CurrentCombat { get; private set; } = null;
        public List<(Character Attacker, Character Defender)> CombatHistory { get; private set; }

        public Battle(Player player1, Player player2, View view)
        {
            _player1 = player1;
            _player2 = player2;
            _view = view;
            _advantages = new Dictionary<string, string>
            {
                {"Sword", "Axe"},
                {"Axe", "Lance"},
                {"Lance", "Sword"}
            };
            CombatHistory = new List<(Character Attacker, Character Defender)>();
        }

        public void Start()
        {
            _gameFinished = false;
            while (!_gameFinished) 
            {
                _turn++;
                if (_turn % 2 == 1) 
                {
                    PerformTurn(_player1, _player2);
                }
                else
                {
                    PerformTurn(_player2, _player1);
                }
                _gameFinished = IsGameFinished();
            }
            AnnounceWinner();
        }
        

        private void PrepareAttack(Player attackerPlayer, Player defenderPlayer)
        {
            _attackerUnit = ChooseUnit(attackerPlayer);
            _defenderUnit = ChooseUnit(defenderPlayer);
            _view.WriteLine($"Round {_turn}: {_attackerUnit.Name} ({attackerPlayer.Name}) comienza");
            _currentAdvantage = CalculateAdvantage();
            PrintAdvantage();

        }
        
        
        private void PerformTurn(Player attackerPlayer, Player defenderPlayer)
        {
            PrepareAttack(attackerPlayer, defenderPlayer);
            CurrentCombat = new Combat( _attackerUnit, _defenderUnit, _currentAdvantage, _view, this);
            CurrentCombat.Start();
            CombatHistory.Add((Attacker: _attackerUnit, Defender: _defenderUnit));
            
            if (_attackerUnit.CurrentHP <= 0)
            {
                attackerPlayer.Team.Characters.Remove(_attackerUnit);
            }
            if (_defenderUnit.CurrentHP <= 0)
            {
                defenderPlayer.Team.Characters.Remove(_defenderUnit);
            }
        }

        

        public string CalculateAdvantage()
        {
            if (IsAdvantageAttacker())
            {
                return "atacante";
            }
            else if (IsAdvantageDefender())
            {
                return "defensor";
            }
            return "ninguno";
        }

        private bool IsAdvantageAttacker()
        {
            return _advantages.ContainsKey(_attackerUnit.Weapon) &&
                   _advantages[_attackerUnit.Weapon] == _defenderUnit.Weapon;
        }
        
        private bool IsAdvantageDefender()
        {
            return _advantages.ContainsKey(_defenderUnit.Weapon) &&
                   _advantages[_defenderUnit.Weapon] == _attackerUnit.Weapon;
        }

        private void PrintAdvantage()
        {
            switch (_currentAdvantage)
            {
                case "atacante":
                    _view.WriteLine($"{_attackerUnit.Name} ({_attackerUnit.Weapon}) tiene ventaja con respecto a {_defenderUnit.Name} ({_defenderUnit.Weapon})");
                    break;
                case "defensor":
                    _view.WriteLine($"{_defenderUnit.Name} ({_defenderUnit.Weapon}) tiene ventaja con respecto a {_attackerUnit.Name} ({_attackerUnit.Weapon})");
                    break;
                default:
                    _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
                    break;
            }
        }

        private void PrintCharacterOptions(Player player)
        {
            _view.WriteLine($"{player.Name} selecciona una opción");
            for (int i = 0; i < player.Team.Characters.Count; i++)
            {
                _view.WriteLine($"{i}: {player.Team.Characters[i].Name}");
            }
        }

        private Character ChooseUnit(Player player)
        {
            PrintCharacterOptions(player); 
            int choice = -1; 
            do
            {
                string input = _view.ReadLine();
                if (int.TryParse(input, out choice) && choice >= 0 && choice < player.Team.Characters.Count)
                {
                    break;
                }
                else
                {
                    _view.WriteLine("Elección inválida. Por favor, elige de nuevo.");
                }
            } while (true); 

            return player.Team.Characters[choice];
        }


        private bool IsGameFinished()
        {
            return _player1.Team.Characters.Count == 0 || _player2.Team.Characters.Count == 0;
        }

        private void AnnounceWinner()
        {
            if (_player1.Team.Characters.Count == 0)
            {
                _view.WriteLine($"{_player2.Name} ganó");
            }
            else if (_player2.Team.Characters.Count == 0)
            {
                _view.WriteLine($"{_player1.Name} ganó");
            }
            else
            {
                _view.WriteLine("Empate!");
            }
        }
    }
}
