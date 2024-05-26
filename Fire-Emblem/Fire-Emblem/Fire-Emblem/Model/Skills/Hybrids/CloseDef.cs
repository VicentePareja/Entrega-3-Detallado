namespace Fire_Emblem {
    public class CloseDef : Skill {
        public CloseDef(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.CurrentCombat;
            Character opponent;
            if (owner == combat._attacker) {
                opponent = combat._defender;
            }else {
                opponent = combat._attacker;
            }
            
            if (IsDefenderAndAttackerClose(combat, opponent)) {
                owner.AddTemporaryBonus("Def", 8);
                owner.AddTemporaryBonus("Res", 8);
                opponent.DisableAllBonuses();
            }
        }
        
        public bool IsDefenderAndAttackerClose(Combat combat, Character opponent) {
            return opponent == combat._attacker && (opponent.Weapon == "Sword" || opponent.Weapon == "Lance" || opponent.Weapon == "Axe");
        }
        
    }
}