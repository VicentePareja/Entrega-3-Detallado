namespace Fire_Emblem {
    public class LancePower : Skill {
        public int AttackBonus { get; private set; }
        public int DefensePenalty { get; private set; }

        public LancePower(string name, string description) : base(name, description) {
            AttackBonus = 10;   
            DefensePenalty = -10;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            if (owner.Weapon == "Lance") {
                owner.AddTemporaryBonus("Atk", AttackBonus);
                owner.AddTemporaryPenalty("Def", DefensePenalty);
            }
        }
    }
}