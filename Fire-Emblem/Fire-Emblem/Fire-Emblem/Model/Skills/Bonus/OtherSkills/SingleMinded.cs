namespace Fire_Emblem {
    public class SingleMinded : Skill {
        public int Bonus { get; private set; }

        public SingleMinded(string name, string description) : base(name, description) {
            Bonus = 8;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Character lastOpponent = FindLastOpponent(battle, owner);
            Character currentOpponent = GetCurrentOpponent(battle, owner);

            if (currentOpponent == lastOpponent) {
                owner.AddTemporaryBonus("Atk", Bonus);
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
            if (battle.CurrentCombat._attacker == owner) {
                return battle.CurrentCombat._defender;
            } else if (battle.CurrentCombat._defender == owner) {
                return battle.CurrentCombat._attacker;
            }
            return null;
        }
    }
}