namespace Fire_Emblem {
    public class AdvantageHandler {
        private readonly Dictionary<string, string> _advantages;

        public AdvantageHandler() {
            _advantages = new Dictionary<string, string> {
                {"Sword", "Axe"},
                {"Axe", "Lance"},
                {"Lance", "Sword"}
            };
        }

        public string CalculateAdvantage(Character attacker, Character defender) {
            if (IsAdvantage(attacker.Weapon, defender.Weapon)) {
                return "atacante";
            }
            else if (IsAdvantage(defender.Weapon, attacker.Weapon)) {
                return "defensor";
            }
            return "ninguno";
        }

        private bool IsAdvantage(string weapon1, string weapon2) {
            return _advantages.ContainsKey(weapon1) && _advantages[weapon1] == weapon2;
        }
    }
}