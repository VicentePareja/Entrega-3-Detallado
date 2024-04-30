namespace Fire_Emblem {
    public class FairFightSkill : Skill {
        public int Bonus { get; private set; }

        public FairFightSkill(string name, string description) : base(name, description) {
            Bonus = 6;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            if (combat._attacker == owner) {
                owner.AddTemporaryBonus("Atk", Bonus);
                combat._defender.AddTemporaryBonus("Atk", Bonus);
            }
        }
    }
}