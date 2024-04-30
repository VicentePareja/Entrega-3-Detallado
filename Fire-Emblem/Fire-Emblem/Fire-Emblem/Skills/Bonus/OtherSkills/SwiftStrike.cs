namespace Fire_Emblem {
    public class SwiftStrikeSkill : Skill {
        public int Bonus { get; private set; }

        public SwiftStrikeSkill(string name, string description) : base(name, description) {
            Bonus = 6;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Spd", Bonus);
                owner.AddTemporaryBonus("Res", Bonus);
            }
        }
    }
}