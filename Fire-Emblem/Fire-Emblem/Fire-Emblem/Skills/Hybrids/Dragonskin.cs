namespace Fire_Emblem {
    public class Dragonskin : Skill {
        public Dragonskin(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            
            if (opponent == combat._attacker || opponent.CurrentHP >= 0.75 * opponent.MaxHP) {
                
                owner.AddTemporaryBonus("Atk", 6);
                owner.AddTemporaryBonus("Spd", 6);
                owner.AddTemporaryBonus("Def", 6);
                owner.AddTemporaryBonus("Res", 6);

                opponent.DisableAllBonuses();
            }
        }
    }
}