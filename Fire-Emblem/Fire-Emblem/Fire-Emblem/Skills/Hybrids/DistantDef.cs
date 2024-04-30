namespace Fire_Emblem {
    public class DistantDef : Skill {
        public DistantDef(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent;
            if (owner == combat._attacker) {
                opponent = combat._defender;
            } else {
                opponent = combat._attacker;
            }
            
            if (opponent == combat._attacker && (opponent.Weapon == "Magic" || opponent.Weapon == "Bow")) {
                owner.AddTemporaryBonus("Def", 8);
                owner.AddTemporaryBonus("Res", 8);
                opponent.DisableAllBonuses();
            }
        }
    }
}