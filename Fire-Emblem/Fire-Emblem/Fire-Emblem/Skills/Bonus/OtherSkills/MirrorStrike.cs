namespace Fire_Emblem {
    public class MirrorStrikeSkill : Skill {
        public int Bonus { get; private set; }

        public MirrorStrikeSkill(string name, string description) : base(name, description) {
            Bonus = 6;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Atk", Bonus);
                owner.AddTemporaryBonus("Res", Bonus);
            }
        }
    }
}