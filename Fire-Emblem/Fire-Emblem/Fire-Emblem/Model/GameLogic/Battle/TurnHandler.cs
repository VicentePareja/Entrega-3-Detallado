using Fire_Emblem_View;

namespace Fire_Emblem {
    public class TurnHandler {
        private readonly Battle _battle;
        private readonly BattleInterface _battleInterface;
        private readonly AdvantageHandler _advantageHandler;
        private readonly BattleController _battleController;

        public TurnHandler(Battle battle, BattleInterface battleInterface, AdvantageHandler advantageHandler, BattleController battleController) {
            _battle = battle;
            _battleInterface = battleInterface;
            _advantageHandler = advantageHandler;
            _battleController = battleController;
        }

        public void PerformTurn(Player attackerPlayer, Player defenderPlayer) {
            var attackerUnit = ChooseUnit(attackerPlayer);
            var defenderUnit = ChooseUnit(defenderPlayer);
            _battleInterface.PrintRoundStart(_battle.CurrentTurn, attackerUnit, attackerPlayer);
            string currentAdvantage = _advantageHandler.CalculateAdvantage(attackerUnit, defenderUnit);
            _battleInterface.PrintAdvantages(currentAdvantage, attackerUnit, defenderUnit);

            Combat currentCombat = new Combat(attackerUnit, defenderUnit, currentAdvantage, _battleInterface.CombatInterface, _battle);
            _battle.CurrentCombat = currentCombat;
            currentCombat.Start();
            _battle.RecordCombat(attackerUnit, defenderUnit);

            RemoveDefeatedUnit(attackerPlayer, attackerUnit);
            RemoveDefeatedUnit(defenderPlayer, defenderUnit);
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
