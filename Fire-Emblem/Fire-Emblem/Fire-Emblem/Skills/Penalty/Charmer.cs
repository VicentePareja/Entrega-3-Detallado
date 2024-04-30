namespace Fire_Emblem {
    public class Charmer : Skill {
        public int Penalty { get; private set; }

        public Charmer(string name, string description) : base(name, description) {
            Penalty = -3;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Character lastOpponent = FindLastOpponent(battle, owner);
            Character currentOpponent = GetCurrentOpponent(battle, owner);
            
            if (currentOpponent == lastOpponent) {
                currentOpponent.AddTemporaryPenalty("Atk", Penalty);
                currentOpponent.AddTemporaryPenalty("Spd", Penalty);
            }
        }

        private Character FindLastOpponent(Battle battle, Character owner) {
            Character lastOpponent = null;
            for (int i = 0; i < battle.CombatHistory.Count; i++) {
                var combat = battle.CombatHistory[i];
                if (combat.Attacker == owner) {
                    lastOpponent = combat.Defender;
                } else if (combat.Defender == owner) {
                    lastOpponent = combat.Attacker;
                }
            }
            return lastOpponent;
        }

        private Character GetCurrentOpponent(Battle battle, Character owner) {
            if (battle.currentCombat._attacker == owner) {
                return battle.currentCombat._defender;
            } else if (battle.currentCombat._defender == owner) {
                return battle.currentCombat._attacker;
            }
            return null;
        }
    }
}