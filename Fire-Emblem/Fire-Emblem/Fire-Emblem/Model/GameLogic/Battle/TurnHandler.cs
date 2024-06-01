using Fire_Emblem_View;

namespace Fire_Emblem {
    public class TurnHandler {
        private readonly Battle _battle;
        private readonly BattleInterface _battleInterface;
        private readonly AdvantageHandler _advantageHandler;
        private readonly BattleController _battleController;
        private Character _attackerUnit;
        private Character _defenderUnit;
        private string _currentAdvantage;

        public TurnHandler(Battle battle, BattleInterface battleInterface, AdvantageHandler advantageHandler, BattleController battleController) {
            _battle = battle;
            _battleInterface = battleInterface;
            _advantageHandler = advantageHandler;
            _battleController = battleController;
        }

        public void PerformTurn(Player attackerPlayer, Player defenderPlayer)
        {
            ChooseUnits(attackerPlayer, defenderPlayer);
            StartRound(attackerPlayer);
            PerformCombat();
            RemoveDefeatedUnit(attackerPlayer, _attackerUnit);
            RemoveDefeatedUnit(defenderPlayer, _defenderUnit);
        }
        private void StartRound(Player attackerPlayer) {
            _battleInterface.PrintRoundStart(_battle.CurrentTurn, _attackerUnit, attackerPlayer);
            _currentAdvantage = _advantageHandler.CalculateAdvantage(_attackerUnit, _defenderUnit);
            _battleInterface.PrintAdvantages(_currentAdvantage, _attackerUnit, _defenderUnit);
        }
        private void ChooseUnits(Player attackerPlayer, Player defenderPlayer) {
            _attackerUnit = ChooseUnit(attackerPlayer);
            _defenderUnit = ChooseUnit(defenderPlayer);
        }

        private Character ChooseUnit(Player player) {
            _battleInterface.PrintCharacterOptions(player);
            int choice;
            do {
                string unitIndex = _battleController.GetUnitIndex();
                if (IsValidCharacter(unitIndex, player, out choice)) {
                    break;
                } else {
                    _battleInterface.PrintNotValidOption();
                }
            } while (true);

            return player.Team.Characters[choice];
        }
        private void PerformCombat() {
            Combat currentCombat = new Combat(_attackerUnit, _defenderUnit, _currentAdvantage, _battleInterface.CombatInterface, _battle);
            _battle.CurrentCombat = currentCombat;
            currentCombat.Start();
            _battle.RecordCombat(_attackerUnit, _defenderUnit);
        }
        private bool IsValidCharacter(string input, Player player, out int choice) {
            return int.TryParse(input, out choice) && choice >= 0 && choice < player.Team.Characters.Count;
        }
        private void RemoveDefeatedUnit(Player player, Character unit) {
            if (unit.CurrentHP <= 0) {
                player.Team.Characters.Remove(unit);
            }
        }
    }
}
