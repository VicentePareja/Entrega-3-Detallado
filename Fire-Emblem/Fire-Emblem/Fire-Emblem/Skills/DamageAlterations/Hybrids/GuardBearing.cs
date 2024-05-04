namespace Fire_Emblem {
    public class GuardBearing : Skill {
        public GuardBearing(string name, string description) : base(name, description) {
        }

        private bool firstCombatInitiatedByUnit = false;
        private bool firstCombatInitiatedByOpponent = false;

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = DetermineOpponent(combat, owner);
            ApplyStatPenalties(opponent);
            int damageReductionPercentage = CalculateDamageReduction(combat, owner);
            ApplyDamageReduction(owner, damageReductionPercentage);
        }

        private Character DetermineOpponent(Combat combat, Character owner) {
            return (combat._attacker == owner) ? combat._defender : combat._attacker;
        }

        private void ApplyStatPenalties(Character opponent) {
            opponent.AddTemporaryPenalty("Spd", -4);
            opponent.AddTemporaryPenalty("Def", -4);
        }

        private int CalculateDamageReduction(Combat combat, Character owner) {
            if ((combat._attacker == owner && !firstCombatInitiatedByUnit) ||
                (combat._attacker != owner && !firstCombatInitiatedByOpponent)) {
                UpdateCombatFlags(combat, owner);
                return 60;
            } else {
                return 30;
            }
        }

        private void UpdateCombatFlags(Combat combat, Character owner) {
            if (combat._attacker == owner) {
                firstCombatInitiatedByUnit = true;
            } else {
                firstCombatInitiatedByOpponent = true;
            }
        }

        private void ApplyDamageReduction(Character owner, int damageReductionPercentage) {
            owner.MultiplyTemporaryDamageAlterations("PercentageReduction", damageReductionPercentage);
        }
    }
}