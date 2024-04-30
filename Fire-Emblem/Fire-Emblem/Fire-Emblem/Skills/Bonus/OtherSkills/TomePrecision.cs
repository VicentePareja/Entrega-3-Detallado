namespace Fire_Emblem {
    public class TomePrecision : Skill {
        public int Bonus { get; private set; }

        public TomePrecision(string name, string description) : base(name, description) {
            Bonus = 6;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            if (owner.Weapon == "Magic") {
                owner.AddTemporaryBonus("Spd", Bonus);
                owner.AddTemporaryBonus("Atk", Bonus);
            }
        }
    }
}