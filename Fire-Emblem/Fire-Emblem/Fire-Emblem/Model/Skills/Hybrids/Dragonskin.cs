namespace Fire_Emblem {
    public class Dragonskin : Skill {
        public Dragonskin(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.CurrentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            
            if (IsEffectApllied(combat, opponent)) {
                
                AddBonuses(owner);
                opponent.DisableAllBonuses();
            }
        }
        public bool IsEffectApllied(Combat combat, Character opponent) {
            return opponent == combat._attacker || opponent.CurrentHP >= 0.75 * opponent.MaxHP;
        }
        
        public void AddBonuses(Character owner) {
            owner.AddTemporaryBonus("Atk", 6);
            owner.AddTemporaryBonus("Spd", 6);
            owner.AddTemporaryBonus("Def", 6);
            owner.AddTemporaryBonus("Res", 6);
        }
    }
}